using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using ParquetClassLibrary.Utilities;

namespace ParquetClassLibrary
{
    /// <summary>
    /// Stores an <see cref="GameObject"/> collection.
    /// Provides bounds-checking and type-checking against <typeparamref name="ParentType"/>.
    /// </summary>
    /// <remarks>
    /// This generic version is intended to support <see cref="All.Parquets"/> allowing
    /// the collection to store all parquet types but return only the requested subtype.
    /// </remarks>
    public class GameObjectCollection<ParentType> where ParentType : GameObject
    {
        /// <summary>A value to use in place of uninitialized <see cref="GameObjectCollection{T}"/>s.</summary>
        public static readonly GameObjectCollection<ParentType> Default = new GameObjectCollection<ParentType>(
            new List<Range<GameObjectID>> { new Range<GameObjectID>(int.MinValue, int.MaxValue) }, Enumerable.Empty<GameObject>());

        /// <summary>The internal collection mechanism.</summary>
        private IReadOnlyDictionary<GameObjectID, GameObject> Entities { get; }

        private List<Range<GameObjectID>> Bounds { get; }

        /// <summary>The number of <see cref="GameObject"/>s in the <see cref="GameObjectCollection{T}"/>.</summary>
        public int Count => Entities.Count;

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="GameObjectCollection{T}"/> class.
        /// </summary>
        /// <param name="in_bounds">The bounds within which the collected <see cref="GameObjectID"/>s are defined.</param>
        /// <param name="in_entities">The <see cref="GameObject"/>s to collect.  Cannot be null.</param>
        public GameObjectCollection(List<Range<GameObjectID>> in_bounds, IEnumerable<GameObject> in_entities)
        {
            Precondition.IsNotNull(in_entities, nameof(in_entities));

            var baseDictionary = new Dictionary<GameObjectID, GameObject> { { GameObjectID.None, null } };
            foreach (var entity in in_entities)
            {
                Precondition.IsInRange(entity.ID, in_bounds, nameof(in_entities));

                if (!baseDictionary.ContainsKey(entity.ID))
                {
                    baseDictionary[entity.ID] = entity;
                }
                else
                {
                    throw new InvalidOperationException($"Tried to duplicate entity ID {entity.ID}.");
                }
            }

            Bounds = in_bounds;
            Entities = baseDictionary;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameObjectCollection{T}"/> class.
        /// </summary>
        /// <param name="in_bounds">The bounds within which the collected <see cref="GameObjectID"/>s are defined.</param>
        /// <param name="in_serializedParquets">The serialized parquets.</param>
        public GameObjectCollection(List<Range<GameObjectID>> in_bounds, string in_serializedParquets)
        {
            Precondition.IsNotEmpty(in_serializedParquets, nameof(in_serializedParquets));

            // TODO: Ensure this is working as intended.  See:
            // https://stackoverflow.com/questions/6348215/how-to-deserialize-json-into-ienumerablebasetype-with-newtonsoft-json-net
            // https://www.newtonsoft.com/json/help/html/SerializeTypeNameHandling.htm

            Dictionary<GameObjectID, GameObject> baseCollection;
            try
            {
                baseCollection = JsonConvert.DeserializeObject<Dictionary<GameObjectID, GameObject>>(in_serializedParquets);
            }
            catch (JsonReaderException exception)
            {
                throw new System.Runtime.Serialization.SerializationException(
                    $"Error reading string while deserializing an {nameof(GameObject)} or {nameof(GameObjectID)}", exception);
            }

            Bounds = in_bounds;
            Entities = baseCollection;
        }
        #endregion

        #region Collection Access
        /// <summary>
        /// Determines whether the <see cref="GameObjectCollection{T}"/> contains the specified <see cref="GameObject"/>.
        /// </summary>
        /// <param name="in_entity">The <see cref="GameObject"/> to find.</param>
        /// <returns><c>true</c> if the <see cref="GameObject"/> was found; <c>false</c> otherwise.</returns>
        public bool Contains(GameObject in_entity)
        {
            return Entities.ContainsKey(in_entity.ID);
        }

        /// <summary>
        /// Determines whether the <see cref="GameObjectCollection{T}"/> contains the specified <see cref="GameObject"/>.
        /// </summary>
        /// <param name="in_id">The <see cref="GameObjectID"/> of the <see cref="GameObject"/> to find.</param>
        /// <returns><c>true</c> if the <see cref="GameObjectID"/> was found; <c>false</c> otherwise.</returns>
        /// <remarks>This method is equivalent to <see cref="Dictionary{EntityID, Entity}.ContainsKey"/>.</remarks>
        public bool Contains(GameObjectID in_id)
        {
            Precondition.IsInRange(in_id, Bounds, nameof(in_id));

            return Entities.ContainsKey(in_id);
        }

        /// <summary>
        /// Returns the specified <typeparamref name="T"/>.
        /// </summary>
        /// <param name="in_id">A valid, defined <typeparamref name="T"/> identifier.</param>
        /// <typeparam name="T">
        /// The type of <typeparamref name="ParentType"/> sought.  Must correspond to the given <paramref name="in_id"/>.
        /// </typeparam>
        /// <returns>The specified <typeparamref name="T"/>.</returns>
        public T Get<T>(GameObjectID in_id) where T : ParentType
        {
            Precondition.IsInRange(in_id, Bounds, nameof(in_id));

            return (T)Entities[in_id];
        }
        #endregion

        #region Utility Methods
        /// <summary>
        /// Serializes all defined parquets to a string.
        /// </summary>
        /// <returns>The serialized parquets.</returns>
        public string SerializeToString()
            => JsonConvert.SerializeObject(Entities, Formatting.None);

        /// <summary>
        /// Retrieves an enumerator for the <see cref="GameObjectCollection{T}"/>.
        /// </summary>
        /// <returns>An enumerator that iterates through the collection.</returns>
        public IEnumerator<GameObject> GetEnumerator()
            => Entities.Values.GetEnumerator();

        /// <summary>
        /// Returns a <see langword="string"/> that represents the current <see cref="GameObjectCollection{T}"/>.
        /// </summary>
        /// <returns>The representation.</returns>
        public override string ToString()
        {
            var allBoundNames = new StringBuilder();
            foreach (var bound in Bounds)
            {
                allBoundNames.Append(bound.ToString());
            }
            return $"Collects {typeof(ParentType)} over {allBoundNames}.";
        }
        #endregion
    }

