using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace Labyrinth
{
	/// <summary>
	/// Stellt einen rechteckigen Raum dar
	/// </summary>
	[DebuggerDisplay("Walls: {Walls}, Doors: {Doors}, Tag: {Tag}")]
	public sealed class Room4 : IRoom4
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:System.Object"/> class.
		/// </summary>
		/// <remarks></remarks>
		public Room4()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Room4"/> class.
		/// </summary>
		/// <param name="north">The north.</param>
		/// <param name="south">The south.</param>
		/// <param name="east">The east.</param>
		/// <param name="west">The west.</param>
		/// <remarks></remarks>
		public Room4(Room4 north, Room4 south, Room4 east, Room4 west)
		{
			NorthRoom = north;
			SouthRoom = south;
			WestRoom = west;
			EastRoom = east;
		}

		/// <summary>
		/// Benutzerdefiniertes Tag
		/// </summary>
		public object Tag { [Pure] get; set; }

		/// <summary>
		/// Der nördliche Raum
		/// </summary>
		public Room4 NorthRoom { get; set; }

		/// <summary>
		/// Der südliche Raum
		/// </summary>
		public Room4 SouthRoom { get; set; }

		/// <summary>
		/// Der östliche Raum
		/// </summary>
		public Room4 EastRoom { get; set; }

		/// <summary>
		/// Der westliche Raum
		/// </summary>
		public Room4 WestRoom { get; set; }

		/// <summary>
		/// Gets a value indicating whether this instance a has west room.
		/// </summary>
		/// <remarks></remarks>
		public bool HasWestRoom { [Pure] get { return WestRoom != null; } }

		/// <summary>
		/// Gets a value indicating whether this instance a has a north room.
		/// </summary>
		/// <remarks></remarks>
		public bool HasNorthRoom { [Pure] get { return NorthRoom != null; } }

		/// <summary>
		/// Gets a value indicating whether this instance a has a west room.
		/// </summary>
		/// <remarks></remarks>
		public bool HasSouthRoom { [Pure] get { return SouthRoom != null; } }

		/// <summary>
		/// Gets a value indicating whether this instance a has a east room.
		/// </summary>
		/// <remarks></remarks>
		public bool HasEastRoom { [Pure] get { return EastRoom != null; } }

		/// <summary>
		/// Liefert die Wandbeschreibung
		/// </summary>
		public Wall4 Walls { [Pure] get { return (Wall4) this; } }

		/// <summary>
		/// Liefert die Türbeschreibung
		/// </summary>
		public Door4 Doors { [Pure] get { return (Door4)this; } }

		/// <summary>
		/// Performs an explicit conversion from <see cref="Labyrinth.Room4"/> to <see cref="Labyrinth.Wall4"/>.
		/// </summary>
		/// <param name="room">The room.</param>
		/// <returns>The result of the conversion.</returns>
		/// <remarks></remarks>
		[Pure]
		public static explicit operator Wall4 (Room4 room)
		{
			Wall4 walls = Wall4.None;
			if (!room.HasNorthRoom) walls |= Wall4.North;
			if (!room.HasSouthRoom) walls |= Wall4.South;
			if (!room.HasEastRoom) walls |= Wall4.East;
			if (!room.HasWestRoom) walls |= Wall4.West;
			return walls;
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="Labyrinth.Room4"/> to <see cref="Labyrinth.Door4"/>.
		/// </summary>
		/// <param name="room">The room.</param>
		/// <returns>The result of the conversion.</returns>
		/// <remarks></remarks>
		[Pure]
		public static explicit operator Door4(Room4 room)
		{
			Door4 doors = Door4.None;
			if (room.HasNorthRoom) doors |= Door4.North;
			if (room.HasSouthRoom) doors |= Door4.South;
			if (room.HasEastRoom) doors |= Door4.East;
			if (room.HasWestRoom) doors |= Door4.West;
			return doors;
		}

		#region IRoom4 Members

		/// <summary>
		/// Der nördliche Raum
		/// </summary>
		/// <remarks></remarks>
		IRoom4 IRoom4.NorthRoom
		{
			get { return NorthRoom; }
		}

		/// <summary>
		/// Der südliche Raum
		/// </summary>
		/// <remarks></remarks>
		IRoom4 IRoom4.SouthRoom
		{
			get { return SouthRoom; }
		}

		/// <summary>
		/// Der östliche Raum
		/// </summary>
		/// <remarks></remarks>
		IRoom4 IRoom4.EastRoom
		{
			get { return EastRoom; }
		}

		/// <summary>
		/// Der westliche Raum
		/// </summary>
		/// <remarks></remarks>
		IRoom4 IRoom4.WestRoom
		{
			get { return WestRoom; }
		}

		#endregion
	}
}
