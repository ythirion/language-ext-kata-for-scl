using System;
using LanguageExt;
using static LanguageExt.Prelude;
using Xunit;

namespace language_ext.kata.tests
{
    public class TryExercises : PetDomainKata
    {
        private const string SUCCESS_MESSAGE = "I m a fucking genius the result is ";

        [Fact]
        public void GetTheResultOfDivide()
        {
            // Divide x = 9 by y = 2
            Try<int> tryResult = Divide(9, 2);
            int result = 0;

            Assert.Equal(4, result);
            Assert.True(tryResult.IsSucc());
            Assert.False(tryResult.IsDefault());
            Assert.False(tryResult.IsFail());
        }

        [Fact]
        public void MapTheResultOfDivide()
        {
            // Divide x = 9 by y = 2 and add z to the result
            int z = 3;
            int result = 0;

            Assert.Equal(7, result);
        }


        [Fact]
        public void DivideByZeroIsAlwaysAGoodIdea()
        {
            // Divide x by 0 and get the result
            int x = 1;
            Assert.Throws<DivideByZeroException>(() => { });
        }

        [Fact]
        public void DivideByZeroOrElse()
        {
            // Divide x by 0, on exception returns 0
            int x = 1;
            int result = -1;

            Assert.Equal(0, result);
        }

        [Fact]
        public void MapTheFailure()
        {
            // Divide x by 0, log the failure message to the console and get 0
            int x = 1;

            int result = -1;

            Assert.Equal(0, result);
        }

        [Fact]
        public void MapTheSuccess()
        {
            // Divide x by y
            // log the failure message to the console
            // Log your success to the console
            // Get the result or 0 if exception
            int x = 8;
            int y = 4;

            var result = -1;

            Assert.Equal(2, result);
        }

        [Fact]
        public void ChainTheTry()
        {
            // Divide x by y
            // Chain 2 other calls to divide with x = previous Divide result
            // log the failure message to the console
            // Log your success to the console
            // Get the result or 0 if exception
            int x = 27;
            int y = 3;

            int result = -1;

            Assert.Equal(1, result);
        }

        private static Try<int> Divide(int x, int y)
        {
            return Try(() => x / y);
        }
    }
}