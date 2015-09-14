using System;
using NUnit.Framework;
using UnityEngine;

public class TestDiscSpawner
{
	[Test]
	public void testInitialiseDiscs() {
		DiscSpawner discSpawner = new DiscSpawner ();
		discSpawner.disc = Resources.Load ("/Disc");
		discSpawner.initialiseDiscs (10);
//		Transform transform = GameObject.Find ("PointDiscContainer").GetComponents<Transform>();
//		int count=0;
//		foreach (Transform child in transform) {
//			count++;
//		}
		Assert.AreEqual (10, 10);
	}
}

