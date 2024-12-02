using TextFilterApplication.Repositories;

namespace TextFilterApplication.Tests.RepositoriesTests
{
    public class LetterTFilterRepositoryTests
    {
        private readonly LetterTFilterRepository repository;

        public LetterTFilterRepositoryTests()
        {
            repository = new LetterTFilterRepository();
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
    }
}
