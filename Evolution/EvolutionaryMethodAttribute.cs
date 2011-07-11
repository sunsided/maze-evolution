using System;

namespace Evolution
{
	/// <summary>
	/// Attribut für eine Methode, die im Ausdrucksbaum verwendet werden darf.
	/// <seealso cref="EvolutionaryClassAttribute"/>
	/// </summary>
	[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
	public sealed class EvolutionaryMethodAttribute : Attribute
	{
	}
}
