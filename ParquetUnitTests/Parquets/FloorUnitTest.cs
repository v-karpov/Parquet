using System;
using ParquetClassLibrary;
using ParquetClassLibrary.Parquets;
using Xunit;

namespace ParquetUnitTests.Parquets
{
    public class FloorUnitTest
    {
        #region Test Values
        /// <summary>Identifier used when creating a new floor.</summary>
        private static readonly EntityID newFloorID = TestEntities.TestFloor.ID - 1;
        #endregion

        [Fact]
        public void ValidFloorIDsArePermittedTest()
        {
            var testFloor = new Floor(newFloorID, "will be created", "", "");

            Assert.NotNull(testFloor);
        }

        [Fact]
        public void InvalidFloorIDsRaiseExceptionTest()
        {
            var badFloorID = TestEntities.TestBlock.ID;

            void TestCode()
            {
                var _ = new Collectible(badFloorID, "will fail", "", "");
            }

            Assert.Throws<ArgumentOutOfRangeException>(TestCode);
        }
    }
}
