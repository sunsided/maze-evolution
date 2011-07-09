using System.Diagnostics.Contracts;

namespace Labyrinth
{
	/// <summary>
	/// Interface, das einen Raum beschreibt
	/// </summary>
	/// <remarks></remarks>
	public interface IRoom4
	{
		/// <summary>
		/// Benutzerdefiniertes Tag
		/// </summary>
		object Tag { [Pure] get; set; }

		/// <summary>
		/// Der nördliche Raum
		/// </summary>
		IRoom4 NorthRoom { get; }

		/// <summary>
		/// Der südliche Raum
		/// </summary>
		IRoom4 SouthRoom { get; }

		/// <summary>
		/// Der östliche Raum
		/// </summary>
		IRoom4 EastRoom { get; }

		/// <summary>
		/// Der westliche Raum
		/// </summary>
		IRoom4 WestRoom { get; }

		/// <summary>
		/// Gets a value indicating whether this instance a has west room.
		/// </summary>
		/// <remarks></remarks>
		bool HasWestRoom { [Pure] get; }

		/// <summary>
		/// Gets a value indicating whether this instance a has a north room.
		/// </summary>
		/// <remarks></remarks>
		bool HasNorthRoom { [Pure] get; }

		/// <summary>
		/// Gets a value indicating whether this instance a has a west room.
		/// </summary>
		/// <remarks></remarks>
		bool HasSouthRoom { [Pure] get; }

		/// <summary>
		/// Gets a value indicating whether this instance a has a east room.
		/// </summary>
		/// <remarks></remarks>
		bool HasEastRoom { [Pure] get; }

		/// <summary>
		/// Liefert die Wandbeschreibung
		/// </summary>
		Wall4 Walls { [Pure] get; }

		/// <summary>
		/// Liefert die Türbeschreibung
		/// </summary>
		Door4 Doors { [Pure] get; }
	}
}