using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Evolution
{
    /// <summary>
    /// Report über die Generationsveränderung
    /// </summary>
    /// <typeparam name="TElement">Der Elementtyp</typeparam>
    public sealed class GenerationReport<TElement> where TElement : class
    {
        /// <summary>
        /// Die Elemente der alten Generation
        /// </summary>
        public ICollection<TElement> OldGeneration { get; private set; }

        /// <summary>
        /// Die selektierten Elemente
        /// </summary>
        public ICollection<TElement> SelectedElements { get; private set; }

        /// <summary>
        /// Die verworfenenen Elemente
        /// </summary>
        public ICollection<TElement> DeceasedElements { get; private set; }

        /// <summary>
        /// Die mutierten Elemente
        /// </summary>
        public ICollection<Tuple<TElement, CodeExpression<TElement>>> MutatedElements { get; private set; }

        /// <summary>
        /// Die gekreuzten Elemente
        /// </summary>
        public ICollection<Tuple<TElement, TElement, CodeExpression<TElement>>> CrossedElements { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerationReport&lt;TElement&gt;"/> class.
        /// </summary>
        /// <param name="oldGeneration">The old generation.</param>
        /// <param name="selectedElements">The selected elements.</param>
        /// <param name="deceasedElements">The deceased elements.</param>
        /// <param name="mutatedElements">The mutated elements.</param>
        /// <param name="crossedElements">The crossed elements.</param>
        public GenerationReport(
            IEnumerable<TElement> oldGeneration,
            IEnumerable<TElement> selectedElements,
            IEnumerable<TElement> deceasedElements,
            IEnumerable<Tuple<TElement, CodeExpression<TElement>>> mutatedElements,
            IEnumerable<Tuple<TElement, TElement, CodeExpression<TElement>>> crossedElements)
        {
            Contract.Requires(oldGeneration != null);
            Contract.Requires(selectedElements != null);
            Contract.Requires(deceasedElements != null);
            Contract.Requires(mutatedElements != null);
            Contract.Requires(crossedElements != null);

            OldGeneration = new HashSet<TElement>(oldGeneration);
            SelectedElements = new HashSet<TElement>(selectedElements);
            DeceasedElements = new HashSet<TElement>(deceasedElements);
            MutatedElements = new HashSet<Tuple<TElement, CodeExpression<TElement>>>(mutatedElements);
            CrossedElements = new HashSet<Tuple<TElement, TElement, CodeExpression<TElement>>>(crossedElements);
        }
    }
}
