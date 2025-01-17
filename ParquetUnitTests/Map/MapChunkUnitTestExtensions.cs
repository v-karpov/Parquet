﻿using ParquetClassLibrary.Map;
using ParquetClassLibrary.Stubs;

namespace ParquetUnitTests.Map
{
    /// <summary>
    /// Provides extension methods of <see cref="MapChunk"/> used in unit testing.
    /// </summary>
    internal static class MapChunkUnitTestExtensions
    {
        /// <summary>Fills the chunk with a test pattern.</summary>
        public static MapChunk FillTestPattern(this MapChunk in_mapChunk)
        {
            for (var y = 0; y < in_mapChunk.DimensionsInParquets.Y; y++)
            {
                for (var x = 0; x < in_mapChunk.DimensionsInParquets.X; x++)
                {
                    in_mapChunk.TrySetFloorDefinition(TestEntities.TestFloor.ID, new Vector2Int(x, y));
                }

                in_mapChunk.TrySetBlockDefinition(TestEntities.TestBlock.ID, new Vector2Int(0, y));
                in_mapChunk.TrySetBlockDefinition(TestEntities.TestBlock.ID, new Vector2Int(in_mapChunk.DimensionsInParquets.X - 1, y));
            }
            for (var x = 0; x < in_mapChunk.DimensionsInParquets.X; x++)
            {
                in_mapChunk.TrySetBlockDefinition(TestEntities.TestBlock.ID, new Vector2Int(x, 0));
                in_mapChunk.TrySetBlockDefinition(TestEntities.TestBlock.ID, new Vector2Int(x, in_mapChunk.DimensionsInParquets.Y - 1));
            }
            in_mapChunk.TrySetFurnishingDefinition(TestEntities.TestFurnishing.ID, new Vector2Int(1, 2));
            in_mapChunk.TrySetCollectibleDefinition(TestEntities.TestCollectible.ID, new Vector2Int(3, 3));

            return in_mapChunk;
        }
    }
}
