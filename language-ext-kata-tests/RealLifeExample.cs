using System;
using language_ext.kata.Account;
using Xunit;

namespace language_ext.kata.tests
{
    public class RealLifeExample
    {
        private static readonly Guid BUD_SPENCER = new Guid("376510ae-4e7e-11ea-b77f-2e728ce88125");
        private static readonly Guid UNKNOWN_USER = new Guid("376510ae-4e7e-11ea-b77f-2e728ce88121");

        private readonly AccountService accountService;

        public RealLifeExample() =>
            accountService = new AccountService(
                new UserService(),
                new TwitterService(),
                new BusinessLogger());

        [Fact]
        public void Register_BudSpencer_should_return_a_new_tweet_url()
        {
            var tweetUrl = accountService.Register(BUD_SPENCER);
            Assert.Equal("TweetUrl", tweetUrl);
        }

        [Fact]
        public void Register_an_unknown_user_should_return_an_error_message()
        {
            var tweetUrl = accountService.Register(UNKNOWN_USER);
            Assert.Null(tweetUrl);
        }
    }
}