    /// <summary>
    /// Stores an <see cref="GameObject"/> collection.
    /// Provides bounds-checking and type-checking against <see cref="GameObject"/>.
    /// </summary>
    /// <remarks>
    /// This version supports collections that do not rely heavily on
    /// multiple incompatible subclasses of <see cref="GameObject"/>.
    /// </remarks>
    public class GameObjectCollection : GameObjectCollection<GameObject>
    {
        /// <summary>A value to use in place of uninitialized <see cref="GameObjectCollection{T}"/>s.</summary>
        public static new readonly GameObjectCollection Default =
            new GameObjectCollection(new Range<GameObjectID>(int.MinValue, int.MaxValue), Enumerable.Empty<GameObject>());
            
        /// <summary>
        /// Initializes a new instance of the <see cref="GameObjectCollection"/> class.
        /// </summary>
        /// <param name="in_bounds">The bounds within which the collected <see cref="GameObjectID"/>s are defined.</param>
        /// <param name="in_entities">The <see cref="GameObject"/>s to collect.  Cannot be null.</param>
        public GameObjectCollection(Range<GameObjectID> in_bounds, IEnumerable<GameObject> in_entities)
            : base(new List<Range<GameObjectID>> { in_bounds }, in_entities) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameObjectCollection"/> class.
        /// </summary>
        /// <param name="in_bounds">The bounds within which the collected <see cref="GameObjectID"/>s are defined.</param>
        /// <param name="in_serializedParquets">The serialized parquets.</param>
        public GameObjectCollection(Range<GameObjectID> in_bounds, string in_serializedParquets)
            : base(new List<Range<GameObjectID>> { in_bounds }, in_serializedParquets) { }

        /// <summary>
        /// Returns the specified <see cref="GameObject"/>.
        /// </summary>
        /// <param name="in_id">A valid, defined <see cref="GameObject"/> identifier.</param>
        /// <returns>The specified <see cref="GameObject"/>.</returns>
        public GameObject Get(GameObjectID in_id)
        {
            return Get<GameObject>(in_id);
        }
    }
}
