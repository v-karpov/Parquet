using System;
using Newtonsoft.Json;
using ParquetClassLibrary.Utilities;

namespace ParquetClassLibrary
{
    /// <summary>
    /// Models a unique, identifiable type of game object.
    /// </summary>
    /// <remarks>
    /// All <see cref="GameObject"/>s are identified with an <see cref="GameObjectID"/>,
    /// and are considered equal if and only if their respective EntityIDs are equal.
    /// 
    /// Entity is intended to model the parts of a game object that do not change from one
    /// instance to another.  In this sense, it can be thought of as analagous to a <see langword="class"/>.
    /// Individual game objects are represented and referenced as instances of <see cref="GameObjectID"/>
    /// within collections in other classes.  Their definitions are found by submitting their EntityID
    /// to the appropriate <see cref="GameObjectCollection{T}"/>.
    /// 
    /// If individual game objects must have mutable state then a separate wrapper class,
    /// such as <see cref="Parquets.ParquetStatus"/>, models that state.
    /// </remarks>
    /// <seealso cref="Parquets.ParquetStatus"/>
    /// <seealso cref="Items.Item"/>
    public abstract class GameObject : IEquatable<GameObject>
    {
        /// <summary>Game-wide unique identifier.</summary>
        [JsonProperty(PropertyName = "in_ID")]
        public GameObjectID ID { get; }

        /// <summary>Player-facing name.</summary>
        [JsonProperty(PropertyName = "in_name")]
        public string Name { get; }

        /// <summary>Player-facing description.</summary>
        [JsonProperty(PropertyName = "in_description")]
        public string Description { get; }

        /// <summary>Optional comment.</summary>
        /// <remarks>
        /// Could be used for designer notes or to implement an in-game dialogue
        /// with or on the <see cref="GameObject"/>.
        /// </remarks>
        [JsonProperty(PropertyName = "in_comment")]
        public string Comment { get; }

        #region Initialization
        /// <summary>
        /// Initializes a new instance of concrete implementations of the <see cref="GameObject"/> class.
        /// </summary>
        /// <param name="in_bounds">The bounds within which the derived type's <see cref="GameObjectID"/> is defined.</param>
        /// <param name="in_id">Unique identifier for the <see cref="GameObject"/>.  Cannot be null.</param>
        /// <param name="in_name">Player-friendly name of the <see cref="GameObject"/>.  Cannot be null or empty.</param>
        /// <param name="in_description">Player-friendly description of the <see cref="GameObject"/>.</param>
        /// <param name="in_comment">Comment of, on, or by the <see cref="GameObject"/>.</param>
        [JsonConstructor]
        protected GameObject(Range<GameObjectID> in_bounds, GameObjectID in_id, string in_name, string in_description, string in_comment)
        {
            Precondition.IsInRange(in_id, in_bounds, nameof(in_id));
            Precondition.IsNotEmpty(in_name, nameof(in_name));

            ID = in_id;
            Name = in_name;
            Description = in_description ?? "";
            Comment = in_comment ?? "";
        }
        #endregion

        #region IEquatable Implementation
        /// <summary>
        /// Serves as a hash function for an <see cref="GameObject"/>.
        /// </summary>
        /// <returns>
        /// A hash code for this instance that is suitable for use in hashing algorithms and data structures.
        /// </returns>
        public override int GetHashCode()
            => ID.GetHashCode();

        /// <summary>
        /// Determines whether the specified <see cref="GameObject"/> is equal to the current <see cref="GameObject"/>.
        /// </summary>
        /// <param name="in_entity">The <see cref="GameObject"/> to compare with the current.</param>
        /// <returns><c>true</c> if they are equal; otherwise, <c>false</c>.</returns>
        public bool Equals(GameObject in_entity)
            => null != in_entity && ID == in_entity.ID;

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="GameObject"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="GameObject"/>.</param>
        /// <returns><c>true</c> if they are equal; otherwise, <c>false</c>.</returns>
        // ReSharper disable once InconsistentNaming
        public override bool Equals(object obj)
            => obj is GameObject entity && Equals(entity);

        /// <summary>
        /// Determines whether a specified instance of <see cref="GameObject"/> is equal to another specified instance of <see cref="GameObject"/>.
        /// </summary>
        /// <param name="in_entity1">The first <see cref="GameObject"/> to compare.</param>
        /// <param name="in_entity2">The second <see cref="GameObject"/> to compare.</param>
        /// <returns><c>true</c> if they are equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(GameObject in_entity1, GameObject in_entity2)
            => (in_entity1 is null && in_entity2 is null)
            || (!(in_entity1 is null) && !(in_entity2 is null) && in_entity1.ID == in_entity2.ID);

        /// <summary>
        /// Determines whether a specified instance of <see cref="GameObject"/> is not equal to another specified instance of <see cref="GameObject"/>.
        /// </summary>
        /// <param name="in_entity1">The first <see cref="GameObject"/> to compare.</param>
        /// <param name="in_entity2">The second <see cref="GameObject"/> to compare.</param>
        /// <returns><c>true</c> if they are NOT equal; otherwise, <c>false</c>.</returns>
        public static bool operator !=(GameObject in_entity1, GameObject in_entity2)
            => (!(in_entity1 is null) && !(in_entity2 is null) && in_entity1.ID != in_entity2.ID)
            || (!(in_entity1 is null) && in_entity2 is null)
            || (in_entity1 is null && !(in_entity2 is null));
        #endregion

        #region Utility Methods
        /// <summary>
        /// Returns a <see langword="string"/> that represents the current <see cref="GameObject"/>.
        /// </summary>
        /// <returns>The representation.</returns>
        public override string ToString()
            => Name;
        #endregion
    }
}
