using ParquetClassLibrary.Parquets;
using Xunit;

namespace ParquetUnitTests.Parquets
{
    public class ParquetStatusUnitTest
    {
        #region Test Values
        private static readonly ParquetStack ValidStack =
            new ParquetStack(TestEntities.TestFloor.ID, TestEntities.TestBlock.ID, TestEntities.TestFurnishing.ID,
                             TestEntities.TestCollectible.ID);
        #endregion

        [Fact]
        public void ToughnessCannotBeSetBelowZeroTest()
        {
            var testStatus = new ParquetStatus(ValidStack);

            testStatus.Toughness = int.MinValue;

            Assert.Equal(Block.LowestPossibleToughness, testStatus.Toughness);
        }

        [Fact]
        public void ToughnessCannotBeAboveMaxToughnessTest()
        {
            var testStatus = new ParquetStatus(ValidStack);

            var priorToughness = testStatus.Toughness;

            testStatus.Toughness = int.MaxValue;

            Assert.Equal(priorToughness, testStatus.Toughness);
        }
    }
}
