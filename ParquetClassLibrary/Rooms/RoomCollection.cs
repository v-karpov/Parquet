using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace ParquetClassLibrary.Rooms
{
    /// <summary>
    /// Stores an <see cref="GameObject"/> collection.
    /// Provides bounds-checking and type-checking against <typeparamref name="ParentType"/>.
    /// </summary>
    /// <remarks>
    /// This generic version is intended to support <see cref="All.Parquets"/> allowing
    /// the collection to store all parquet types but return only the requested subtype.
    /// </remarks>
    public class RoomCollection
    {
        /// <summary>The internal collection mechanism.</summary>
        private IReadOnlyList<Room> Rooms { get; } = new List<Room>();

        /// <summary>The number of <see cref="GameObject"/>s in the <see cref="RoomCollection"/>.</summary>
        public int Count
            => Rooms.Count;

        /// <summary>
        /// Determines whether the <see cref="RoomCollection"/> contains the specified <see cref="Room"/>.
        /// </summary>
        /// <param name="in_room">The <see cref="Room"/> to find.</param>
        /// <returns><c>true</c> if the <see cref="Room"/> was found; <c>false</c> otherwise.</returns>
        public bool Contains(Room in_room)
            => Rooms.Contains(in_room);

        /// <summary>
        /// Returns the <see cref="Room"/> at the given position, if there is one.
        /// </summary>
        /// <param name="in_position">An in-bonds position to search for a <see cref="Room"/>.</param>
        /// <returns>The specified <see cref="Room"/> if found; otherwise, null.</returns>
        public Room GetRoomAt(Point in_position)
            => Rooms.First(room => room.ContainsPosition(in_position));

        #region Utility Methods
        /// <summary>
        /// Retrieves an enumerator for the <see cref="RoomCollection"/>.
        /// </summary>
        /// <returns>An enumerator that iterates through the collection.</returns>
        public IEnumerator<Room> GetEnumerator()
            => Rooms.GetEnumerator();

        /// <summary>
        /// Returns a <see langword="string"/> that represents the current <see cref="RoomCollection"/>.
        /// </summary>
        /// <returns>The representation.</returns>
        public override string ToString()
            => $"{Rooms.Count} Rooms";
        #endregion
    }
}
