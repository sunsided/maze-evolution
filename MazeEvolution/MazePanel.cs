using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Labyrinth;

namespace MazeEvolution
{
	/// <summary>
	/// <see cref="Panel"/>, das ein Labyrinth darstellt
	/// </summary>
	public partial class MazePanel : Panel
	{
		private object lockObject = new object();

		/// <summary>
		/// Initializes a new instance of the <see cref="T:System.Windows.Forms.Panel"/> class.
		/// </summary>
		/// <remarks></remarks>
		public MazePanel()
		{
			InitializeComponent();
			DoubleBuffered = true;
		}

		/// <summary>
		/// Das Labyrinth
		/// </summary>
		public Maze4 Maze { [Pure]
		get; private set; }

		/// <summary>
		/// Setzt das anzuzeigende Labyrinth
		/// </summary>
		/// <param name="maze"></param>
		public void SetMaze(Maze4 maze)
		{
			Contract.Requires(maze != null);

			lock (lockObject)
			{
				_sourceCellX = null;
				_sourceCellY = null;
				_deadEnds.Clear();

				// Altes Binding entfernen
				if (Maze != null) Maze.MazeChanged -= OnMazeChanged;

				// Neu setzen
				Maze = maze;
				maze.MazeChanged += OnMazeChanged;
				RecalculateBlockSizes();
			}
			Invalidate();
		}

		/// <summary>
		/// Called when the maze is changed.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="args">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		/// <remarks></remarks>
		private void OnMazeChanged(object sender, EventArgs args)
		{
			Maze4 maze = Maze;
			if (maze == null) return;
			RecalculateBlockSizes();
		}

		/// <summary>
		/// Fires the event indicating that the panel has been resized. Inheriting controls should use this in favor of actually listening to the event, but should still call base.onResize to ensure that the event is fired for external listeners.
		/// </summary>
		/// <param name="eventargs">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
		/// <remarks></remarks>
		protected override void OnResize(EventArgs eventargs)
		{
			base.OnResize(eventargs);
			RecalculateBlockSizes();
			Invalidate();
		}

		/// <summary>
		/// Berechnet die Blockgrößen neu
		/// </summary>
		private void RecalculateBlockSizes()
		{
			Maze4 maze = Maze;
			if (maze == null) return;

			int controlWidth = ClientRectangle.Width;
			int controlHeight = ClientRectangle.Height;
			_blockWidth = (controlWidth)/(float) maze.Rooms.GetLength(0);
			_blockHeight = (controlHeight)/(float) maze.Rooms.GetLength(1);
		}

		/// <summary>
		/// Die Breite eines Blocks
		/// </summary>
		private float _blockWidth;

		/// <summary>
		/// Die Höhe eines Blocks
		/// </summary>
		private float _blockHeight;

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.Paint"/> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs"/> that contains the event data.</param>
		/// <remarks></remarks>
		protected override void OnPaint(PaintEventArgs e)
		{
			lock (lockObject)
			{

				// base.OnPaint(e);
				Graphics gr = e.Graphics;
				//gr.Clear(Color.Red);

				Maze4 maze = Maze;
				if (maze == null) return;

				int mazeWidth = maze.Rooms.GetLength(0);
				int mazeHeight = maze.Rooms.GetLength(1);

				float wallWidth = Math.Max(_blockWidth/10.0f, 1.0f);
				float wallHeight = Math.Max(_blockHeight/10.0f, 1.0f);

				Brush floorBrush = new SolidBrush(Color.White);
				Brush deadEndBrush = new SolidBrush(Color.IndianRed);
				Brush sourceFloorBrush = new SolidBrush(Color.DarkGreen);
				Brush wallBrush = new SolidBrush(Color.Black);

				for (int y = 0; y < mazeHeight; ++y)
				{
					for (int x = 0; x < mazeWidth; ++x)
					{
						// Fläche zeichnen

						float sx = x*_blockWidth;
						float sy = y*_blockHeight;

						Brush brush = floorBrush;
						if ((x == _sourceCellX) && (y == _sourceCellY))
						{
							brush = sourceFloorBrush;
						}
						else if (_deadEnds != null && _deadEnds.Contains(new Tuple<int, int>(x, y)))
						{
							brush = deadEndBrush;
						}

						RectangleF rect = new RectangleF(sx, sy, _blockWidth, _blockHeight);
						gr.FillRectangle(brush, rect);
					}
				}

				// Pfad rendern
				floorBrush = new SolidBrush(Color.Crimson);
				Brush targetBrush = new SolidBrush(Color.Orange);
				if (SourceCellSelected && _hoverCellX.HasValue && _hoverCellY.HasValue)
				{
					int x = _hoverCellX.Value;
					int y = _hoverCellY.Value;

					if (x != _sourceCellX || y != _sourceCellY)
					{

						while (true)
						{
							int distance = _distances[x, y];
							if (distance == 0) break;

							float sx = x*_blockWidth;
							float sy = y*_blockHeight;

							RectangleF rect = new RectangleF(sx, sy, _blockWidth, _blockHeight);
							if (x == _hoverCellX.Value && y == _hoverCellY.Value)
							{
								gr.FillRectangle(targetBrush, rect);
							}
							else
							{
								gr.FillRectangle(floorBrush, rect);
							}

							// Weiterwandern
							if (x - 1 >= 0 && _distances[x - 1, y] == distance - 1 && maze.Rooms[x, y].HasWestRoom)
							{
								--x;
								continue;
							}
							if (y - 1 >= 0 && _distances[x, y - 1] == distance - 1 && maze.Rooms[x, y].HasNorthRoom)
							{
								--y;
								continue;
							}
							if (x + 1 < mazeWidth && _distances[x + 1, y] == distance - 1 && maze.Rooms[x, y].HasEastRoom)
							{
								++x;
								continue;
							}
							if (y + 1 < mazeHeight && _distances[x, y + 1] == distance - 1 && maze.Rooms[x, y].HasSouthRoom)
							{
								++y;
								continue;
							}
						}
					}
				}

				// Wände rendern
				for (int y = 0; y < mazeHeight; ++y)
				{
					for (int x = 0; x < mazeWidth; ++x)
					{
						float sx = x*_blockWidth;
						float sy = y*_blockHeight;

						// W#nde zeichnen

						IRoom4 room = maze.Rooms[x, y];

						// Nordwand zeichnen
						if (!room.HasNorthRoom)
						{
							RectangleF wall = new RectangleF(sx, sy, _blockWidth, wallHeight);
							gr.FillRectangle(wallBrush, wall);
						}

						// Westwand zeichnen
						if (!room.HasWestRoom)
						{
							RectangleF wall = new RectangleF(sx, sy, wallWidth, _blockHeight);
							gr.FillRectangle(wallBrush, wall);
						}

						// Ostwand zeichnen
						if (!room.HasEastRoom)
						{
							RectangleF wall = new RectangleF(sx + _blockWidth - wallWidth, sy, wallWidth, _blockHeight);
							gr.FillRectangle(wallBrush, wall);
						}

						// Südwand zeichnen
						if (!room.HasSouthRoom)
						{
							RectangleF wall = new RectangleF(sx, sy + _blockHeight - wallHeight, _blockWidth, wallHeight);
							gr.FillRectangle(wallBrush, wall);
						}
					}
				}
			}
		}

