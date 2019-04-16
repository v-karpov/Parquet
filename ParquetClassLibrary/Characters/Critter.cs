using System;
using System.Collections.Generic;
using ParquetClassLibrary.Sandbox.IDs;

namespace ParquetClassLibrary.Characters
{
    /// <summary>
    /// Models the definition for a simple in-game actor, such as a friendly mob with limited interaction.
    /// </summary>
    public sealed class Critter : Being
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Critter"/> class.
        /// </summary>
        /// <param name="in_id">
        /// Unique identifier for the <see cref="Critter"/>.  Cannot be null.
        /// Must be a <see cref="AssemblyInfo.CritterIDs"/>.
        /// </param>
        /// <param name="in_name">Player-friendly name of the <see cref="Critter"/>.  Cannot be null or empty.</param>
        /// <param name="in_nativeBiome">The <see cref="Biome"/> in which this <see cref="Critter"/> is most comfortable.</param>
        /// <param name="in_primaryBehavior">The rules that govern how this <see cref="Critter"/> acts.  Cannot be null.</param>
        /// <param name="in_avoids">Any parquets this <see cref="Critter"/> avoids.</param>
        /// <param name="in_seeks">Any parquets this <see cref="Critter"/> seeks.</param>
        public Critter(EntityID in_id, string in_name, Biome in_nativeBiome, Behavior in_primaryBehavior,
                       List<EntityID> in_avoids = null, List<EntityID> in_seeks = null)
            : base(AssemblyInfo.CritterIDs, in_id, in_name, in_nativeBiome, in_primaryBehavior, in_avoids, in_seeks)
        {
            if (!in_id.IsValidForRange(AssemblyInfo.CritterIDs))
            {
                throw new ArgumentOutOfRangeException(nameof(in_id));
            }
        }
    }
}