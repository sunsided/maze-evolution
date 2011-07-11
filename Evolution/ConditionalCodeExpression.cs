using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;

namespace Evolution
{
	/// <summary>
	/// Klasse, die einen Codeausdruck beschreibt
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public sealed class ConditionalCodeExpression<T> : CodeExpression<T> where T: class
	{
		/// <summary>
		/// Die auszuführende Aktion
		/// </summary>
		public CodeExpression<T> LeftAction { get; internal set; }

		/// <summary>
		/// Die auszuführende Aktion
		/// </summary>
		public CodeExpression<T> RightAction { get; internal set; }

		/// <summary>
		/// Die auszuführende Aktion
		/// </summary>
		public MethodInfo Decision { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="CodeExpression&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="decision">The decision.</param>
		/// <param name="leftAction">The left action.</param>
		/// <param name="rightAction">The right action.</param>
		/// <remarks></remarks>
		public ConditionalCodeExpression(MethodInfo decision, CodeExpression<T> leftAction, CodeExpression<T> rightAction)
		{
			Contract.Requires(decision != null);
			Contract.Requires(leftAction != null);
			Contract.Requires(rightAction != null);
			LeftAction = leftAction;
			RightAction = rightAction;
			Decision = decision;
		}

		/// <summary>
		/// Führt die angegebene Aktion aus
		/// </summary>
		/// <param name="target">The target.</param>
		/// <remarks></remarks>
		public override void Execute(T target)
		{
			if ((bool)Decision.Invoke(target, null))
				LeftAction.Execute(target);
			else
				RightAction.Execute(target);
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents this instance.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents this instance.</returns>
		/// <remarks></remarks>
		public override string ToString()
		{
			return ToString(0);
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents this instance.
		/// </summary>
		/// <param name="indentationLevel">The indentation level.</param>
		/// <returns>A <see cref="System.String"/> that represents this instance.</returns>
		/// <remarks></remarks>
		internal override string ToString(int indentationLevel)
		{
			return String.Concat(Enumerable.Repeat("\t", indentationLevel)) + "if (" + Decision.Name + "()) {" +
			       Environment.NewLine +
				   LeftAction.ToString(indentationLevel + 1) + Environment.NewLine + "} " + Environment.NewLine +
			       "else { " + Environment.NewLine +
			       RightAction.ToString(indentationLevel + 1) + Environment.NewLine + "}";
		}
	}
}
