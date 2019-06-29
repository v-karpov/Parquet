using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Newtonsoft.Json;
using ParquetClassLibrary.Utilities;

namespace ParquetClassLibrary
{
    /// <summary>
    /// Uniquely identifies every <see cref="GameObject"/>.
    /// </summary>
    /// <remarks>
    /// Multiple identicle parquet IDs may be assigned to MapChunks
    /// or MapRegions, and multiple duplicate item IDs may exist in
    /// the Inventory.  These IDs provide a means for the library to
    /// look up the game entity definition when other game elements
    /// interact with it.
    /// 
    /// To be clear: there are multiple entity subtypes (<see cref="Parquets.ParquetParent"/>,
    /// <see cref="Items.Item"/>, etc.), and each of these subtypes
    /// has multiple definitions.  The definitions are purely data-driven,
    /// read in from JSON or CSV files, and not type-checked by the compiler.
    /// 
    /// Although the compiler does not provide type-checking for
    /// IDs, within the scope of their usage the library defines
    /// valid ranges for and these are checked by library code.
    /// <see cref="ParquetClassLibrary.All"/>
    /// </remarks>
    /// TODO: Include this explanation in the Wiki.
    public struct GameObjectID : IComparable<GameObjectID>
    {
        /// <summary>Indicates the lack of an <see cref="GameObject"/>.</summary>
        public static readonly GameObjectID None = 0;

        /// <summary>Backing type for the <see cref="GameObjectID"/>.</summary>
        /// <remarks>
        /// This is implemented as an <see langword="int"/> rather than a <see cref="System.Guid"/>
        /// to support human-readable design documents and <see cref="Range{T}"/> validation.
        /// </remarks>
        [JsonProperty]
        private int _id;

        #region IComparable Methods
        /// <summary>
        /// Enables <see cref="GameObjectID"/>s to be treated as their backing type.
        /// </summary>
        /// <param name="in_value">Any valid identifier value.</param>
        /// <returns>The given identifier value.</returns>
        public static implicit operator GameObjectID(int in_value)
        {
            return new GameObjectID { _id = in_value };
        }

        /// <summary>
        /// Enables <see cref="GameObjectID"/> to be treated as their backing type.
        /// </summary>
        /// <param name="in_identifier">Any valid identifier value.</param>
        /// <returns>The given identifier value.</returns>
        public static implicit operator int(GameObjectID in_identifier)
        {
            return in_identifier._id;
        }

        /// <summary>
        /// Enables <see cref="GameObjectID"/> to be compared one another.
        /// </summary>
        /// <param name="in_identifier">Any valid <see cref="GameObjectID"/> value.</param>
        /// <returns>
        /// A value indicating the relative ordering of the <see cref="GameObjectID"/>s being compared.
        /// The return value has these meanings:
        /// Less than zero indicates that the current instance precedes the given <see cref="GameObjectID"/> in the sort order.
        /// Zero indicates that the current instance occurs in the same position in the sort order as the given <see cref="GameObjectID"/>.
        /// Greater than zero indicates that the current instance follows the given <see cref="GameObjectID"/> in the sort order.
        /// </returns>
        public int CompareTo(GameObjectID in_identifier)
        {
            return _id.CompareTo(in_identifier._id);
        }
        #endregion

        #region Utility Methods
        /// <summary>
        /// Validates the current <see cref="GameObjectID"/> over a <see cref="Range{EntityID}"/>.
        /// An <see cref="GameObjectID"/> is valid if:
        /// 1) it is <see cref="None"/>
        /// 2) it is defined within the given <see cref="Range{T}"/>, regardless of sign.
        /// </summary>
        /// <param name="in_range">The <see cref="Range{T}"/> within which the absolute value of the <see cref="GameObjectID"/> must fall.</param>
        /// <returns>
        /// <c>true</c>, if the <see cref="GameObjectID"/> is valid given the <see cref="Range{T}"/>, <c>false</c> otherwise.
        /// </returns>
        [Pure]
        public bool IsValidForRange(Range<GameObjectID> in_range)
        {
            return _id == None || in_range.ContainsValue(Math.Abs(_id));
        }

        /// <summary>
        /// Validates the current <see cref="GameObjectID"/> over a <see cref="IEnumerable{Range{GameObjectID}}"/>.
        /// An <see cref="GameObjectID"/> is valid if:
        /// 1) it is <see cref="None"/>
        /// 2) it is defined within any of the <see cref="Range{T}"/> in the given <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="in_ranges">
        /// The <see cref="IEnumerable{Range{GameObjectID}}"/> within which the <see cref="GameObjectID"/> must fall.
        /// </param>
        /// <returns>
        /// <c>true</c>, if the <see cref="GameObjectID"/> is valid given the <see cref="Range{T}"/>, <c>false</c> otherwise.
        /// </returns>
        [Pure]
        public bool IsValidForRange(IEnumerable<Range<GameObjectID>> in_ranges)
        {
            var result = false;

            foreach (var idRange in in_ranges)
            {
                if (IsValidForRange(idRange))
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Returns a <see langword="string"/> that represents the current <see cref="GameObjectID"/>.
        /// </summary>
        /// <returns>The representation.</returns>
        public override string ToString()
            => _id.ToString();
        #endregion
    }
}
