using System;
using NUnit.Framework;

namespace Labyrinth.Tests
{
	[TestFixture]
	public class RenderTest
	{
		[Test]
		public void TestRenderToString()
		{
			RecursiveBacktracker4 generator = new RecursiveBacktracker4();
			var labyrinth = generator.Generate(30, 10);
			string rendered = labyrinth.RenderToString();
			Console.WriteLine(rendered);
		}
	}
}