		/// <summary>
		/// X-Koordinate der aktiv gesetzten Zelle
		/// </summary>
		private int? _sourceCellX;

		/// <summary>
		/// Y-Koordinate der aktiv gesetzten Zelle
		/// </summary>
		private int? _sourceCellY;

		/// <summary>
		/// X-Koordinate der aktiv gesetzten Zelle
		/// </summary>
		private int? _hoverCellX;

		/// <summary>
		/// Y-Koordinate der aktiv gesetzten Zelle
		/// </summary>
		private int? _hoverCellY;

		private HashSet<Tuple<int, int>> _deadEnds = new HashSet<Tuple<int, int>>();

		/// <summary>
		/// Gibt an, ob eine Zielzelle gewählt wurde
		/// </summary>
		public bool SourceCellSelected
		{
			get { return _sourceCellX.HasValue && _sourceCellY.HasValue; }
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.Click"/> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
		/// <remarks></remarks>
		protected override void OnClick(EventArgs e)
		{
			base.OnClick(e);
			MouseEventArgs me = (MouseEventArgs) e;

			Maze4 maze = Maze;
			if (maze == null) return;

			int mazeWidth = maze.Rooms.GetLength(0);
			int mazeHeight = maze.Rooms.GetLength(1);

			int controlWidth = ClientRectangle.Width;
			int controlHeight = ClientRectangle.Height;
			int sourceCellX = (int) (((float) me.X/controlWidth)*mazeWidth);
			int sourceCellY = (int) (((float) me.Y/controlHeight)*mazeHeight);

			if (sourceCellX != _sourceCellX || sourceCellY != _sourceCellY)
			{
				_sourceCellX = sourceCellX;
				_sourceCellY = sourceCellY;

				int[,] distances;
				var deadEnds = maze.SetStartingPoint(_sourceCellX.Value, _sourceCellY.Value, out distances);
				_distances = distances;

				_deadEnds.Clear();
				foreach (var entry in deadEnds.Where(entry => entry.Item1 == deadEnds[0].Item1))
				{
					_deadEnds.Add(maze.GetPosition(entry.Item2));
				}
			}
			else
			{
				_sourceCellX = null;
				_sourceCellY = null;
				_deadEnds.Clear();
			}

			Invalidate();
		}

		/// <summary>
		/// Die Distanzen
		/// </summary>
		private int[,] _distances;

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.MouseMove"/> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"/> that contains the event data.</param>
		/// <remarks></remarks>
		protected override void OnMouseMove(MouseEventArgs e)
		{
			Maze4 maze = Maze;
			if (maze == null) return;

			int mazeWidth = maze.Rooms.GetLength(0);
			int mazeHeight = maze.Rooms.GetLength(1);

			int controlWidth = ClientRectangle.Width;
			int controlHeight = ClientRectangle.Height;
			_hoverCellX = (int)(((float)e.X / controlWidth) * mazeWidth);
			_hoverCellY = (int)(((float)e.Y / controlHeight) * mazeHeight);

			Invalidate();
		}
	}
}
