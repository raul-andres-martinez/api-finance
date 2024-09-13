using Finance.Domain.Utils.Result;
using System.Net;
using Xunit;

namespace Finance.Test.Utils
{
    public class CustomActionResultTests
    {
        [Fact]
        public void NoContent_WhenTypeIsList_ShouldReturnEmptyList()
        {
            // Arrange
            var result = CustomActionResult<List<int>>.NoContent();

            // Act
            var value = result.Value;

            // Assert
            Assert.NotNull(value);
            Assert.IsType<List<int>>(value);
            Assert.Empty(value);
            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Fact]
        public void NoContent_WhenTypeIsInt_ShouldReturnDefaultInt()
        {
            // Arrange
            var result = CustomActionResult<int>.NoContent();

            // Act
            var value = result.Value;

            // Assert
            Assert.Equal(0, value);
            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Fact]
        public void Created_ShouldReturnCreatedResult()
        {
            // Arrange & Act
            var result = CustomActionResult.Created();

            // Assert
            Assert.True(result.Success);
            Assert.Null(result.Error);
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        }

        [Fact]
        public void ImplicitConversion_FromCustomError_ShouldCreateErrorResult()
        {
            // Arrange
            var error = new CustomError(HttpStatusCode.BadRequest, "Test.Error", "Test error");

            // Act
            CustomActionResult result = error;

            // Assert
            Assert.False(result.Success);
            Assert.Equal(error, result.Error);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public void ImplicitConversion_FromValue_ShouldCreateSuccessResult()
        {
            // Arrange
            int value = 42;

            // Act
            CustomActionResult<int> result = value;

            // Assert
            Assert.True(result.Success);
            Assert.Null(result.Error);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(value, result.Value);
        }

        [Fact]
        public void GetError_WhenSuccessful_ShouldThrowException()
        {
            // Arrange
            var result = CustomActionResult.Created();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => result.GetError());
        }

        [Fact]
        public void GetValue_WhenUnsuccessful_ShouldThrowException()
        {
            // Arrange
            var error = new CustomError(HttpStatusCode.BadRequest, "Test.Error", "Test error");
            CustomActionResult<int> result = error;

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => result.GetValue());
        }
    }
}