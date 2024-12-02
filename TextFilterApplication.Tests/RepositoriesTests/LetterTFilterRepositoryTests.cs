using Microsoft.Extensions.Logging;
using Moq;
using TextFilterApplication.Exceptions;
using TextFilterApplication.Repositories;

namespace TextFilterApplication.Tests.RepositoriesTests
{
    /// <summary>
    /// Filters words based on character 't'.
    /// </summary>
    public class LetterTFilterRepositoryTests
    {
        private readonly Mock<ILogger<LetterTFilterRepository>> mockLogger;

        private readonly LetterTFilterRepository repository;

        public LetterTFilterRepositoryTests()
        {
            mockLogger = new Mock<ILogger<LetterTFilterRepository>>();
            repository = new LetterTFilterRepository(mockLogger.Object);
        }

        [Fact]
        public void Apply_ShouldFilterWordsContainingLetterT()
        {
            // Arrange
            var input = "This is a test sentence";
            var expected = "This is a";

            // Act
            var result = repository.Apply(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Apply_ShouldReturnEmptyStringWhenAllWordsContainLetterT()
        {
            // Arrange
            var input = "This that";
            var expected = "This";

            // Act
            var result = repository.Apply(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Apply_ShouldReturnInputWhenNoWordsContainLetterT()
        {
            // Arrange
            var input = "Hello world";
            var expected = "Hello world";

            // Act
            var result = repository.Apply(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Apply_ShouldHandleEmptyString()
        {
            // Arrange
            var input = "";
            var expected = "";

            // Act
            var result = repository.Apply(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Apply_ShouldHandleStringWithMixedCase()
        {
            // Arrange
            var input = "Test this That";
            var expected = "";

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
            Assert.Equal("An error occurred during filtering in LetterTFilterRepository.", exception.Message);

            // Verify the logger logs an error
            mockLogger.Verify(
                logger => logger.Log(
                    It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("An error occurred during filtering in LetterTFilterRepository.")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }
    }
}
