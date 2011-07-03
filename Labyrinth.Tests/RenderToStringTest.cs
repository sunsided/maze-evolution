using System;
using NUnit.Framework;

namespace Labyrinth.Tests
{
	[TestFixture]
	public class RenderToStringTest
	{
		[Test]
		public void TestRecursiveBacktracker()
		{
			IMazeGenerator generator = new RecursiveBacktracker4();
			var labyrinth = generator.Generate(30, 10);
			string rendered = labyrinth.RenderToString();
			Console.WriteLine(rendered);
		}

		[Test]
		public void TestRandomizedPrim()
		{
			IMazeGenerator generator = new RandomizedPrim4();
			var labyrinth = generator.Generate(30, 10);
			string rendered = labyrinth.RenderToString();
			Console.WriteLine(rendered);
		}
	}
}
