using System;
using System.Collections.Generic;
using System.Linq;
using ParquetClassLibrary.Characters;
using ParquetClassLibrary.Crafting;
using ParquetClassLibrary.Items;
using ParquetClassLibrary.Parquets;
using ParquetClassLibrary.Rooms;
using ParquetClassLibrary.Utilities;

namespace ParquetClassLibrary
{
    /// <summary>
    /// Provides content, rules, and parameters for the game.
    /// </summary>
    public static class All
    {
        #region EntityID Ranges
        /// <summary>
        /// A subset of the values of <see cref="GameObjectID"/> set aside for <see cref="Characters.PlayerCharacter"/>s.
        /// Valid identifiers may be positive or negative.  By convention, negative IDs indicate test Items.
        /// </summary>
        public static readonly Range<GameObjectID> PlayerCharacterIDs;

        /// <summary>
        /// A subset of the values of <see cref="GameObjectID"/> set aside for <see cref="Characters.Critter"/>s.
        /// Valid identifiers may be positive or negative.  By convention, negative IDs indicate test Items.
        /// </summary>
        public static readonly Range<GameObjectID> CritterIDs;

        /// <summary>
        /// A subset of the values of <see cref="GameObjectID"/> set aside for <see cref="Characters.Being"/>s.
        /// Valid identifiers may be positive or negative.  By convention, negative IDs indicate test Items.
        /// </summary>
        public static readonly Range<GameObjectID> NpcIDs;

        /// <summary>
        /// A collection containing all defined <see cref="Range{EntityID}"/>s of <see cref="Characters.Being"/>s.
        /// </summary>
        public static readonly List<Range<GameObjectID>> BeingIDs;

        /// <summary>
        /// A subset of the values of <see cref="GameObjectID"/> set aside for <see cref="Floor"/>s.
        /// Valid identifiers may be positive or negative.  By convention, negative IDs indicate test parquets.
        /// </summary>
        public static readonly Range<GameObjectID> FloorIDs;

        /// <summary>
        /// A subset of the values of <see cref="GameObjectID"/> set aside for <see cref="Block"/>s.
        /// Valid identifiers may be positive or negative.  By convention, negative IDs indicate test parquets.
        /// </summary>
        public static readonly Range<GameObjectID> BlockIDs;

        /// <summary>
        /// A subset of the values of <see cref="GameObjectID"/> set aside for <see cref="Furnishing"/>s.
        /// Valid identifiers may be positive or negative.  By convention, negative IDs indicate test parquets.
        /// </summary>
        public static readonly Range<GameObjectID> FurnishingIDs;

        /// <summary>
        /// A subset of the values of <see cref="GameObjectID"/> set aside for <see cref="Collectible"/>s.
        /// Valid identifiers may be positive or negative.  By convention, negative IDs indicate test parquets.
        /// </summary>
        public static readonly Range<GameObjectID> CollectibleIDs;

        /// <summary>
        /// A collection containing all defined <see cref="Range{EntityID}"/>s of parquet types.
        /// </summary>
        public static readonly List<Range<GameObjectID>> ParquetIDs;

        /// <summary>
        /// A subset of the values of <see cref="GameObjectID"/> set aside for <see cref="RoomRecipe"/>s.
        /// Valid identifiers may be positive or negative.  By convention, negative IDs indicate test Items.
        /// </summary>
        public static readonly Range<GameObjectID> RoomRecipeIDs;

        /// <summary>
        /// A subset of the values of <see cref="GameObjectID"/> set aside for <see cref="CraftingRecipe"/>s.
        /// Valid identifiers may be positive or negative.  By convention, negative IDs indicate test Items.
        /// </summary>
        public static readonly Range<GameObjectID> CraftingRecipeIDs;

        /// <summary>
        /// A subset of the values of <see cref="GameObjectID"/> set aside for <see cref="Quests.Quest"/>s.
        /// Valid identifiers may be positive or negative.  By convention, negative IDs indicate test Items.
        /// </summary>
        public static readonly Range<GameObjectID> QuestIDs;

        /// <summary>
        /// A subset of the values of <see cref="GameObjectID"/> set aside for <see cref="Biomes.Biome"/>s.
        /// Valid identifiers may be positive or negative.  By convention, negative IDs indicate test Items.
        /// </summary>
        public static readonly Range<GameObjectID> BiomeIDs;

        /// <summary>
        /// A subset of the values of <see cref="GameObjectID"/> set aside for <see cref="Items.Item"/>s.
        /// Valid identifiers may be positive or negative.  By convention, negative IDs indicate test Items.
        /// </summary>
        public static readonly Range<GameObjectID> ItemIDs;
        #endregion

