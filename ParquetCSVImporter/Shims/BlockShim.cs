﻿using ParquetClassLibrary.Sandbox.ID;
using ParquetClassLibrary.Sandbox.Parquets;

namespace ParquetCSVImporter.ClassMaps
{
    /// <summary>
    /// Provides a default public parameterless constructor for a
    /// <see cref="T:ParquetClassLibrary.Sandbox.Parquets.Block"/>-like
    /// class that CSVHelper can instantiate.
    /// 
    /// Provides the ability to generate a <see cref="T:ParquetClassLibrary.Sandbox.Parquets.Block"/>
    /// from this class.
    /// </summary>
    public class BlockShim : ParquetParentShim
    {
        /// <summary>The tool used to remove the block.</summary>
        public GatheringTools GatherTool;

        /// <summary>The block is flammable.</summary>
        public bool IsFlammable;

        /// <summary>The block is a liquid.</summary>
        public bool IsLiquid;

        /// <summary>The block's native toughness.</summary>
        public int MaxToughness;

        /// <summary>
        /// Converts a shim into the class is corresponds to.
        /// </summary>
        /// <typeparam name="T">The type to convert this shim to.</typeparam>
        /// <returns>An instance of a child class of <see cref="T:ParquetClassLibrary.Sandbox.Parquets.ParquetParent"/>.</returns>
        /// <exception cref="System.ArgumentException">
        /// Thrown when the current shim does not correspond to the specified type.
        /// </exception>
        public override T To<T>()
        {
            T result;

            if (typeof(T) == typeof(Block))
            {
                result = (T)(ParquetParent)new Block(ID, Name, AddsToBiome, GatherTool, IsFlammable, IsLiquid, MaxToughness);
            }
            else
            {
                throw new System.ArgumentException(nameof(T));
            }

            return result;
        }
    }
}