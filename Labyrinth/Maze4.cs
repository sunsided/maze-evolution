using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Labyrinth
{
	/// <summary>
	/// Ein Labyrinth, bestehend aus <see cref="IRoom4"/>-Elementen
	/// </summary>
	public class Maze4
	{
		/// <summary>
		/// Der Labyrinthgenerator
		/// </summary>
		private readonly IMazeGenerator _generator;

		/// <summary>
		/// Die Wände
		/// </summary>
		private Wall4[,] _walls;

		/// <summary>
		/// Das Array der Räume
		/// </summary>
		private Room4[,] _rooms;

		/// <summary>
		/// Das Array der Räume
		/// </summary>
		public IRoom4[,] Rooms
		{
			[Pure] get { return _rooms; }
		}

		/// <summary>
		/// Verzeichnis von Raum auf Position
		/// </summary>
		private ConcurrentDictionary<IRoom4, Tuple<int, int>> _roomIndexLookup = new ConcurrentDictionary<IRoom4,Tuple<int,int>>();

		/// <summary>
		/// Initializes a new instance of the <see cref="Maze4"/> class.
		/// </summary>
		/// <param name="generator">The generator.</param>
		/// <remarks></remarks>
		public Maze4(IMazeGenerator generator)
		{
			Contract.Requires(generator != null);
			_generator = generator;
		}

		/// <summary>
		/// Erzeugt ein neues Labyrinth
		/// </summary>
		/// <remarks>Verwendet den im Konstruktor gesetzten Generator</remarks>
		public void GenerateNew(int width, int height)
		{
			Contract.Requires(width > 0 && height > 0);
			Wall4[,] walls = _generator.Generate(width, height);

			// Dictionary vorbereiten
			ConcurrentDictionary<IRoom4, Tuple<int, int>> lookup = new ConcurrentDictionary<IRoom4, Tuple<int, int>>();

			// Räume erzeugen
			Room4[,] rooms = new Room4[width, height];
			for (int y = 0; y < height; ++y)
			{
				for (int x = 0; x < width; ++x)
				{
					Wall4 currentWall = walls[x, y];
					Room4 currentRoom = new Room4();
					currentRoom.Tag = new Tuple<int, int>(x, y);

					// 2..n Zeile: Raum darüber setzen
					// Die letzte Zeile hat keine Räume darunter, womit alle
					// vertikalen Verbindungsfälle abgedeckt werden.
					if (y > 0)
					{
						if (!currentWall.ContainsWall(Wall4.North))
						{
							Room4 roomAbove = rooms[x, y - 1];
							currentRoom.NorthRoom = roomAbove;
							roomAbove.SouthRoom = currentRoom;
						}
					}

					// 2..n Spalte: Raum links daneben setzen
					// Die letzte Spalte hat keine Räume rechtsseitig, womit alle
					// horizontalen Verbindungsfälle abgedeckt werden.
					if (x > 0)
					{
						if (!currentWall.ContainsWall(Wall4.West))
						{
							Room4 left = rooms[x - 1, y];
							currentRoom.WestRoom = left;
							left.EastRoom = currentRoom;
						}
					}

					// Raum speichern
					rooms[x, y] = currentRoom;
					
					// Tabelle bauen
					var position = new Tuple<int, int>(x, y);
					lookup.AddOrUpdate(currentRoom, position, (room, old) => position);
				}
			}

			// Und setzen.
			_roomIndexLookup = lookup;
			_rooms = rooms;
			_walls = walls;

			// Feuer frei.
			OnMazeChanged(new MazeEventArgs(this));
		}

		/// <summary>
		/// Ermittelt einen Raum anhand seiner Koordinaten
		/// </summary>
		/// <param name="x">X-Koordinate.</param>
		/// <param name="y">Y-Koordinate.</param>
		/// <returns>Der Raum oder <c>null</c>, falls der Raum nicht gefunden wurde</returns>
		/// <remarks></remarks>
		public IRoom4 GetRoom(int x, int y)
		{
			Contract.Requires(x >= 0 && x >= 0);
			Contract.Ensures(Contract.Result<IRoom4>() != null);
			return Rooms[x, y];
		}

		/// <summary>
		/// Ermittelt die Koordinaten eines Raumes
		/// </summary>
		/// <param name="room">Der zu findende Raum</param>
		/// <returns>Die Position des Raumes oder <c>null</c>, falls der Raum nicht gefunden wurde</returns>
		public Tuple<int, int> GetPosition(IRoom4 room)
		{
			Contract.Requires(room != null);

			Tuple<int, int> position;
			return _roomIndexLookup.TryGetValue(room, out position) ? position : null;
		}

		/// <summary>
		/// Setzt einen Startraum und ermittelt den Raum, der am weitesten entfernt ist
		/// </summary>
		/// <param name="x">X-Koordinate.</param>
		/// <param name="y">Y-Koordinate.</param>
		/// <param name="distances">Die Karte der Entfernungen vom Startraum</param>
		/// <returns>Die Liste der Sackgassen mit ihren Entfernungen zum Startpunkt</returns>
		/// <exception cref="ArgumentException">Der angegebene Raum liegt nicht im Labyrinth</exception>
		/// <remarks></remarks>
		public IList<Tuple<int, IRoom4>> SetStartingPoint(int x, int y, out int[,] distances)
		{
			Contract.Requires(x >= 0 && x >= 0);
			Contract.Ensures(Contract.Result<IList<Tuple<int, IRoom4>>>() != null);
			Contract.Ensures(Contract.ValueAtReturn(out distances) != null);

			IRoom4 room = Rooms[x, y];
			Contract.Assume(room != null);
			return SetStartingPoint(room, out distances);
		}
		
		/// <summary>
		/// Setzt einen Startraum und ermittelt den Raum, der am weitesten entfernt ist
		/// </summary>
		/// <param name="startRoom">Der Startraum</param>
		/// <param name="distances">Die Karte der Entfernungen vom Startraum</param>
		/// <returns>Die Liste der Sackgassen mit ihren Entfernungen zum Startpunkt</returns>
		/// <exception cref="ArgumentException">Der angegebene Raum liegt nicht im Labyrinth</exception>
		public IList<Tuple<int, IRoom4>> SetStartingPoint(IRoom4 startRoom, out int[,] distances)
		{
			// TODO: Logik in den Generator übernehmen.

			Contract.Requires(startRoom != null);
			Contract.Ensures(Contract.Result<IList<Tuple<int, IRoom4>>>() != null);
			Contract.Ensures(Contract.ValueAtReturn(out distances) != null);

			var startPosition = GetPosition(startRoom);
			if (startPosition == null) throw new ArgumentException("Der gegebene Raum liegt nicht im Labyrinth", "startRoom");

			// Werte intialisieren
			int width = Rooms.GetLength(0);
			int height = Rooms.GetLength(1);

			int roomCount = width * height;
			int roomsVisited = 1;

			// Hilfslisten vorbereiten
			HashSet<Tuple<IRoom4, Door4>> visited = new HashSet<Tuple<IRoom4, Door4>>();
			distances = new int[width, height];
			int lastDistance = 0;

			// Die Zielwerte
			List<Tuple<int, IRoom4>> deadEnds = new List<Tuple<int, IRoom4>>();

			// Bewegungs-Hilfsklasse erzeugen
			Marcher4 marcher = new Marcher4(Door4.East, startPosition.Item1, startPosition.Item2);

			// Und los.
			while (roomsVisited < roomCount)
			{
				IRoom4 currentRoom = GetRoom(marcher.X, marcher.Y);
				Door4 currentDoors = currentRoom.Doors;

				// Signalify your life
				OnMarcherMoved(new DistanceTrackingEventArgs(marcher.X, marcher.Y, marcher.Direction));

				// Distanz setzen.
				// - Wenn wir nicht der Startraum sind und noch keine Distanz haben, alte Distanz erhöhen und setzen
				// - In jedem anderen Fall: "alte Distanz" auf aktuelle Distanz setzen (backtracking!)
				int currentDistance;
				if (currentRoom != startRoom && distances[marcher.X, marcher.Y] == 0 )
				{
					currentDistance = ++lastDistance;
					distances[marcher.X, marcher.Y] = currentDistance;
					++roomsVisited;
				}
				else
				{
					lastDistance = currentDistance = distances[marcher.X, marcher.Y];
				}

				// Wenn wir uns nach links bewegen können und dort noch nicht waren
				if (marcher.CanTurnLeft(currentDoors) && !BeenThere(visited, currentRoom, marcher.LeftDirection))
				{
					SetBeenThere(visited, currentRoom, marcher.LeftDirection);
					marcher.TurnLeft().MoveForward();
					continue;
				}
				
				// Wenn wir uns vorwärts bewegen können und dort noch nicht waren
				if (marcher.CanMoveForward(currentDoors) && !BeenThere(visited, currentRoom, marcher.Direction))
				{
					SetBeenThere(visited, currentRoom, marcher.Direction);
					marcher.MoveForward();
					continue;
				}

				// Wenn wir uns nach rechts bewegen können und dort noch nicht waren
				// (entspricht mehrfacher Linksdrehung)
				if (marcher.CanTurnRight(currentDoors) && !BeenThere(visited, currentRoom, marcher.RightDirection))
				{
					SetBeenThere(visited, currentRoom, marcher.RightDirection);
					marcher.TurnRight().MoveForward();
					continue;
				}

				// Hier angekommen, kann weder vorwärts, noch rückwärts gegangen werden
				
				// Raum als Sackgasse markieren
				if (currentRoom != startRoom) deadEnds.Add(new Tuple<int, IRoom4>(currentDistance, currentRoom));
				marcher.TurnAround().MoveForward();

			}

			// Entfernteste Punkte zurückgeben
			deadEnds.Sort((a, b) => -1 * a.Item1.CompareTo(b.Item1));
			deadEnds.TrimExcess();
			return deadEnds;
		}

		/// <summary>
		/// Ermittelt, ob eine Tür bereits durchschritten wurde
		/// </summary>
		/// <param name="set">Das Lookup-Set</param>
		/// <param name="room">Der Raum</param>
		/// <param name="direction">Die Richtung</param>
		/// <returns><c>true</c>, wenn der angegebene Raum bereits durch diese Tür verlassen wurde, ansonsten <c>false</c></returns>
		private static bool BeenThere(HashSet<Tuple<IRoom4, Door4>> set, IRoom4 room, Door4 direction)
		{
			Contract.Requires(set != null);
			Contract.Requires(room != null);
			Contract.Requires(direction.ExactlyOneValueSet());
			return set.Contains(new Tuple<IRoom4, Door4>(room, direction));
		}

		/// <summary>
		/// Legt fest, dass eine Tür durchschritten wurde
		/// </summary>
		/// <param name="set">Das Lookup-Set</param>
		/// <param name="room">Der Raum</param>
		/// <param name="direction">Die Richtung</param>
		private static void SetBeenThere(HashSet<Tuple<IRoom4, Door4>> set, IRoom4 room, Door4 direction)
		{
			Contract.Requires(set != null);
			Contract.Requires(room != null);
			Contract.Requires(direction.ExactlyOneValueSet());
			set.Add(new Tuple<IRoom4, Door4>(room, direction));
		}

		/// <summary>
		/// Gibt das Labyrinth als String aus
		/// </summary>
		/// <returns></returns>
		/// <remarks></remarks>
		public string RenderToString()
		{
			return _walls == null ? String.Empty : _walls.RenderToString(true);
		}

		#region Events 

		/// <summary>
		/// Wird gerufen, wenn sich das Labyrinth verändert hat
		/// </summary>
		public event EventHandler<MazeEventArgs> MazeChanged;

		/// <summary>
		/// Raises the <see cref="MazeChanged"/> event.
		/// </summary>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		/// <remarks></remarks>
		private void OnMazeChanged(MazeEventArgs e)
		{
			EventHandler<MazeEventArgs> handler = MazeChanged;
			if (handler != null) handler(this, e);
		}

		#endregion Events

		#region Tracking der Distanzmessung

		/// <summary>
		/// Wird gerufen, wenn sich der Marcher bewegt hat
		/// </summary>
		public event EventHandler<DistanceTrackingEventArgs> MarcherMoved;

		/// <summary>
		/// Raises the <see cref="MarcherMoved"/> event.
		/// </summary>
		/// <param name="e">The <see cref="Labyrinth.Maze4.DistanceTrackingEventArgs"/> instance containing the event data.</param>
		/// <remarks></remarks>
		private void OnMarcherMoved(DistanceTrackingEventArgs e)
		{
			EventHandler<DistanceTrackingEventArgs> handler = MarcherMoved;
			if (handler != null) handler(this, e);
		}

		/// <summary>
		/// Ereignisparameter für das Tracking der DIstanzmessung
		/// </summary>
		public class DistanceTrackingEventArgs : EventArgs
		{
			public int X { get; private set; }
			public int Y { get; private set; }
			public Door4 CurrentDirection { get; private set; }

			public DistanceTrackingEventArgs(int x, int y, Door4 currentDirection)
			{
				X = x;
				Y = y;
				CurrentDirection = currentDirection;
			}
		}

		#endregion Tracking der Distanzmessung
	}
}
