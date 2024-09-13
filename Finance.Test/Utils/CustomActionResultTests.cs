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

    }
}