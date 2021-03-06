﻿using System;

namespace Labyrinth
{
	/// <summary>
	/// Enumeration, die die Türen in einem rechteckigen Raum angibt
	/// </summary>
	[Flags]
	public enum Door4
	{
		/// <summary>
		/// Keine Türem
		/// </summary>
		None = 0,

		/// <summary>
		/// Nordtür
		/// </summary>
		North = 1,

		/// <summary>
		/// Südtür
		/// </summary>
		South = 2,

		/// <summary>
		/// Osttür
		/// </summary>
		East = 4,

		/// <summary>
		/// Westtür
		/// </summary>
		West = 8,

		/// <summary>
		/// Alle Türen
		/// </summary>
		All = North | South | East | West
	}
}
