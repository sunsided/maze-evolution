using System;

namespace Evolution
{
	/// <summary>
	/// Ereignisparameter für einen Generationsreport
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <remarks></remarks>
	public class GenerationReportEventArgs<T> : EventArgs where T:class, ICodeProvider<T>, IFitnessProvider
	{
		/// <summary>
		/// Gets the report.
		/// </summary>
		/// <remarks></remarks>
		public GenerationReport<T> Report { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="GenerationReportEventArgs&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="report">The report.</param>
		/// <remarks></remarks>
		public GenerationReportEventArgs(GenerationReport<T> report)
		{
			Report = report;
		}
	}
}
