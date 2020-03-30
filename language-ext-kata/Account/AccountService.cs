using System;

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

        public string Register(Guid id)
        {
            try
            {
                User user = userService.FindById(id);

                if (user == null)
                    return null;

                string accountId = twitterService.Register(user.Email, user.Name);

                if (accountId == null)
                    return null;

                string twitterToken = twitterService.Authenticate(user.Email, user.Password);

                if (twitterToken == null)
                    return null;

                string tweetUrl = twitterService.Tweet(twitterToken, "Hello I am " + user.Name);

                if (tweetUrl == null)
                    return null;

                userService.UpdateTwitterAccountId(id, accountId);
                businessLogger.LogSuccessRegister(id);

                return tweetUrl;
            }
            catch (Exception ex)
            {
                businessLogger.LogFailureRegister(id, ex);
                return null;
            }
        }
    }

}
