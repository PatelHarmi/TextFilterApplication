using Microsoft.Extensions.Logging;
using Moq;
using TextFilterApplication.Exceptions;
using TextFilterApplication.Repositories;

namespace TextFilterApplication.Tests.RepositoriesTests
{
    public class VowelMiddleFilterRepositoryTests
    {
        private readonly Mock<ILogger<VowelMiddleFilterRepository>> mockLogger;

        private readonly VowelMiddleFilterRepository repository;

        public VowelMiddleFilterRepositoryTests()
        {
            mockLogger = new Mock<ILogger<VowelMiddleFilterRepository>>();
            repository = new VowelMiddleFilterRepository(mockLogger.Object);
        }

        [Theory]
        [InlineData("clean what currently door the rather", "the rather")]
        [InlineData("clean what currently", "")]
        [InlineData("the rather", "the rather")]
        [InlineData("a b c", "a b c")]
        [InlineData("what rather here", "rather")]
        [InlineData("clean door rather", "rather")]
        [InlineData("clean what the rather", "the rather")]
        public void Apply_ShouldFilterOutWordsWithVowelInMiddle(string input, string expected)
        {
            // Act
            var result = repository.Apply(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("test", "")]
        [InlineData("Hello", "Hello")]
        public void Apply_ShouldHandleEmptyOrWhitespaceOrSingleWordInput(string input, string expected)
        {
            // Act
            var result = repository.Apply(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("clean", "")]
        [InlineData("what", "")]
        [InlineData("currently", "")]
        [InlineData("clean what currently", "")]
        public void Apply_ShouldFilterOutWordsWithVowelInMiddleForSpecificWords(string input, string expected)
        {
            // Act
            var result = repository.Apply(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("the rather", "the rather")]
        [InlineData("test shot boat", "")]
        [InlineData("rather here", "rather")]
        [InlineData("clean door rather", "rather")]
        public void Apply_ShouldNotFilterOutWordsWithoutVowelInMiddle(string input, string expected)
        {
            // Act
            var result = repository.Apply(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("clean what the rather", "the rather")]
        [InlineData("door how", "")]
        [InlineData("test", "")]
        public void Apply_ShouldHandleMixedCaseWordsCorrectly(string input, string expected)
        {
            // Act
            var result = repository.Apply(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("a b c", "a b c")]
        [InlineData("test shot boat", "")]
        [InlineData("what", "")]
        [InlineData("clean", "")]
        public void Apply_ShouldHandleSingleLetterWordsAndVowelMiddleCorrectly(string input, string expected)
        {
            // Act
            var result = repository.Apply(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("clean what the rather", "the rather")]
        [InlineData("what rather", "rather")]
        [InlineData("clean door", "")]
        [InlineData("clean bank", "")]
        public void Apply_ShouldFilterOutWordsWithVowelMiddleFromMultipleInputs(string input, string expected)
        {
            // Act
            var result = repository.Apply(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Apply_ShouldLogErrorAndThrowException_OnFailure()
        {
            // Arrange
            string input = null;
            mockLogger.Reset();

            // Act & Assert
            var exception = Assert.Throws<TextFilterException>(() => repository.Apply(input));
            Assert.Equal("An error occurred during filtering in VowelMiddleFilterRepository.", exception.Message);

            // Verify the logger logs an error
            mockLogger.Verify(
                logger => logger.Log(
                    It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("An error occurred during filtering in VowelMiddleFilterRepository.")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }
    }
}
