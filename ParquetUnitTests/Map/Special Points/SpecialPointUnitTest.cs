using ParquetClassLibrary.Map.SpecialPoints;
using ParquetClassLibrary.Stubs;
using Xunit;

namespace ParquetUnitTests.Map.SpecialPoints
{
    public class SpecialPointUnitTest
    {
        private static readonly Vector2Int testPosition = new Vector2Int(2, 2);

        [Fact]
        public void SpecialPointsEquateIfTheirPositionsEquateTest()
        {
            var point1 = new SpecialPoint(testPosition);
            var point2 = new SpecialPoint(testPosition);

            Assert.Equal(point1, point2);
        }

        [Fact]
        public void SpecialPointsDoNotEquateIfTheirPositionsDoNotEquateTest()
        {
            var point1 = new SpecialPoint(testPosition);
            var point2 = new SpecialPoint(Vector2Int.ZeroVector);

            Assert.NotEqual(point1, point2);
        }
    }
}
