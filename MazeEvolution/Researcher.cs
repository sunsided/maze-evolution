using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
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
		private readonly List<Proband> _probanden;

        /// <summary>
        /// Initializes a new instance of the <see cref="Researcher"/> class.
        /// </summary>
        /// <param name="maze">The maze.</param>
        /// <param name="probanden">The probanden.</param>
        /// <remarks></remarks>
		public Researcher(Maze4 maze, IEnumerable<Proband> probanden)
		{
            Contract.Requires(maze != null, "Labyrinth darf nicht null sein");
            Contract.Requires(probanden != null, "Elementliste darf nicht null sein");

			Maze = maze;
			_probanden = new List<Proband>(probanden);

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
            try
            {
                // Liste der Probanden, die das Ziel erreicht haben
                IList<Proband> goalReached = new List<Proband>();
                const int targetCount = 10;

                // Cache
                Maze4 maze = Maze;

                // Distanzen ermitteln
                int[,] distances;
                var remoteRooms = maze.SetStartingPoint(0, 0, out distances);
                var mostDistant = remoteRooms[0];
                var coordinates = maze.GetPosition(mostDistant.Item2);

                // ELemente zurücksetzen
                _probanden.ForEach(proband => proband.Reset(coordinates.Item1, coordinates.Item2));

                // Hauptschleife beginnen
                ReportProgress(0);
                while (!CancellationPending)
                {
                    // Schleife durchlaufen
                    Parallel.For(0, _probanden.Count - 1, p =>
                                                              {
                                                                  if (CancellationPending) return;

                                                                  // Proband ermitteln
                                                                  Proband proband = _probanden[p];
                                                                  if (proband.TargetReached) return;

                                                                  // Generierte Funktion ausführen
                                                                  CodeExpression<Proband> code = proband.GeneticCode;
                                                                  Contract.Assert(code != null);
                                                                  code.Execute(proband);

                                                                  // Wenn das Ziel noch nicht erreicht ist, fortfahren
                                                                  if (!proband.TargetReached) return;

                                                                  // Ziel as erreicht markieren
                                                                  goalReached.Add(proband);
                                                                  ReportProgress(goalReached.Count*100/targetCount);
                                                              });
                }

                // Erfolg vermelden
                e.Result = (goalReached.Count == targetCount);
            }
            catch(ThreadAbortException)
            {
            }
            catch(Exception ex)
            {
                Debugger.Break();
            }
		}
	}
}
