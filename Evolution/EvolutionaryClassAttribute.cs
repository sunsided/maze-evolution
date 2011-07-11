using System;

namespace Evolution
{
	/// <summary>
	/// Attribut für eine Klasse, für die ein Ausdrucksbaum erstellt werden kann.
	/// <seealso cref="EvolutionaryMethodAttribute"/>
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
	public sealed class EvolutionaryClassAttribute : Attribute
	{
	}
}
