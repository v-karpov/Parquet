using System;
using ParquetClassLibrary;
using ParquetClassLibrary.Biomes;
using ParquetClassLibrary.Characters;
using Xunit;

namespace ParquetUnitTests.Characters
{
    public class CritterUnitTest
    {
        #region Test Values
        /// <summary>Identifier used when creating a new block.</summary>
        private static readonly EntityID newCritterID = TestEntities.TestCritter.ID - 1;
        #endregion

        [Fact]
        public void ValidCritterIDsArePermittedTest()
        {
            var newCritter = new Critter(newCritterID, "will be created", "", "",
                                         All.BiomeIDs.Minimum, Behavior.Still);

            Assert.NotNull(newCritter);
        }

        [Fact]
        public void InvalidCritterIDsRaiseExceptionTest()
        {
            var badCritterID = TestEntities.TestBlock.ID - 1;

            void TestCode()
            {
                var _ = new Critter(badCritterID, "will fail", "", "",
                                    All.BiomeIDs.Minimum, Behavior.Still);
            }

            Assert.Throws<ArgumentOutOfRangeException>(TestCode);
        }
    }
}
