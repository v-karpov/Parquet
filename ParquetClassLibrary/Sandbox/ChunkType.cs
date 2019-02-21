namespace ParquetClassLibrary.Sandbox
{
    /// <summary>
    /// Indicates a the sorts of parquets to generate in a MapChunk.
    /// </summary>
    internal enum ChunkType
    {
        Empty,
        ScatteredClouds,
        SolidCloud,

        SolidWater,
        SolidEarth,
        SolidStone,
        SolidSandstone,
        SolidIce,
        SolidLava,
        SolidBrick,

        BeachWaterNorth,
        BeachWaterSouth,
        BeachWaterEast,
        BeachWaterWest,

        GrassyCanyon,
        EarthyCave,
        GrassyField,
        GrassyIsland,
        GrassyLake,
        GrassyPools,
        GrassyRiver,
        GrassyRoad,
        GrassyRuin,
        GrassyStones,

        IcyCanyon,
        IcyCave,
        IcyField,
        IcyIsland,
        IcyLake,
        IcyPools,
        IcyRiver,
        IcyFrozenLake,
        IcyFrozenPools,
        IcyFrozenRiver,
        IcyRoad,
        IcyRuin,
        IcyStones,

        SandyCanyon,
        SandyCave,
        SandyField,
        SandyIsland,
        SandyLake,
        SandyPools,
        SandyRiver,
        SandyRoad,
        SandyRuin,
        SandyStones,
        SandyVolcanicLake,

        Handmade,

        Done,
    }

    /// <summary>
    /// Convenience extension methods for concise coding when working with ChunkTypes instances.
    /// </summary>
    internal static class ChunkTypeExtensions
    {
        /// <summary>
        /// Indicates which Elevations are valid for this ChunkType.
        /// </summary>
        /// <param name="in_chunkType">The chunk to evaluate.</param>
        /// <returns>An elevation mask.</returns>
        public static ElevationMask ToElevation(this ref ChunkType in_chunkType)
        {
            var result = new ElevationMask();

            switch (in_chunkType)
            {
                case ChunkType.ScatteredClouds:
                case ChunkType.SolidCloud:
                    result.Set(ElevationMask.AboveGround);
                    break;

                case ChunkType.BeachWaterNorth:
                case ChunkType.BeachWaterSouth:
                case ChunkType.BeachWaterEast:
                case ChunkType.BeachWaterWest:
                case ChunkType.GrassyCanyon:
                case ChunkType.GrassyField:
                case ChunkType.GrassyIsland:
                case ChunkType.GrassyLake:
                case ChunkType.GrassyPools:
                case ChunkType.GrassyRiver:
                case ChunkType.GrassyRoad:
                case ChunkType.GrassyRuin:
                case ChunkType.GrassyStones:
                case ChunkType.IcyCanyon:
                case ChunkType.IcyField:
                case ChunkType.IcyIsland:
                case ChunkType.IcyLake:
                case ChunkType.IcyPools:
                case ChunkType.IcyRiver:
                case ChunkType.IcyFrozenLake:
                case ChunkType.IcyFrozenPools:
                case ChunkType.IcyFrozenRiver:
                case ChunkType.IcyRoad:
                case ChunkType.IcyRuin:
                case ChunkType.IcyStones:
                case ChunkType.SandyCanyon:
                case ChunkType.SandyField:
                case ChunkType.SandyIsland:
                case ChunkType.SandyLake:
                case ChunkType.SandyPools:
                case ChunkType.SandyRiver:
                case ChunkType.SandyRoad:
                case ChunkType.SandyStones:
                    result.Set(ElevationMask.LevelGround);
                    break;

                case ChunkType.EarthyCave:
                case ChunkType.IcyCave:
                case ChunkType.SandyCave:
                    result.Set(ElevationMask.BelowGround);
                    break;

                case ChunkType.SandyRuin:
                case ChunkType.SandyVolcanicLake:
                case ChunkType.SolidWater:
                case ChunkType.SolidEarth:
                case ChunkType.SolidStone:
                case ChunkType.SolidSandstone:
                case ChunkType.SolidIce:
                case ChunkType.SolidLava:
                case ChunkType.SolidBrick:
                    result.Set(ElevationMask.LevelGround);
                    result.Set(ElevationMask.BelowGround);
                    break;

                case ChunkType.Empty:
                case ChunkType.Handmade:
                case ChunkType.Done:
                    result.Set(ElevationMask.AboveGround);
                    result.Set(ElevationMask.LevelGround);
                    result.Set(ElevationMask.BelowGround);
                    break;
            }

            return result;
        }
    }
}
