using Microsoft.Xna.Framework;
using ParquetClassLibrary.Parquets;


namespace ParquetClassLibrary.Rooms
{
    /// <summary>
    /// A <see cref="ParquetStack"/> together with its coordinates on the world map.
    /// </summary>
    public struct Space
    {
        /// <summary>The null <see cref="Space"/>, representing an arbitrary empty <see cref="ParquetStack"/>.</summary>
        public static readonly Space Empty = new Space(Point.Zero, ParquetStack.Empty);

        /// <summary>Location of this <see cref="Space"/>.</summary>
        public readonly Point Position;

        /// <summary>All parquets occupying this <see cref="Space"/>.</summary>
        public readonly ParquetStack Content;

        /// <summary>
        /// Initializes a new instance of the <see cref="Space"/> class.
        /// </summary>
        /// <param name="in_position">Location of this <see cref="Space"/>.</param>
        /// <param name="in_content">All parquets occupying this <see cref="Space"/>.</param>
        public Space(Point in_position, ParquetStack in_content)
        {
            Position = in_position;
            Content = in_content;
        }
    }
}
