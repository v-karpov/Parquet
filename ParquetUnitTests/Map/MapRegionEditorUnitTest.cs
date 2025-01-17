using ParquetClassLibrary.Map;
using ParquetClassLibrary.Parquets;
using ParquetClassLibrary.Stubs;
using Xunit;

namespace ParquetUnitTests.Map
{
    public class MapRegionEditorUnitTest
    {
        [Fact]
        public void EditorInstantiatesWithNoMapLoadedTest()
        {
            var editor = new MapRegionEditor();

            Assert.False(editor.IsMapLoaded);
        }

        [Fact]
        public void EditorHasLoadedMapAfterNewMapTest()
        {
            var editor = new MapRegionEditor();
            editor.NewMapRegion();

            Assert.True(editor.IsMapLoaded);
        }

        [Fact]
        public void DisplayInfoAtPositionRaisesPositionInfoEvent()
        {
            PositionInfoEvent eventArgument = null;
            object sender = null;
            var wasEventRaised = false;
            void HandleEvent(object in_sender, PositionInfoEvent in_args)
            {
                eventArgument = in_args;
                sender = in_sender;
                wasEventRaised = true;
            }

            var editor = new MapRegionEditor();
            editor.NewMapRegion();
            MapRegionEditor.DisplayPositionInfo += HandleEvent;

            editor.DisplayInfoAtPosition(Vector2Int.ZeroVector);

            Assert.True(wasEventRaised);
            Assert.Equal(editor, sender);
            Assert.NotNull(eventArgument);

            MapRegionEditor.DisplayPositionInfo -= HandleEvent;
        }

        [Theory]
        [InlineData(false, false, false, false)]
        [InlineData(true, false, false, false)]
        [InlineData(false, true, false, false)]
        [InlineData(false, false, true, false)]
        [InlineData(false, false, false, true)]
        [InlineData(true, true, true, true)]
        public void EditorParquetPatternCorrespondsToSetCommandsTest(bool in_setPaintFloor, bool in_setPaintBlock, bool in_setPaintFurnishing, bool in_setPaintCollectible)
        {
            var editor = new MapRegionEditor();

            editor.SetPaintFloor(in_setPaintFloor);
            editor.SetPaintBlock(in_setPaintBlock);
            editor.SetPaintFurnishing(in_setPaintFurnishing);
            editor.SetPaintCollectible(in_setPaintCollectible);

            Assert.Equal(in_setPaintFloor, editor.ParquetPaintPattern.HasFlag(ParquetMask.Floor));
            Assert.Equal(in_setPaintBlock, editor.ParquetPaintPattern.HasFlag(ParquetMask.Block));
            Assert.Equal(in_setPaintFurnishing, editor.ParquetPaintPattern.HasFlag(ParquetMask.Furnishing));
            Assert.Equal(in_setPaintCollectible, editor.ParquetPaintPattern.HasFlag(ParquetMask.Collectible));
        }
    }
}
