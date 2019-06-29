using System;
using Newtonsoft.Json;

namespace ParquetClassLibrary
{
    /// <summary>
    /// Identifies functional characteristics of <see cref="GameObject"/>s,
    /// such as their role in <see cref="Crafting.CraftingRecipe"/>s or
    /// <see cref="Biomes.Biome"/>s.
    /// </summary>
    /// <remarks>
    /// The intent is that definitional narrative and mechanical features
    /// of each game <see cref="GameObject"/> be taggable.
    /// This means that more than one <see cref="GameObjectTag"/> can coexist
    /// on a specific Entity within the same category.
    /// This allows for flexible definition of Entities such that a loose
    /// category of entities may answer a particular functional need; e.g.,
    /// "any parquet that has the Volcanic tag" or "any item that is a Key".
    /// </remarks>
    /// TODO: Include this explanation in the Wiki.
    public struct GameObjectTag : IComparable<GameObjectTag>
    {
        /// <summary>Indicates the lack of any <see cref="GameObjectTag"/>s.</summary>
        public static readonly GameObjectTag None = string.Empty;

        /// <summary>Backing type for the <see cref="GameObjectTag"/>.</summary>
        [JsonProperty]
        private string _tagName;

        #region IComparable Methods
        /// <summary>
        /// Enables <see cref="GameObjectTag"/>s to be treated as their backing type.
        /// </summary>
        /// <param name="in_value">Any valid tag value.</param>
        /// <returns>The given value as a tag.</returns>
        public static implicit operator GameObjectTag(string in_value)
        {
            return new GameObjectTag { _tagName = in_value };
        }

        /// <summary>
        /// Enables <see cref="GameObjectTag"/>s to be treated as their backing type.
        /// </summary>
        /// <param name="in_tag">Any valid tag value.</param>
        /// <returns>The given value as a tag.</returns>
        public static implicit operator string(GameObjectTag in_tag)
        {
            return in_tag._tagName;
        }

        /// <summary>
        /// Enables <see cref="GameObjectTag"/>s to be compared one another.
        /// </summary>
        /// <param name="in_tag">Any valid <see cref="GameObjectTag"/>.</param>
        /// <returns>
        /// A value indicating the relative ordering of the <see cref="GameObjectTag"/>s being compared.
        /// The return value has these meanings:
        /// Less than zero indicates that the current instance precedes the given <see cref="GameObjectTag"/> in the sort order.
        /// Zero indicates that the current instance occurs in the same position in the sort order as the given <see cref="GameObjectTag"/>.
        /// Greater than zero indicates that the current instance follows the given <see cref="GameObjectTag"/> in the sort order.
        /// </returns>
        public int CompareTo(GameObjectTag in_tag)
        {
            return string.Compare(_tagName, in_tag._tagName, StringComparison.Ordinal);
        }
        #endregion

        #region Utility Methods
        /// <summary>
        /// Returns a <see langword="string"/> that represents the current <see cref="GameObjectTag"/>.
        /// </summary>
        /// <returns>The representation.</returns>
        public override string ToString()
            => _tagName;
        #endregion
    }
}
