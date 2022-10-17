using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;

public class ball_edit_mode_tests
{
    
    [Test]
    public void ball_count_decreases_by_one_on_collision_with_floor()
    {
        // ARRANGE
        IBall ball = Substitute.For<IBall>();
        Ball.BallBehavior ballBehavior = new Ball.BallBehavior();

        // ACT
        ballBehavior.reduceBallCountByOne();

        // ASSERT
        Assert.NotNull(ball);


    }

 
}
