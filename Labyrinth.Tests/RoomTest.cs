using System;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;

namespace Labyrinth.Tests
{
	[TestFixture]
	public class RoomTests
	{
		[Test]
		public void TestLinking()
		{
			{
				Room4 room = new Room4();
				Assert.IsFalse(room.HasNorthRoom);
				Assert.IsFalse(room.HasNorthRoom);
				Assert.IsFalse(room.HasNorthRoom);
				Assert.IsFalse(room.HasNorthRoom);

				Assert.AreEqual(Door4.None, room.Doors);
				Assert.AreEqual(Wall4.All, room.Walls);
			}

			{
				Room4 room = new Room4();
				room.NorthRoom = new Room4();
				Assert.IsTrue(room.HasNorthRoom);
				Assert.IsFalse(room.HasSouthRoom);
				Assert.IsFalse(room.HasEastRoom);
				Assert.IsFalse(room.HasWestRoom);

				Assert.AreEqual(Door4.North, room.Doors);
				Assert.AreEqual(Wall4.South | Wall4.East | Wall4.West, room.Walls);
			}

			{
				Room4 room = new Room4();
				room.SouthRoom = new Room4();
				Assert.IsFalse(room.HasNorthRoom);
				Assert.IsTrue(room.HasSouthRoom);
				Assert.IsFalse(room.HasEastRoom);
				Assert.IsFalse(room.HasWestRoom);

				Assert.AreEqual(Door4.South, room.Doors);
				Assert.AreEqual(Wall4.North | Wall4.East | Wall4.West, room.Walls);
			}

			{
				Room4 room = new Room4();
				room.WestRoom = new Room4();
				Assert.IsFalse(room.HasNorthRoom);
				Assert.IsFalse(room.HasSouthRoom);
				Assert.IsFalse(room.HasEastRoom);
				Assert.IsTrue(room.HasWestRoom);

				Assert.AreEqual(Door4.West, room.Doors);
				Assert.AreEqual(Wall4.South | Wall4.East | Wall4.North, room.Walls);
			}

			{
				Room4 room = new Room4();
				room.EastRoom = new Room4();
				Assert.IsFalse(room.HasNorthRoom);
				Assert.IsFalse(room.HasSouthRoom);
				Assert.IsTrue(room.HasEastRoom);
				Assert.IsFalse(room.HasWestRoom);

				Assert.AreEqual(Door4.East, room.Doors);
				Assert.AreEqual(Wall4.South | Wall4.North | Wall4.West, room.Walls);
			}
		}
	}
}
