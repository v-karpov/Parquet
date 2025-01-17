using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using ParquetClassLibrary.Crafting;
using ParquetClassLibrary.Utilities;

namespace ParquetClassLibrary.Items
{
    /// <summary>
    /// Models an item that characters may carry, use, equip, trade, and/or build with.
    /// </summary>
    public class Item : GameObject
    {
        /// <summary>The type of item this is.</summary>
        [JsonProperty(PropertyName = "in_subtype")]
        public ItemType Subtype { get; }

        /// <summary>In-game value of the item.  Must be non-negative.</summary>
        [JsonProperty(PropertyName = "in_price")]
        public int Price { get; }

        /// <summary>How relatively rare this item is.</summary>
        [JsonProperty(PropertyName = "in_rarity")]
        public int Rarity { get; }

        /// <summary>How many of the item may share a single inventory slot.</summary>
        [JsonProperty(PropertyName = "in_stackMax")]
        public int StackMax { get; }

        /// <summary>An in-game effect caused by keeping the item in a character's inventory.</summary>
        [JsonProperty(PropertyName = "in_effectWhileHeld")]
        // TODO This is not actually an int; this type needs to be implemented.
        public int EffectWhileHeld { get; }

        /// <summary>An in-game effect caused by using (consuming) the item.</summary>
        [JsonProperty(PropertyName = "in_effectWhenUsed")]
        // TODO This is not actually an int; this type needs to be implemented.
        public int EffectWhenUsed { get; }

        /// <summary>The parquet that corresponds to this item, if any.</summary>
        [JsonProperty(PropertyName = "in_asParquet")]
        public GameObjectID AsParquet { get; }

        /// <summary>Any <see cref="Biomes.Biome"/>-related functionality this item has.</summary>
        [JsonProperty(PropertyName = "in_itemTags")]
        public IReadOnlyList<GameObjectTag> ItemTags { get; }

        /// <summary>How this item is crafted.</summary>
        [JsonProperty(PropertyName = "in_recipe")]
        public GameObjectID Recipe { get; }

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="in_id">Unique identifier for the <see cref="Item"/>.  Cannot be null.</param>
        /// <param name="in_subtype">The type of <see cref="Item"/>.</param>
        /// <param name="in_name">Player-friendly name of the <see cref="Item"/>.  Cannot be null or empty.</param>
        /// <param name="in_description">Player-friendly description of the <see cref="Item"/>.</param>
        /// <param name="in_comment">Comment of, on, or by the <see cref="Item"/>.</param>
        /// <param name="in_price"><see cref="Item"/> cost.</param>
        /// <param name="in_rarity"><see cref="Item"/> rarity.</param>
        // TODO Implement the Inventory class.
        /// <param name="in_stackMax">How many such items may be stacked together in the <see cref="Inventory"/>.  Must be positive.</param>
        /// <param name="in_effectWhileHeld"><see cref="Item"/>'s passive effect.</param>
        /// <param name="in_effectWhenUsed"><see cref="Item"/>'s active effect.</param>
        /// <param name="in_asParquet">The parquet represented, if any.</param>
        /// <param name="in_itemTags">Any <see cref="Biomes.Biome"/>-related functionality this item has.</param>
        /// <param name="in_recipeID">The <see cref="GameObjectID"/> that expresses how to craft this <see cref="Item"/>.</param>
        [JsonConstructor]
        public Item(GameObjectID in_id, ItemType in_subtype, string in_name, string in_description, string in_comment,
                    int in_price, int in_rarity, int in_stackMax, int in_effectWhileHeld,
                    int in_effectWhenUsed, GameObjectID in_asParquet,
                    List<GameObjectTag> in_itemTags = null, GameObjectID? in_recipeID = null)
            : base(All.ItemIDs, in_id, in_name, in_description, in_comment)
        {
            Precondition.IsInRange(in_asParquet, All.ParquetIDs, nameof(in_asParquet));
            Precondition.MustBePositive(in_stackMax, nameof(in_stackMax));

            // TODO Do we need to bounds-check in_effectWhileHeld?  If so, add a unit test.
            // TODO Do we need to bounds-check in_effectWhenUsed?  If so, add a unit test.
            /* TODO This check is a good idea but it is improper to get a specific entity from All
             * during initialization of an entity.  If we are to include this functionality another
             * means of implementing it must be found.
            if (nonNullCraftingRecipeID != CraftingRecipe.NotCraftable.ID)
            {
                var craftingRecipe = All.CraftingRecipes.Get<CraftingRecipe>(nonNullCraftingRecipeID);
                var givenRecipeProducesGivenItem = false;
                foreach (var product in craftingRecipe.Products)
                {
                    if (product.ItemID == in_id)
                    {
                        givenRecipeProducesGivenItem = true;
                        break;
                    }
                }
                if (!givenRecipeProducesGivenItem)
                {
                    throw new ArgumentException($"The crafting recipe for {in_name} include {in_name} among its products.");
                }
            }
            */

            var nonNullItemTags = in_itemTags ?? Enumerable.Empty<GameObjectTag>().ToList();
            var nonNullCraftingRecipeID = in_recipeID ?? CraftingRecipe.NotCraftable.ID;

            Subtype = in_subtype;
            Price = in_price;
            Rarity = in_rarity;
            StackMax = in_stackMax;
            EffectWhileHeld = in_effectWhileHeld;
            EffectWhenUsed = in_effectWhenUsed;
            AsParquet = in_asParquet;
            ItemTags = nonNullItemTags;
            Recipe = nonNullCraftingRecipeID;
        }
        #endregion
    }
}
