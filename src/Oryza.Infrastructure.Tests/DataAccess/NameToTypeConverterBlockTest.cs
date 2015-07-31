using Oryza.Infrastructure.DataAccess;
using Oryza.TestBase;
using Oryza.TestBase.Composition;
using Xunit;

namespace Oryza.Infrastructure.Tests.DataAccess
{
    public class NameToTypeConverterBlockTest : Test
    {
        [Theory]
        [InlineData("Long grain white rice - high quality", "LongGrainWhiteRiceHighQuality")]
        public void ConvertCategoryName_CategoryName_ReturnsCorrectCategoryTypeName(string categoryName, string expectedEntryTypeName)
        {
            // arrange
            var nameToTypeConverterBlock = _serviceProvider.GetService<NameToTypeConverterBlock>();

            // act
            var categoryTypeName = nameToTypeConverterBlock.Handle(categoryName);

            // assert
            Assert.Equal(expectedEntryTypeName, categoryTypeName);
        }
    }
}