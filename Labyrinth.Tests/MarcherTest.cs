using NUnit.Framework;

namespace Labyrinth.Tests
{
	[TestFixture]
	public class MarcherTests
	{
		[Test]
		public void TestTurnLeft()
		{
			Marcher4 marcher = new Marcher4(Door4.North, 0, 0);
			Assert.AreEqual(Door4.North, marcher.Direction);
			
			Assert.AreEqual(Door4.West, marcher.LeftDirection);
			marcher.TurnLeft();
			Assert.AreEqual(Door4.West, marcher.Direction);

			Assert.AreEqual(Door4.South, marcher.LeftDirection);
			marcher.TurnLeft();
			Assert.AreEqual(Door4.South, marcher.Direction);

			Assert.AreEqual(Door4.East, marcher.LeftDirection);
			marcher.TurnLeft();
			Assert.AreEqual(Door4.East, marcher.Direction);

			Assert.AreEqual(Door4.North, marcher.LeftDirection);
			marcher.TurnLeft();
			Assert.AreEqual(Door4.North, marcher.Direction);
		}

		[Test]
		public void TestTurnRight()
		{
			Marcher4 marcher = new Marcher4(Door4.North, 0, 0);
			Assert.AreEqual(Door4.North, marcher.Direction);

			Assert.AreEqual(Door4.East, marcher.RightDirection);
			marcher.TurnRight();
			Assert.AreEqual(Door4.East, marcher.Direction);

			Assert.AreEqual(Door4.South, marcher.RightDirection);
			marcher.TurnRight();
			Assert.AreEqual(Door4.South, marcher.Direction);

			Assert.AreEqual(Door4.West, marcher.RightDirection);
			marcher.TurnRight();
			Assert.AreEqual(Door4.West, marcher.Direction);

			Assert.AreEqual(Door4.North, marcher.RightDirection);
			marcher.TurnRight();
			Assert.AreEqual(Door4.North, marcher.Direction);
		}

		[Test]
		public void TestTurnAround()
		{
			// Nord-Süd
			{
				Marcher4 marcher = new Marcher4(Door4.North, 0, 0);
				Assert.AreEqual(Door4.North, marcher.Direction);

				marcher.TurnAround();
				Assert.AreEqual(Door4.South, marcher.Direction);

				marcher.TurnAround();
				Assert.AreEqual(Door4.North, marcher.Direction);
			}

			// Ost-West
			{
				Marcher4 marcher = new Marcher4(Door4.West, 0, 0);
				Assert.AreEqual(Door4.West, marcher.Direction);

				marcher.TurnAround();
				Assert.AreEqual(Door4.East, marcher.Direction);

				marcher.TurnAround();
				Assert.AreEqual(Door4.West, marcher.Direction);
			}
		}
	}
}
