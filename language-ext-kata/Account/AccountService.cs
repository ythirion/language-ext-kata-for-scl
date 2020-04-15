using System;
using System.Threading.Tasks;
using LanguageExt;
using static LanguageExt.Prelude;

namespace language_ext.kata.Account
{
    public class AccountService
    {
        private readonly UserService userService;
        private readonly TwitterService twitterService;
        private readonly IBusinessLogger businessLogger;

        public AccountService(UserService userService, TwitterService twitterService, IBusinessLogger businessLogger)
        {
            this.userService = userService;
            this.twitterService = twitterService;
            this.businessLogger = businessLogger;
        }

        private TryAsync<RegistrationContext> CreateContext(Guid userId) =>
            TryAsync(() => userService.FindById(userId)).Map(user => new RegistrationContext(user));

        private TryAsync<RegistrationContext> RegisterOnTwitter(RegistrationContext context) =>
            TryAsync(() => twitterService.Register(context.Email, context.Name)).Map(context.SetAccount);

        private TryAsync<RegistrationContext> AuthenticateOnTwitter(RegistrationContext context) =>
            TryAsync(() => twitterService.Authenticate(context.Email, context.Password)).Map(context.SetToken);

        private TryAsync<RegistrationContext> Tweet(RegistrationContext context) =>
            TryAsync(() => twitterService.Tweet(context.Token, "Hello I am " + context.Name)).Map(context.SetTweetUrl);

        private TryAsync<RegistrationContext> UpdateUser(RegistrationContext context) =>
            TryAsync(async () =>
            {
                await userService.UpdateTwitterAccountId(context.Id, context.AccountId);
                return context;
            });

        public async Task<Option<string>> Register(Guid id)
        {
            return await CreateContext(id)
                    .Bind(RegisterOnTwitter)
                    .Bind(AuthenticateOnTwitter)
                    .Bind(Tweet)
                    .Bind(UpdateUser)
                    .Do(context => businessLogger.LogSuccessRegister(context.Id))
                    .Map(context => context.Url)
                    .IfFail(failure =>
                    {
                        businessLogger.LogFailureRegister(id, failure);
                        return (string)null;
                    });
        }
    }
}