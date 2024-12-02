using Microsoft.Extensions.Logging;
using Moq;
using TextFilterApplication.Exceptions;
using TextFilterApplication.Repositories;

namespace TextFilterApplication.Tests.RepositoriesTests
{
    public class LengthFilterRepositoryTests
    {
        private readonly Mock<ILogger<LengthFilterRepository>> mockLogger;

        private readonly LengthFilterRepository repository;

        public LengthFilterRepositoryTests()
        {
            mockLogger = new Mock<ILogger<LengthFilterRepository>>();
            repository = new LengthFilterRepository(mockLogger.Object);
        }

        [Fact]
        public void Apply_ShouldFilterWordsLessThanThreeCharacters()
        {
            // Arrange
            var input = "I am a developer";
            var expected = "developer";

            // Act
            var result = repository.Apply(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Apply_ShouldReturnEmptyStringWhenAllWordsAreTooShort()
        {
            // Arrange
            var input = "I a";
            var expected = "";

            // Act
            var result = repository.Apply(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Apply_ShouldReturnInputWhenAllWordsAreLongEnough()
        {
            // Arrange
            var input = "This is a test";
            var expected = "This test";

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
        public void Apply_ShouldLogErrorAndThrowException_OnFailure()
        {
            // Arrange
            string input = null;
            mockLogger.Reset();

            // Act & Assert
            var exception = Assert.Throws<TextFilterException>(() => repository.Apply(input));
            Assert.Equal("An error occurred during filtering in LengthFilterRepository.", exception.Message);

            // Verify the logger logs an error
            mockLogger.Verify(
                logger => logger.Log(
                    It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("An error occurred during filtering in LengthFilterRepository.")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }
    }
}
