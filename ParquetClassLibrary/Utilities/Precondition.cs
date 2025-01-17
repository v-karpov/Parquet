using System;
using System.Collections.Generic;
using System.Linq;

namespace ParquetClassLibrary.Utilities
{
    /// <summary>
    /// Provides constructors and initialization routines with concise arugment boilerplate.
    /// </summary>
    public static class Precondition
    {
        #region Class Defaults
        /// <summary>Text to use when no argument name is provided.</summary>
        private const string DefaultArgumentName = "Argument";
        #endregion

        /// <summary>
        /// Checks if the given <see cref="GameObjectID"/> falls within the given <see cref="Range{T}"/>.
        /// </summary>
        /// <param name="in_id">The identifier to test.</param>
        /// <param name="in_bounds">The range it must fall within.</param>
        /// <param name="in_argumentName">The name of the argument to use in error reporting.</param>
        /// <exception cref="ArgumentOutOfRangeException">When the identifier is not in range.</exception>
        public static void IsInRange(GameObjectID in_id, Range<GameObjectID> in_bounds,
                                     string in_argumentName = DefaultArgumentName)
        {
            if (!in_id.IsValidForRange(in_bounds))
            {
                throw new ArgumentOutOfRangeException($"{in_argumentName}: {in_id} is not within {in_bounds}.");
            }
        }

        /// <summary>
        /// Checks if the first given <see cref="Range{T}"/> falls within the second given <see cref="Range{T}"/>.
        /// </summary>
        /// <param name="in_innerBounds">The range to test.</param>
        /// <param name="in_outerBounds">The range it must fall within.</param>
        /// <param name="in_argumentName">The name of the argument to use in error reporting.</param>
        /// <exception cref="ArgumentOutOfRangeException">When the first range is not in the second range.</exception>
        public static void IsInRange(Range<GameObjectID> in_innerBounds, Range<GameObjectID> in_outerBounds,
                                     string in_argumentName = DefaultArgumentName)
        {
            if (!in_outerBounds.ContainsRange(in_innerBounds))
            {
                throw new ArgumentOutOfRangeException(
                    $"{in_argumentName}: {in_innerBounds} is not within {in_outerBounds}.");
            }
        }

        /// <summary>
        /// Checks if the first given <see cref="GameObjectID"/> falls within at least one of the
        /// given collection of <see cref="Range{T}"/>s.
        /// </summary>
        /// <param name="in_id">The identifier to test.</param>
        /// <param name="in_boundsCollection">The collection of ranges it must fall within.</param>
        /// <param name="in_argumentName">The name of the argument to use in error reporting.</param>
        /// <exception cref="ArgumentOutOfRangeException">When the identifier is not in any of the ranges.</exception>
        public static void IsInRange(GameObjectID in_id, List<Range<GameObjectID>> in_boundsCollection,
                                     string in_argumentName = DefaultArgumentName)
        {
            if (!in_id.IsValidForRange(in_boundsCollection))
            {
                var allBounds = "";
                foreach (var range in in_boundsCollection)
                {
                    allBounds += range + " ";
                }
                throw new ArgumentOutOfRangeException(
                    $"{in_argumentName}: {in_id} is not within {allBounds}.");
            }
        }

        /// <summary>
        /// Checks if the given <see cref="Range{T}"/> falls within at least one of the
        /// given collection of <see cref="Range{T}"/>s.
        /// </summary>
        /// <param name="in_innerBounds">The range to test.</param>
        /// <param name="in_boundsCollection">The collection of ranges it must fall within.</param>
        /// <param name="in_argumentName">The name of the argument to use in error reporting.</param>
        /// <exception cref="ArgumentOutOfRangeException">When the first range is not in the second range.</exception>
        public static void IsInRange(Range<GameObjectID> in_innerBounds, List<Range<GameObjectID>> in_boundsCollection,
                                     string in_argumentName = DefaultArgumentName)
        {
            if (!in_boundsCollection.ContainsRange(in_innerBounds))
            {
                throw new ArgumentOutOfRangeException(
                    $"{in_argumentName}: {in_innerBounds} is not within {in_boundsCollection}.");
            }
        }

        /// <summary>
        /// Verifies that the first given <see langword="class"/> is or is derived from
        /// the second given <see langword="class"/>.
        /// </summary>
        /// <param name="in_argumentName">The name of the argument to use in error reporting.</param>
        /// <typeparam name="TypeToCheck">The type to check.</typeparam>
        /// <typeparam name="TargetType">The type of which it must be a subtype.</typeparam>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown when <typeparamref name="TypeToCheck"/> does not correspond to <typeparamref name="TargetType"/>.
        /// </exception>
        public static void IsOfType<TypeToCheck, TargetType>(string in_argumentName = DefaultArgumentName)
        {
            if (!typeof(TypeToCheck).IsSubclassOf(typeof(TargetType))
                && typeof(TypeToCheck) != typeof(TargetType))
            {
                throw new InvalidCastException(
                    $"{in_argumentName} is of type {typeof(TypeToCheck)} but must be of type {typeof(TargetType)}.");
            }
        }

