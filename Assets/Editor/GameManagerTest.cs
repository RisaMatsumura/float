using System;
using NUnit.Framework;
using UnityEngine;

public class GameManagerTest{
		
	[Test]
	public void testGetLivesAsSymbol(){
		GameManager myGameManager = new GameManager();
		myGameManager.lives = 1;
		Assert.AreEqual (myGameManager.getLivesAsSymbols (), "•");
		myGameManager.lives = 10;
		Assert.AreEqual (myGameManager.getLivesAsSymbols (), "••••••••••");
	}

	[Test]
	public void testCalculateGameLevel() {
		GameManager myGameManager = new GameManager();
		Assert.AreEqual (myGameManager.calculateGameLevel (100, 100), 1);
	}

	[Test]
	public void testCalculateDiscSpeed() {
		GameManager myGameManager = new GameManager();
		Assert.AreEqual (myGameManager.calculateDiscSpeed(10f, 0.5f, 1), 10.5f);
	}

	[Test]
	public void testCalculateLevelBackgroundColors () {
		GameManager myGameManager = new GameManager();
		Color[] colors = {myGameManager.colorList [1], myGameManager.colorList [2]};
		Assert.AreEqual (myGameManager.calculateLevelBackgroundColors(1, myGameManager.colorList), colors);
	}

	[Test]
	public void testCalculateParticleSpeed() {
		GameManager myGameManager = new GameManager();
		Assert.AreEqual (myGameManager.calculateParticleSpeed (10f, 2), 5f);
	}

	[Test]
	public void testSetBackgroundColor() {
		GameManager myGameManager = new GameManager();
		Color red    = new Color (0xab/255.0F, 0x46/255.0F, 0x42/255.0F);
		myGameManager.setBackground (myGameManager.getCamera(), red);
		Assert.AreEqual (myGameManager.getCamera ().backgroundColor, red);
	}
}
