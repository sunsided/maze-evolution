using System;

namespace Labyrinth
{
	/// <summary>
	/// Enumeration, die die Wände in einem rechteckigen Raum angibt
	/// </summary>
	[Flags]
	public enum Wall4
	{
		/// <summary>
		/// Keine Wände
		/// </summary>
		None = 0,

		/// <summary>
		/// Nordwand
		/// </summary>
		North = 1,

		/// <summary>
		/// Südwand
		/// </summary>
		South = 2,

		/// <summary>
		/// Ostwand
		/// </summary>
		East = 4,

		/// <summary>
		/// Westwand
		/// </summary>
		West = 8,

		/// <summary>
		/// Alle Wände
		/// </summary>
		All = North | South | East | West
	}
}
