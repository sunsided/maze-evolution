using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace Labyrinth
{
	/// <summary>
	/// Klasse, die die Bewegungen erleichtert
	/// </summary>
	[DebuggerDisplay("at {X},{Y} facing {Direction}")]
	public sealed class Marcher4
	{
		/// <summary>
		/// Die Marschrichtung
		/// </summary>
		public Door4 Direction { get; private set; }

		/// <summary>
		/// Die Richtung linkerhand
		/// </summary>
		public Door4 LeftDirection
		{
			get
			{
				switch (Direction)
				{
					case Door4.East:
						return Door4.North;
					case Door4.West:
						return Door4.South;
					case Door4.North:
						return Door4.West;
					case Door4.South:
						return Door4.East;
					default:
						throw new InvalidOperationException("Ungültiger Zustand: " + Direction);
				}
			}
		}

		/// <summary>
		/// Die Richtung rechterhand
		/// </summary>
		public Door4 RightDirection
		{
			get
			{
				switch (Direction)
				{
					case Door4.East:
						return Door4.South;
					case Door4.West:
						return Door4.North;
					case Door4.North:
						return Door4.East;
					case Door4.South:
						return Door4.West;
					default:
						throw new InvalidOperationException("Ungültiger Zustand: " + Direction);
				}
			}
		}

		/// <summary>
		/// Gets or sets the X.
		/// </summary>
		/// <value>The X.</value>
		/// <remarks></remarks>
		public int X { get; private set; }

		/// <summary>
		/// Gets or sets the Y.
		/// </summary>
		/// <value>The Y.</value>
		/// <remarks></remarks>
		public int Y { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Marcher4"/> class.
		/// </summary>
		/// <param name="startDirection">The start direction.</param>
		/// <param name="startX">The start X.</param>
		/// <param name="startY">The start Y.</param>
		/// <remarks></remarks>
		public Marcher4(Door4 startDirection, int startX, int startY)
		{
			Contract.Requires(startDirection.ExactlyOneValueSet());
			Contract.Requires(startX >= 0 && startY >= 0);
			Direction = startDirection;
			X = startX;
			Y = startY;
		}

		/// <summary>
		/// Ermittelt, ob eine Vorwärtsbewegung möglich ist.
		/// </summary>
		/// <param name="door">Die zu testende Tür</param>
		/// <returns><c>true</c>, wenn Vorwärtsbewegung möglich, ansonsten <c>false</c></returns>
		public bool CanMoveForward(Door4 door)
		{
			return door.ContainsDoor(Direction);
		}

		/// <summary>
		/// Ermittelt, ob eine Linksbewegung  möglich ist.
		/// </summary>
		/// <param name="door">Die zu testende Tür</param>
		/// <returns><c>true</c>, wenn Linksbewegung möglich, ansonsten <c>false</c></returns>
		public bool CanTurnLeft(Door4 door)
		{
			switch (Direction)
			{
				case Door4.North:
					return door.ContainsDoor(Door4.West);
				case Door4.South:
					return door.ContainsDoor(Door4.East);
				case Door4.East:
					return door.ContainsDoor(Door4.North);
				case Door4.West:
					return door.ContainsDoor(Door4.South);
				default:
					throw new InvalidOperationException("Ungültiger Zustand: " + Direction);
			}
		}

		/// <summary>
		/// Ermittelt, ob eine Rechtsbewegung möglich ist.
		/// </summary>
		/// <param name="door">Die zu testende Tür</param>
		/// <returns><c>true</c>, wenn Rechtsbewegung möglich, ansonsten <c>false</c></returns>
		public bool CanTurnRight(Door4 door)
		{
			switch (Direction)
			{
				case Door4.North:
					return door.ContainsDoor(Door4.East);
				case Door4.South:
					return door.ContainsDoor(Door4.West);
				case Door4.East:
					return door.ContainsDoor(Door4.South);
				case Door4.West:
					return door.ContainsDoor(Door4.North);
				default:
					throw new InvalidOperationException("Ungültiger Zustand: " + Direction);
			}
		}

		/// <summary>
		/// Dreht sich um 90° gegen den Uhrzeigersinn
		/// </summary>
		/// <returns>Diese Instanz</returns>
		public Marcher4 TurnLeft()
		{
			Contract.Ensures(Contract.Result<Marcher4>() != null);
			Contract.Ensures(Contract.OldValue(Direction) != Direction);

			Direction = LeftDirection;
			return this;
		}

		/// <summary>
		/// Dreht sich um 90° gegen den Uhrzeigersinn
		/// </summary>
		/// <returns>Diese Instanz</returns>
		public Marcher4 TurnRight()
		{
			Contract.Ensures(Contract.Result<Marcher4>() != null);
			Contract.Ensures(Contract.OldValue(Direction) != Direction);

			Direction = RightDirection;
			return this;
		}

		/// <summary>
		/// Umdrehen um 180°
		/// </summary>
		/// <returns>Diese Instanz</returns>
		public Marcher4 TurnAround()
		{
			Contract.Ensures(Contract.Result<Marcher4>() != null);
			Contract.Ensures(Contract.OldValue(Direction) != Direction);

			switch (Direction)
			{
				case Door4.East:
					Direction = Door4.West;
					break;
				case Door4.West:
					Direction = Door4.East;
					break;
				case Door4.North:
					Direction = Door4.South;
					break;
				case Door4.South:
					Direction = Door4.North;
					break;
				default:
					throw new InvalidOperationException("Ungültiger Zustand: " + Direction);
			}
			return this;
		}

		/// <summary>
		/// Bewegt das Element vorwärts
		/// </summary>
		/// <returns>Diese Instanz</returns>
		public Marcher4 MoveForward()
		{
			Contract.Ensures(Contract.Result<Marcher4>() != null);
			Contract.Ensures(Contract.OldValue(Direction) == Direction);

			switch (Direction)
			{
				case Door4.East:
					++X;
					break;
				case Door4.West:
					--X;
					break;
				case Door4.North:
					--Y;
					break;
				case Door4.South:
					++Y;
					break;
				default:
					throw new InvalidOperationException("Ungültiger Zustand: " + Direction);
			}
			return this;
		}
	}
}
