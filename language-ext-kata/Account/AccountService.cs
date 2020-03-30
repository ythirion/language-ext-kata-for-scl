using System;
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

        private Try<RegistrationContext> CreateContext(Guid userId) =>
            Try(() => userService.FindById(userId)).Map(user => new RegistrationContext(user));

        private Try<RegistrationContext> RegisterOnTwitter(RegistrationContext context) =>
            Try(() => twitterService.Register(context.Email, context.Name)).Map(context.SetAccount);

        private Try<RegistrationContext> AuthenticateOnTwitter(RegistrationContext context) =>
            Try(() => twitterService.Authenticate(context.Email, context.Password)).Map(context.SetToken);

        private Try<RegistrationContext> Tweet(RegistrationContext context) =>
            Try(() => twitterService.Tweet(context.Token, "Hello I am " + context.Name)).Map(context.SetTweetUrl);

        private Try<RegistrationContext> UpdateUser(RegistrationContext context) =>
            Try(() =>
            {
                userService.UpdateTwitterAccountId(context.Id, context.AccountId);
                return context;
            });

        public Option<string> Register(Guid id)
        {
            string result = null;

            CreateContext(id)
                    .Bind(RegisterOnTwitter)
                    .Bind(AuthenticateOnTwitter)
                    .Bind(Tweet)
                    .Bind(UpdateUser)
                    .Do((context) => result = context.Url)
                    .Match(context => businessLogger.LogSuccessRegister(context.Id),
                        failure => businessLogger.LogFailureRegister(id, failure));

            return result;
        }
    }

}
