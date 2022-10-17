using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class ball_count_tests
{

    private Ball _ball;
    private GameManager _gameManager;

    [OneTimeSetUp]
    public void sceneSetUp()
    {
        SceneManager.LoadScene("SampleScene");


    }

    // Called before each TestCase
    [SetUp]
    public void Setup()
    {
       
    }

    [UnityTest]
    public IEnumerator ball_count_decreases_by_one_on_collision_with_floor()
    {
        // ARRANGE
        cleanLaunch();
        yield return new WaitForSeconds(1f);
        var ballCount = GameManager.Instance.Balls;
        Debug.Log("Ball Test" + ballCount);
        var ballGameObject = GameObject.FindObjectOfType<Ball>();
        relocatePaddle();

        // ACT
        // Wait for ball to hit the ground
        yield return new WaitForSeconds(3f);


        // ASSERT
        Assert.AreEqual(ballCount-1, GameManager.Instance.Balls);


    }

    [UnityTest]
    public IEnumerator game_over_if_ball_count_is_zero()
    {
        // ARRANGE
        cleanLaunch();
        yield return new WaitForSeconds(1f);
        var ballGameObject = GameObject.FindObjectOfType<Ball>();
        // Zero the paddle and move it out of the way of the Ball
        relocatePaddle(); 


        // ACT
        // Reduce ball count to 1 and wait for it to hit the ground.
        GameManager.Instance.Balls--;
        GameManager.Instance.Balls--;
     
        yield return new WaitForSeconds(2.0f);

        // ASSERT
        Assert.AreEqual(GameManager.State.GAMEOVER, GameManager.Instance.getState());


    }

    public void cleanLaunch()
    {
        GameManager.Instance.SwitchState(GameManager.State.MENU, 0.5f);
        GameManager.Instance.SwitchState(GameManager.State.INIT, 0.5f);
    }

    public void relocatePaddle()
    {
        var paddleGameObhject = GameObject.FindObjectOfType<Paddel>();

        // Zero the paddle and move it out of the way of the Ball
        paddleGameObhject.transform.position = new Vector3(0, 0, 0);
        paddleGameObhject.transform.position = new Vector3(-10, 0, 0);

    }
}
