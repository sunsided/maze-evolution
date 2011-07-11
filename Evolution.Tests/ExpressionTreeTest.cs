using System;
using System.Diagnostics;
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
			Trace.WriteLine(func);

			func.Execute(obj);
		}

		[Test, Explicit]
		public void CrossoverTest()
		{
			var obj = new SimpleClass();
			var generator = new Generator<SimpleClass>();
			CodeExpression<SimpleClass> leftTree = generator.BuildRandomExpressionTree();
			CodeExpression<SimpleClass> rightTree = generator.BuildRandomExpressionTree();

			Trace.WriteLine("Vor dem Crossover");
			Trace.WriteLine("-----------------");
			Trace.WriteLine("");
			Trace.WriteLine("Tree links:");
			Trace.WriteLine(leftTree);

			Trace.WriteLine("");
			Trace.WriteLine("Tree rechts:");
			Trace.WriteLine(rightTree);

			generator.Crossover(ref leftTree, ref rightTree);

			Trace.WriteLine("");
			Trace.WriteLine("Nach dem Crossover");
			Trace.WriteLine("------------------");
			Trace.WriteLine("");
			Trace.WriteLine("Tree links:");
			Trace.WriteLine(leftTree);

			Trace.WriteLine("");
			Trace.WriteLine("Tree rechts:");
			Trace.WriteLine(rightTree);
		}

		[Test, Explicit] public void MutationTest()
		{
			var obj = new SimpleClass();
			var generator = new Generator<SimpleClass>();
			CodeExpression<SimpleClass> tree = generator.BuildRandomExpressionTree();

			Trace.WriteLine("Vor der Mutation");
			Trace.WriteLine("----------------");
			Trace.WriteLine("");
			Trace.WriteLine(tree);

			generator.Mutate(ref tree);

			Trace.WriteLine("");
			Trace.WriteLine("Nach der Mutation");
			Trace.WriteLine("-----------------");
			Trace.WriteLine("");
			Trace.WriteLine(tree);

		}
	}
}
