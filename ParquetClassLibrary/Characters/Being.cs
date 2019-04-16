using System;
using System.Collections.Generic;
using ParquetClassLibrary.Sandbox.IDs;
using ParquetClassLibrary.Utilities;

namespace ParquetClassLibrary.Characters
{
    /// <summary>
    /// Models the basic definitions shared by any in-game actor.
    /// </summary>
    public abstract class Being : Entity
    {
        #region Characteristics
        /// <summary>The <see cref="Biome"/> in which this character is at home.</summary>
        public Biome NativeBiome { get; set; }

        /// <summary>The <see cref="Behavior"/> governing the way this character acts.</summary>
        public Behavior PrimaryBehavior { get; set; }

        /// <summary>Types of parquets this critter avoids, if any.</summary>
        public readonly List<EntityID> Avoids = new List<EntityID>();

        /// <summary>Types of parquets this critter seeks out, if any.</summary>
        public readonly List<EntityID> Seeks = new List<EntityID>();
        #endregion

        #region Initialization
        /// <summary>
        /// Used by <see cref="Being"/> subtypes.
        /// </summary>
        /// <param name="in_bounds">
        /// The bounds within which the <see cref="Being"/>'s <see cref="EntityID"/> is defined.
        /// Must be one of <see cref="AssemblyInfo.BeingIDs"/>.
        /// </param>
        /// <param name="in_id">Unique identifier for the <see cref="Being"/>.  Cannot be null.</param>
        /// <param name="in_name">Player-friendly name of the <see cref="Being"/>.  Cannot be null or empty.</param>
        /// <param name="in_nativeBiome">The <see cref="Biome"/> in which this <see cref="Being"/> is most comfortable.</param>
        /// <param name="in_primaryBehavior">The rules that govern how this <see cref="Being"/> acts.  Cannot be null.</param>
        /// <param name="in_avoids">Any parquets this <see cref="Being"/> avoids.</param>
        /// <param name="in_seeks">Any parquets this <see cref="Being"/> seeks.</param>
        protected Being(Range<EntityID> in_bounds, EntityID in_id, string in_name, Biome in_nativeBiome,
                        Behavior in_primaryBehavior, List<EntityID> in_avoids = null, List<EntityID> in_seeks = null)
            : base(in_bounds, in_id, in_name)
        {
            if (!AssemblyInfo.BeingIDs.ContainsRange(in_bounds))
            {
                throw new ArgumentOutOfRangeException(nameof(in_bounds));
            }
            foreach (var parquetID in in_avoids)
            {
                if (!parquetID.IsValidForRange(AssemblyInfo.ParquetIDs))
                {
                    throw new ArgumentOutOfRangeException(nameof(in_avoids));
                }
            }
            foreach (var parquetID in in_seeks)
            {
                if (!parquetID.IsValidForRange(AssemblyInfo.ParquetIDs))
                {
                    throw new ArgumentOutOfRangeException(nameof(in_seeks));
                }
            }

            NativeBiome = in_nativeBiome;
            PrimaryBehavior = in_primaryBehavior;
            Avoids.AddRange(in_avoids);
            Seeks.AddRange(in_seeks);
        }
        #endregion
    }
}