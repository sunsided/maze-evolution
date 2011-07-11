using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Evolution;
using Labyrinth;

namespace MazeEvolution
{
	/// <summary>
	/// Lässt die Probanden durch das Labyrinth laufen
	/// </summary>
	public sealed class Researcher : BackgroundWorker
	{
		/// <summary>
		/// Das Ziel
		/// </summary>
		public Maze4 Maze { get; set; }

		/// <summary>
		/// Die Probanden
		/// </summary>
		private readonly IList<Proband> _probanden;

		/// <summary>
		/// Die genetisch erzeugten Algorithmen
		/// </summary>
		private readonly IList<CodeExpression<Proband>> _codes;

		/// <summary>
		/// Initializes a new instance of the <see cref="Researcher"/> class.
		/// </summary>
		/// <param name="probanden">Die Probanden.</param>
		/// <param name="codes">Die Codes.</param>
		/// <param name="timeoutInSeconds">Der Timeout in Sekunden, nach dem der Durchlauf abgebrochen wird</param>
		/// <remarks></remarks>
		public Researcher(Maze4 maze, IList<Proband> probanden, IList<CodeExpression<Proband>> codes)
		{
			Maze = maze;
			_probanden = probanden;
			_codes = codes;

			WorkerReportsProgress = true;
			WorkerSupportsCancellation = true;
		}

		/// <summary>
		/// Raises the <see cref="E:System.ComponentModel.BackgroundWorker.DoWork"/> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
		/// <remarks></remarks>
		protected override void OnDoWork(DoWorkEventArgs e)
		{
			// Liste der Probanden, die das Ziel erreicht haben
			IList<Proband> goalReached = new List<Proband>();
			const int targetCount = 10;

			Maze4 maze = Maze;

			int[,] distances;
			var remoteRooms = maze.SetStartingPoint(0, 0, out distances);
			var mostDistant = remoteRooms[0];
			var coordinates = maze.GetPosition(mostDistant.Item2);
			for (int p = 0; p < _probanden.Count; ++p )
			{
				_probanden[p].Reset(coordinates.Item1, coordinates.Item2);
			}

			ReportProgress(0);
			while (true /* goalReached.Count < targetCount*/)
			{
				if (CancellationPending) break;

				ParallelLoopResult result = Parallel.For(0, _probanden.Count-1, p =>
				{
					if (CancellationPending) return;

					Proband proband = _probanden[p];
					if (proband.TargetReached) return;

					CodeExpression<Proband> code = _codes[p];
					code.Execute(proband);
					if (proband.TargetReached)
					{
						goalReached.Add(proband);
						ReportProgress(goalReached.Count * 100 / targetCount);
					}
				});

				while (!result.IsCompleted) Thread.Sleep(100);
			}

			// Erfolg vermelden
			e.Result = (goalReached.Count == targetCount);
		}
	}
}
