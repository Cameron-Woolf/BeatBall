using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BallTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void BallTestsSimplePasses()
    {
        // Use the Assert class to test conditions
       
        Assert.AreEqual(0,0);
        var gameManger = new GameManager();
        Assert.AreEqual(gameManger.Score, 0);
        var ball = new Ball();
        Assert.AreNotSame(ball, gameManger);


    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator BallTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
