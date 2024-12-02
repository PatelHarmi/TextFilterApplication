using TextFilterApplication.Repositories;

namespace TextFilterApplication.Tests.RepositoriesTests
{
    public class LengthFilterRepositoryTests
    {
        private readonly LengthFilterRepository repository;

        public LengthFilterRepositoryTests()
        {
            repository = new LengthFilterRepository();
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
    }
}
