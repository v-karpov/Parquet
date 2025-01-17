using System;
using System.Collections.Generic;
using System.Linq;
using ParquetClassLibrary.Utilities;

namespace ParquetClassLibrary.Crafting
{
    /// <summary>
    /// Models the ingredients and process needed to produce a new item.
    /// </summary>
    public class CraftingRecipe : GameObject
    {
        /// <summary>Used in defining <see cref="NotCraftable"/>.</summary>
        private static IReadOnlyList<RecipeElement> EmptyCraftingElementList { get; } =
            new List<RecipeElement> { RecipeElement.None };

        /// <summary>Represents the lack of a <see cref="CraftingRecipe"/> for uncraftable <see cref="Items.Item"/>s.</summary>
        public static CraftingRecipe NotCraftable { get; } =
            new CraftingRecipe(GameObjectID.None, "Not Craftable", "Not Craftable", "",
                               EmptyCraftingElementList, EmptyCraftingElementList,
                               new StrikePanel[All.Dimensions.PanelsPerPatternHeight,
                                               All.Dimensions.PanelsPerPatternWidth]);

        /// <summary>The types and amounts of <see cref="Items.Item"/>s created by following this recipe.</summary>
        public IReadOnlyList<RecipeElement> Products { get; }

        /// <summary>All materials and their quantities needed to follow this recipe once.</summary>
        public IReadOnlyList<RecipeElement> Ingredients { get; }

        /// <summary>The arrangment of panels encompassed by this recipe.</summary>
        public StrikePanel[,] PanelPattern { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CraftingRecipe"/> class.
        /// </summary>
        /// <param name="in_id">Unique identifier for the <see cref="CraftingRecipe"/>.  Cannot be null.</param>
        /// <param name="in_name">Player-friendly name of the <see cref="CraftingRecipe"/>.  Cannot be null or empty.</param>
        /// <param name="in_description">Player-friendly description of the <see cref="CraftingRecipe"/>.</param>
        /// <param name="in_comment">Comment of, on, or by the <see cref="CraftingRecipe"/>.</param>
        /// <param name="in_products">The types and quantities of <see cref="Items.Item"/>s created by following this recipe once.</param>
        /// <param name="in_ingredients">All items needed to follow this <see cref="CraftingRecipe"/> once.</param>
        /// <param name="in_panelPattern">The arrangment of panels encompassed by this <see cref="CraftingRecipe"/>.</param>
        /// <exception cref="IndexOutOfRangeException">
        /// Thrown when <paramref name="in_panelPattern"/> has zero-dimensions or dimensions larger than those given by
        /// <see cref="All.Dimensions.PanelsPerPatternWidth"/> and <see cref="All.Dimensions.PanelsPerPatternHeight"/>.
        /// </exception>
        public CraftingRecipe(GameObjectID in_id, string in_name, string in_description, string in_comment,
                              IEnumerable<RecipeElement> in_products,
                              IEnumerable<RecipeElement> in_ingredients, StrikePanel[,] in_panelPattern)
            : base(All.CraftingRecipeIDs, in_id, in_name, in_description, in_comment)
        {
            Precondition.IsNotNull(in_products, nameof(in_products));
            Precondition.IsNotEmpty(in_products, nameof(in_products));
            Precondition.IsNotNull(in_ingredients, nameof(in_ingredients));
            Precondition.IsNotEmpty(in_ingredients, nameof(in_ingredients));
            Precondition.IsNotNull(in_panelPattern, nameof(in_panelPattern));
            if (in_panelPattern.GetLength(0) > All.Dimensions.PanelsPerPatternHeight
                || in_panelPattern.GetLength(1) > All.Dimensions.PanelsPerPatternWidth
                || in_panelPattern.GetLength(0) < 1
                || in_panelPattern.GetLength(1) < 1)
            {
                throw new IndexOutOfRangeException($"Dimension outside specification: {nameof(in_panelPattern)}");
            }

            Products = in_products.ToList();
            Ingredients = in_ingredients.ToList();
            PanelPattern = in_panelPattern;
        }
    }
}