        #region EntityCollections
        /// <summary><c>true</c> if the collections have been initialized; otherwise, <c>false</c>.</summary>
        private static bool _collectionsHaveBeenInitialized;

        /// <summary>
        /// A collection of all defined <see cref="Being"/>s.
        /// This collection is the source of truth about mobs and characters for the rest of the library,
        /// something like a color palette that other classes can paint with.
        /// </summary>
        /// <remarks>All <see cref="GameObjectID"/>s must be unique.</remarks>
        public static GameObjectCollection<Being> Beings { get; }

        /// <summary>
        /// A collection of all defined <see cref="CraftingRecipe"/>s.
        /// This collection is the source of truth about crafting for the rest of the library,
        /// something like a color palette that other classes can paint with.
        /// </summary>
        /// <remarks>All <see cref="GameObjectID"/>s must be unique.</remarks>
        public static GameObjectCollection CraftingRecipes { get; }

        /// <summary>
        /// A collection of all defined <see cref="Item"/>s.
        /// This collection is the source of truth about items for the rest of the library,
        /// something like a color palette that other classes can paint with.
        /// </summary>
        /// <remarks>All <see cref="GameObjectID"/>s must be unique.</remarks>
        public static GameObjectCollection Items { get; }

        /// <summary>
        /// A collection of all defined parquets of all subtypes.
        /// This collection is the source of truth about parquets for the rest of the library,
        /// something like a color palette that other classes can paint with.
        /// </summary>
        /// <remarks>All <see cref="GameObjectID"/>s must be unique.</remarks>
        public static GameObjectCollection<ParquetParent> Parquets { get; private set; }

        /// <summary>
        /// A collection of all defined <see cref="RoomRecipe"/>s.
        /// This collection is the source of truth about crafting for the rest of the library,
        /// something like a color palette that other classes can paint with.
        /// </summary>
        /// <remarks>All <see cref="GameObjectID"/>s must be unique.</remarks>
        public static GameObjectCollection RoomRecipes { get; }
        #endregion

        #region Rules and Parameters
        /// <summary>
        /// Provides dimensional parameters for the game.
        /// </summary>
        public static class Dimensions
        {
            /// <summary>The length of each <see cref="Map.MapChunkGrid"/> dimension in parquets.</summary>
            public const int ParquetsPerChunk = 16;

            /// <summary>The length of each <see cref="Map.MapRegion"/> dimension in <see cref="Map.MapChunkGrid"/>s.</summary>
            public const int ChunksPerRegion = 4;

            /// <summary>The length of each <see cref="Map.MapRegion"/> dimension in parquets.</summary>
            public const int ParquetsPerRegion = ChunksPerRegion * ParquetsPerChunk;

            /// <summary>Width of the <see cref="Crafting.StrikePanel"/> pattern in <see cref="Crafting.CraftingRecipe"/>.</summary>
            public const int PanelsPerPatternWidth = 2;

            /// <summary>Height of the <see cref="Crafting.StrikePanel"/> pattern in <see cref="Crafting.CraftingRecipe"/>.</summary>
            public const int PanelsPerPatternHeight = 8;
        }

        /// <summary>
        /// Provides recipe requirements for the game.
        /// </summary>
        public static class Recipes
        {
            // TODO Add class for crafting rules here.

            /// <summary>
            /// Provides recipe requirements for the game.
            /// </summary>
            public static class Rooms
            {
                /// <summary>
                /// Maximum number of open walkable spaces needed for any room to register.
                /// </summary>
                public const int MinWalkableSpaces = 4;

                /// <summary>
                /// Minimum number of open walkable spaces needed for any room to register.
                /// </summary>
                public const int MaxWalkableSpaces = 121;

