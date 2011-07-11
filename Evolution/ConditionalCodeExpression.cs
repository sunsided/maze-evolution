using System;
using System.Collections.Generic;
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
		/// <param name="parent">The parent.</param>
		/// <param name="decision">The decision.</param>
		/// <param name="leftAction">The left action.</param>
		/// <param name="rightAction">The right action.</param>
		/// <remarks></remarks>
		public ConditionalCodeExpression(CodeExpression<T> parent, MethodInfo decision, CodeExpression<T> leftAction, CodeExpression<T> rightAction)
			: base(parent)
		{
			Contract.Requires(decision != null);
			Contract.Requires(leftAction != null);
			Contract.Requires(rightAction != null);
			LeftAction = leftAction;
			RightAction = rightAction;
			Decision = decision;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodeExpression&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="parent">The parent.</param>
		/// <param name="decision">The decision.</param>
		/// <remarks></remarks>
		public ConditionalCodeExpression(CodeExpression<T> parent, MethodInfo decision)
			: base(parent)
		{
			Contract.Requires(decision != null);
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
			string padding = String.Concat(Enumerable.Repeat("    ", indentationLevel));
			return padding + "if (" + Decision.Name + "()) {" +
			       Environment.NewLine +
				   LeftAction.ToString(indentationLevel + 1) + Environment.NewLine + padding + "} " + Environment.NewLine + padding +
			       "else { " + Environment.NewLine +
			       RightAction.ToString(indentationLevel + 1) + Environment.NewLine + padding + "}";
		}

		/// <summary>
		/// Liefert eine Liste aller <see cref="CodeExpression{}"/>-Knoten unterhalb dieses Elementes
		/// </summary>
		/// <returns></returns>
		public override IEnumerable<CodeExpression<T>> GetChildNodes()
		{
			yield return this;
			foreach (var node in LeftAction.GetChildNodes())
			{
				yield return node;
			}
			foreach (var node in RightAction.GetChildNodes())
			{
				yield return node;
			}
		}

		/// <summary>
		/// Gibt die Tiefe des Teilbaumes zurück
		/// </summary>
		/// <returns></returns>
		public override int GetDepth()
		{
			return Math.Max(LeftAction.GetDepth(), RightAction.GetDepth()) + 1;
		}
	}
}
