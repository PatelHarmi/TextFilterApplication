using Microsoft.Extensions.Logging;
using Moq;
using TextFilterApplication.Repositories;
using TextFilterApplication.Settings;

namespace TextFilterApplication.Tests.TestFilterTests
{
    public class TextFilterProcessorTests
    {
        private readonly Mock<IFileSettings> _mockFileSettings;
        private readonly Mock<ITextFilterRepository> _mockVowelFilter;
        private readonly Mock<ITextFilterRepository> _mockLengthFilter;
        private readonly Mock<ITextFilterRepository> _mockLetterTFilter;
        private readonly Mock<ILogger<TextFilterProcessor>> _mockLogger;

        public TextFilterProcessorTests()
        {
            _mockFileSettings = new Mock<IFileSettings>();
            _mockVowelFilter = new Mock<ITextFilterRepository>();
            _mockLengthFilter = new Mock<ITextFilterRepository>();
            _mockLetterTFilter = new Mock<ITextFilterRepository>();
            _mockLogger = new Mock<ILogger<TextFilterProcessor>>();
        }

        [Fact]
        public void ApplyFilters_ProcessesFiltersCorrectly()
        {
            // Arrange
            var inputText = "test input data";
            var intermediateResult1 = "input data";
            var intermediateResult2 = "data";
            var finalResult = "data";

            _mockVowelFilter.Setup(f => f.Apply(inputText)).Returns(intermediateResult1);
            _mockLengthFilter.Setup(f => f.Apply(intermediateResult1)).Returns(intermediateResult2);
            _mockLetterTFilter.Setup(f => f.Apply(intermediateResult2)).Returns(finalResult);

            var filters = new List<ITextFilterRepository>
            {
                _mockVowelFilter.Object,
                _mockLengthFilter.Object,
                _mockLetterTFilter.Object
            };

            var processor = new TextFilterProcessor(filters);

            // Act
            var result = processor.ApplyFilters(inputText);

            // Assert
            Assert.Equal(finalResult, result);
            _mockVowelFilter.Verify(f => f.Apply(inputText), Times.Once);
            _mockLengthFilter.Verify(f => f.Apply(intermediateResult1), Times.Once);
            _mockLetterTFilter.Verify(f => f.Apply(intermediateResult2), Times.Once);
        }

        [Fact]
        public void ApplyFilters_HandlesEmptyInputGracefully()
        {
            // Arrange
            var inputText = string.Empty;

            _mockVowelFilter.Setup(f => f.Apply(inputText)).Returns(inputText);
            _mockLengthFilter.Setup(f => f.Apply(inputText)).Returns(inputText);
            _mockLetterTFilter.Setup(f => f.Apply(inputText)).Returns(inputText);

            var filters = new List<ITextFilterRepository>
            {
                _mockVowelFilter.Object,
                _mockLengthFilter.Object,
                _mockLetterTFilter.Object
            };

            var processor = new TextFilterProcessor(filters);

            // Act
            var result = processor.ApplyFilters(inputText);

            // Assert
            Assert.Equal(inputText, result);
            _mockVowelFilter.Verify(f => f.Apply(inputText), Times.Once);
            _mockLengthFilter.Verify(f => f.Apply(inputText), Times.Once);
            _mockLetterTFilter.Verify(f => f.Apply(inputText), Times.Once);
        }

        [Fact]
        public void ApplyFilters_HandlesNullInputGracefully()
        {
            // Arrange
            string inputText = null;

            _mockVowelFilter.Setup(f => f.Apply(inputText)).Returns(inputText);
            _mockLengthFilter.Setup(f => f.Apply(inputText)).Returns(inputText);
            _mockLetterTFilter.Setup(f => f.Apply(inputText)).Returns(inputText);

            var filters = new List<ITextFilterRepository>
            {
                _mockVowelFilter.Object,
                _mockLengthFilter.Object,
                _mockLetterTFilter.Object
            };

            var processor = new TextFilterProcessor(filters);

            // Act
            var result = processor.ApplyFilters(inputText);

            // Assert
            Assert.Null(result);
            _mockVowelFilter.Verify(f => f.Apply(inputText), Times.Once);
            _mockLengthFilter.Verify(f => f.Apply(inputText), Times.Once);
            _mockLetterTFilter.Verify(f => f.Apply(inputText), Times.Once);
        }

        [Fact]
        public void FileSettings_LoadsCorrectPath()
        {
            // Arrange
            var mockPath = "TestData/InputFile.txt";
            _mockFileSettings.SetupGet(fs => fs.InputFilePath).Returns(mockPath);

            // Act
            var filePath = _mockFileSettings.Object.InputFilePath;

            // Assert
            Assert.Equal(mockPath, filePath);
            _mockFileSettings.VerifyGet(fs => fs.InputFilePath, Times.Once);
        }

        [Fact]
        public void TextFilterProcessor_LogsInformationOnProcessing()
        {
            // Arrange
            var inputText = "test input text";
            var processedText = "processed text";

            _mockVowelFilter.Setup(f => f.Apply(inputText)).Returns(processedText);
            var filters = new List<ITextFilterRepository> { _mockVowelFilter.Object };

            var processor = new TextFilterProcessor(filters);

            // Act
            var result = processor.ApplyFilters(inputText);

            // Assert
            Assert.Equal(processedText, result);
            _mockVowelFilter.Verify(f => f.Apply(inputText), Times.Once);
        }
    }
}