                /// <summary>
                /// Finds the <see cref="GameObjectID"/> of the <see cref="RoomRecipe"/> that best matches the given <see cref="Room"/>.
                /// </summary>
                /// <param name="in_room">The <see cref="Room"/> to match.</param>
                /// <returns>The best match's <see cref="GameObjectID"/>.</returns>
                public static GameObjectID FindBestMatch(Room in_room)
                {
                    var matches = new List<RoomRecipe>();

                    foreach (RoomRecipe recipe in RoomRecipes)
                    {
                        if (recipe.Matches(in_room))
                        {
                            matches.Add(recipe);
                        }
                    }

                    return matches.Count > 0
                        ? (GameObjectID)matches.Select(recipe => recipe.Priority).DefaultIfEmpty(GameObjectID.None).Max()
                        : GameObjectID.None;
                }
            }
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes the <see cref="Range{EntityID}"/>s and <see cref="GameObjectCollection{T}"/>s defined in <see cref="All"/>.
        /// </summary>
        /// <remarks>
        /// This supports defining ItemIDs in terms of the other Ranges.
        /// </remarks>
        static All()
        {
            #region Default Values for Enitity Collections
            _collectionsHaveBeenInitialized = false;
            Beings = GameObjectCollection<Being>.Default;
            CraftingRecipes = GameObjectCollection.Default;
            Items = GameObjectCollection.Default;
            Parquets = GameObjectCollection<ParquetParent>.Default;
            RoomRecipes = GameObjectCollection.Default;
            #endregion

            #region Initialize Ranges
            // By convention, the first EntityID in each Range is a multiple of this number.
            // An exception is made for PlayerCharacters as these values are undefined at designtime.
            var TargetMultiple = 10000;

            #region Define Ranges
            PlayerCharacterIDs = new Range<GameObjectID>(1, 9999);
            CritterIDs = new Range<GameObjectID>(10000, 19000);
            NpcIDs = new Range<GameObjectID>(20000, 29000);

            FloorIDs = new Range<GameObjectID>(30000, 39000);
            BlockIDs = new Range<GameObjectID>(40000, 49000);
            FurnishingIDs = new Range<GameObjectID>(50000, 59000);
            CollectibleIDs = new Range<GameObjectID>(60000, 69000);

            RoomRecipeIDs = new Range<GameObjectID>(70000, 79000);
            CraftingRecipeIDs = new Range<GameObjectID>(80000, 89000);

            QuestIDs = new Range<GameObjectID>(90000, 99000);

            BiomeIDs = new Range<GameObjectID>(100000, 109000);
            #endregion

            #region Define Range Collections
            BeingIDs = new List<Range<GameObjectID>> { PlayerCharacterIDs, CritterIDs, NpcIDs };
            ParquetIDs = new List<Range<GameObjectID>> { FloorIDs, BlockIDs, FurnishingIDs, CollectibleIDs };
            #endregion

            // The largest Range.Maximum defined in AssemblyInfo, excluding ItemIDs.
            int MaximumIDNotCountingItems = typeof(All).GetFields()
                .Where(fieldInfo => fieldInfo.FieldType.IsGenericType
                    && fieldInfo.FieldType == typeof(Range<GameObjectID>)
                    && fieldInfo.Name != nameof(ItemIDs))
                .Select(fieldInfo => fieldInfo.GetValue(null))
                .Cast<Range<GameObjectID>>()
                .Select(range => range.Maximum)
                .Max();

            // Since ItemIDs is being defined at runtime, its Range.Minimum must be chosen well above existing maxima.
            var ItemLowerBound = TargetMultiple * ((MaximumIDNotCountingItems + (TargetMultiple - 1)) / TargetMultiple);

            // The largest Range.Maximum of any parquet IDs.
            int MaximumParquetID = ParquetIDs
                .Select(range => range.Maximum)
                .Max();

            // The smallest Range.Minimum of any parquet IDs.
            int MinimumParquetID = ParquetIDs
                .Select(range => range.Minimum)
                .Min();

            // Since it is possible for every parquet to have a corresponding item, this range must be at least
            // as large as all four parquet ranges put together.  Therefore, the Range.Maximum is twice the combined
            // ranges of all parquets.
            var ItemUpperBound = ItemLowerBound + 2 * (TargetMultiple / 10 + MaximumParquetID - MinimumParquetID);

            ItemIDs = new Range<GameObjectID>(ItemLowerBound, ItemUpperBound);
            #endregion
        }

        /// <summary>
        /// Initializes the <see cref="GameObjectCollection{T}s"/> from the given collections.
        /// </summary>
        /// <param name="in_parquets">All parquets to be used in the game.</param>
        /// <remarks>This initialization routine may be called only once per library execution.</remarks>
        /// <exception cref="InvalidOperationException">When called more than once.</exception>
        // TODO Make an version that takes serialized strings instead of ienumerables.
        public static void InitializeCollections(IEnumerable<ParquetParent> in_parquets)
        {
            if (_collectionsHaveBeenInitialized)
            {
                throw new InvalidOperationException($"Attempted to reinitialize {typeof(All)}.");
            }
            Precondition.IsNotNull(in_parquets, nameof(in_parquets));

            // TODO Uncomment these once we have CSV import implemented for non-parquets.
            //Beings = new EntityCollection<Being>(BeingIDs);
            //CraftingRecipes = new EntityCollection(CraftingRecipeIDs);
            //Items = new EntityCollection(ItemIDs);
            Parquets = new GameObjectCollection<ParquetParent>(ParquetIDs, in_parquets);
            //RoomRecipes = new EntityCollection(RoomRecipeIDs);

            _collectionsHaveBeenInitialized = true;
        }
        #endregion
    }
}
