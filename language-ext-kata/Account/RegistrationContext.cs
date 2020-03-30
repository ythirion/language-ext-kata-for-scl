using System;

namespace language_ext.kata.Account
{
    public class RegistrationContext
    {
        public Guid Id { get; }
        public string Email { get; }
        public string Name { get; }
        public string Password { get; }
        public string AccountId { get; private set; }
        public string Token { get; private set; }
        public string Url { get; private set; }

        public RegistrationContext(User user)
        {
            Id = user.Id;
            Email = user.Email;
            Name = user.Name;
            Password = user.Password;
        }

        internal RegistrationContext SetAccount(string accountId)
        {
            AccountId = accountId;
            return this;
        }

        internal RegistrationContext SetToken(string token)
        {
            Token = token;
            return this;
        }

        internal RegistrationContext SetTweetUrl(string tweetUrl)
        {
            Url = tweetUrl;
            return this;
        }
    }
}