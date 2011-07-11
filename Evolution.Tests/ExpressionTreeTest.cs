using System;
using System.Linq.Expressions;
using Evolution.Tests.TestClasses;
using NUnit.Framework;

namespace Evolution.Tests
{
	/// <summary>
	/// Testet die Auswertung der Klassen und das Generieren des Baumes
	/// </summary>
	[TestFixture]
	public class ExpressionTreeTest
	{
		/// <summary>
		/// Überprüft, ob beim Auswerten der Klassen die richtigen Exceptions geworfen werden.
		/// </summary>
		[Test]
		public void TestAttributeMarkExceptions()
		{
			Assert.Throws<ClassDecorationException>(() => new Generator<UnmarkedClass>());
			Assert.Throws<MethodSignatureException>(() => new Generator<InvalidSignatureClass>());
			Assert.DoesNotThrow(() => new Generator<SimpleClass>());
		}

		[Test, Explicit]
		public void GenerateExpressionTree()
		{
			var obj = new SimpleClass();
			var generator = new Generator<SimpleClass>();
			var func = generator.BuildRandomExpressionTree();

			func.Execute(obj);
		}
	}
}
