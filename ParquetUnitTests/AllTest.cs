using System.Linq;
using ParquetClassLibrary;
using ParquetClassLibrary.Utilities;
using Xunit;

namespace ParquetUnitTests
{
    public class AllTest
    {
        #region Values for Tests
        /// <summary>
        /// The highest <see cref="Range{EntityID}.Maximum"/> defined in <see cref="All"/>
        /// except for <see cref="All.ItemIDs"/>.Maximum.
        /// </summary>
        public static readonly EntityID MaximumIDRangeUpperLimit = typeof(All).GetFields()
            .Where(fieldInfo => fieldInfo.FieldType.IsGenericType
                && fieldInfo.FieldType == typeof(Range<EntityID>)
                && fieldInfo.Name != nameof(All.ItemIDs))
            .Select(fieldInfo => fieldInfo.GetValue(null))
            .Cast<Range<EntityID>>()
            .Select(range => range.Maximum)
            .Max();
        #endregion

        [Fact]
        public void AllDimensionsAreGreaterThanZeroTest()
        {
            // ReSharper disable All
            var result = All.ParquetsPerChunkDimension > 0
                         && All.ChunksPerRegionDimension > 0
                         && All.ParquetsPerRegionDimension > 0
                         && All.PanelPatternWidth > 0
                         && All.PanelPatternHeight > 0;
            Assert.True(result);
        }

        [Fact]
        public void ItemIDMinimumIsGreaterThanMaximumDefinedRangeUpperBoundTest()
        {
            Assert.True(All.ItemIDs.Minimum > MaximumIDRangeUpperLimit);
        }
    }
}
