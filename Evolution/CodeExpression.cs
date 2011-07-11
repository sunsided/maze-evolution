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
	public class CodeExpression<T> where T : class
	{
		/// <summary>
		/// Der Elternknoten
		/// </summary>
		public CodeExpression<T> Parent { get; internal set; }

		/// <summary>
		/// Die auszuführende Aktion
		/// </summary>
		public MethodInfo Terminal { get; internal set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="CodeExpression&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="parent">Der Elternknoten</param>
		/// <param name="terminal">The terminal.</param>
		public CodeExpression(CodeExpression<T> parent, MethodInfo terminal)
			: this(parent)
		{
			Contract.Requires(terminal != null);
			Terminal = terminal;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodeExpression&lt;T&gt;"/> class.
		/// </summary>
		protected CodeExpression(CodeExpression<T> parent)
		{
			Parent = parent;
		}

		/// <summary>
		/// Führt die angegebene Aktion aus
		/// </summary>
		/// <param name="target">The target.</param>
		/// <remarks></remarks>
		public virtual void Execute(T target)
		{
			Terminal.Invoke(target, null);
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
		internal virtual string ToString(int indentationLevel)
		{
			return String.Concat(Enumerable.Repeat("    ", indentationLevel)) + Terminal.Name + "();";
		}

		/// <summary>
		/// Liefert eine Liste aller <see cref="CodeExpression{}"/>-Knoten unterhalb dieses Elementes
		/// </summary>
		/// <returns></returns>
		public virtual IEnumerable<CodeExpression<T>> GetChildNodes()
		{
			yield return this;
		}

		/// <summary>
		/// Gibt die Tiefe des Teilbaumes zurück
		/// </summary>
		/// <returns></returns>
		public virtual int GetDepth()
		{
			return 1;
		}

		/// <summary>
		/// Clones this instance.
		/// </summary>
		/// <returns></returns>
		/// <remarks></remarks>
		public virtual CodeExpression<T> Clone()
		{
			return new CodeExpression<T>(Parent, Terminal);
		}
	}
}
