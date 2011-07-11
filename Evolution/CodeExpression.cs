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
	public class CodeExpression<T> where T : class
	{
		/// <summary>
		/// Die auszuführende Aktion
		/// </summary>
		public MethodInfo Terminal { get; internal set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="CodeExpression&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="terminal">The terminal.</param>
		public CodeExpression(MethodInfo terminal)
		{
			Contract.Requires(terminal != null);
			Terminal = terminal;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CodeExpression&lt;T&gt;"/> class.
		/// </summary>
		protected CodeExpression()
		{
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
			return String.Concat(Enumerable.Repeat("\t", indentationLevel)) + Terminal.Name + "();";
		}
	}
}