        /// <summary>
        /// Verifies that all of the given <see cref="GameObjectID"/>s fall within the given <see cref="Range{T}"/>.
        /// </summary>
        /// <param name="in_enumerable">The identifiers to test.</param>
        /// <param name="in_bounds">The range they must fall within.</param>
        /// <param name="in_argumentName">The name of the argument to use in error reporting.</param>
        /// <exception cref="ArgumentOutOfRangeException">When the identifier is not in range.</exception>
        public static void AreInRange(IEnumerable<GameObjectID> in_enumerable, Range<GameObjectID> in_bounds,
                                      string in_argumentName = DefaultArgumentName)
        {
            foreach (var id in in_enumerable ?? Enumerable.Empty<GameObjectID>())
            {
                if (!id.IsValidForRange(in_bounds))
                {
                    throw new ArgumentOutOfRangeException($"{in_argumentName}: {id} is not within {in_bounds}.");
                }
            }
        }

        /// <summary>
        /// Verifies that all of the given <see cref="GameObjectID"/>s fall within the given 
        /// collection of <see cref="Range{T}"/>s.
        /// </summary>
        /// <param name="in_enumerable">The identifiers to test.</param>
        /// <param name="in_boundsCollection">The collection of ranges they must fall within.</param>
        /// <param name="in_argumentName">The name of the argument to use in error reporting.</param>
        /// <exception cref="ArgumentOutOfRangeException">When the identifier is not in range.</exception>
        public static void AreInRange(IEnumerable<GameObjectID> in_enumerable, List<Range<GameObjectID>> in_boundsCollection,
                                      string in_argumentName = DefaultArgumentName)
        {
            foreach (var id in in_enumerable ?? Enumerable.Empty<GameObjectID>())
            {
                if (!id.IsValidForRange(in_boundsCollection))
                {
                    throw new ArgumentOutOfRangeException($"{in_argumentName}: {id} is not within {in_boundsCollection}.");
                }
            }
        }

        /// <summary>
        /// Verifies that the given number is zero or positive.
        /// </summary>
        /// <param name="in_number">The number to test.</param>
        /// <param name="in_argumentName">The name of the argument to use in error reporting.</param>
        /// <exception cref="ArgumentOutOfRangeException">When the number is -1 or less.</exception>
        public static void MustBeNonNegative(int in_number, string in_argumentName = DefaultArgumentName)
        {
            if (in_number < 0)
            {
                throw new ArgumentOutOfRangeException($"{in_argumentName} must be a non-negative number.");
            }
        }

        /// <summary>
        /// Verifies that the given number is positive.
        /// </summary>
        /// <param name="in_number">The number to test.</param>
        /// <param name="in_argumentName">The name of the argument to use in error reporting.</param>
        /// <exception cref="ArgumentOutOfRangeException">When the number is zero or less.</exception>
        public static void MustBePositive(int in_number, string in_argumentName = DefaultArgumentName)
        {
            if (in_number < 1)
            {
                throw new ArgumentOutOfRangeException($"{in_argumentName} must be a positive number.");
            }
        }

        /// <summary>
        /// Verifies that the given <see langword="string"/> is not empty.
        /// </summary>
        /// <param name="in_string">The string to test.</param>
        /// <param name="in_argumentName">The name of the argument to use in error reporting.</param>
        /// <exception cref="IndexOutOfRangeException">When <paramref name="in_string"/> is null or empty.</exception>
        public static void IsNotEmpty(string in_string, string in_argumentName = DefaultArgumentName)
        {
            if (string.IsNullOrEmpty(in_string))
            {
                throw new IndexOutOfRangeException($"{in_argumentName} is null or empty.");
            }
        }

        /// <summary>
        /// Verifies that the given <see cref="IEnumerable{T}"/> is not empty.
        /// </summary>
        /// <param name="in_enumerable">The collection to test.</param>
        /// <param name="in_argumentName">The name of the argument to use in error reporting.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown when <paramref name="in_enumerable"/> is null or empty.</exception>
        public static void IsNotEmpty<T>(IEnumerable<T> in_enumerable, string in_argumentName = DefaultArgumentName)
        {
            if (!in_enumerable?.Any() ?? false)
            {
                throw new IndexOutOfRangeException($"{in_argumentName} is empty.");
            }
        }

        /// <summary>
        /// Verifies that the given reference is not <c>null</c>.
        /// </summary>
        /// <param name="in_reference">The reference to test.</param>
        /// <param name="in_argumentName">The name of the argument to use in error reporting.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="in_reference"/> is null.</exception>
        public static void IsNotNull(object in_reference, string in_argumentName = DefaultArgumentName)
        {
            if (null == in_reference)
            {
                throw new ArgumentNullException($"{in_argumentName} is null.");
            }
        }
    }
}
