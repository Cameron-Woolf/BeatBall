using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class game_manager_tests
{


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
    public IEnumerator game_manager_loads_successfully()
    {
        // ARRANGE
        test_setup();

        // ACT
        var gameManger = GameManager.Instance;


        // ASSERT
        Assert.NotNull(gameManger);


        yield return new WaitForSeconds(1f);
    }

    [UnityTest]
    public IEnumerator game_manager_transitions_to_play_succesfully()
    {

        // ARRANGE
        test_setup();

        // ACT
        yield return new WaitForSeconds(1f);

        // ASSERT
        Assert.AreEqual(GameManager.State.PLAY, GameManager.Instance.getState());

    }

    [UnityTest]
    public IEnumerator game_manager_initilizes_ball_succesfully()
    {
        // ARRANGE
        test_setup();

        // ACT
        yield return new WaitForSeconds(2f);
        var _ballGameObject = GameObject.FindObjectOfType<Ball>();

        // ASSERT
        Assert.NotNull(_ballGameObject);

    }

    [UnityTest]
    public IEnumerator game_manager_initilizes_paddle_successfully()
    {
        // ARRANGE
        test_setup();

        // ACT
        yield return new WaitForSeconds(1f);
        var _paddleGameObject = GameObject.FindObjectOfType<Paddel>();

        // ASSERT
        Assert.NotNull(_paddleGameObject);
    
    }

    [UnityTest]
    public IEnumerator game_manager_initilizes_walls_successfully()
    {
        // ARRANGE
        test_setup();
        yield return new WaitForSeconds(1f);

        // ACT
        var _floorGameObject = GameObject.Find("Floor");
        var _leftWallGameObject = GameObject.Find("LWall");
        var _rightWallGameObject = GameObject.Find("RWall");
        var _ceilingGameObject = GameObject.Find("Ceiling");

        // ASSERT
        Assert.NotNull(_floorGameObject);
        Assert.NotNull(_leftWallGameObject);
        Assert.NotNull(_rightWallGameObject);
        Assert.NotNull(_ceilingGameObject);
        
    }

    [UnityTest]
    public IEnumerator game_manager_transitions_to_next_level_succesfully()
    {
        // ARRANGE
        test_setup();

        // ACT
        yield return new WaitForSeconds(1f);
        GameManager.Instance.SwitchState(GameManager.State.LEVELCOMPLETED, 0.5f);
        yield return new WaitForSeconds(4f);

        // ASSERT
        Assert.AreEqual(GameManager.State.PLAY, GameManager.Instance.getState());

    }

    [UnityTest]
    public IEnumerator game_manager_transitions_to_game_over_after_all_levels_completed()
    {
        // ARRANGE
        test_setup();
        yield return new WaitForSeconds(2f);

        // ACT
        // Since there are only 2 Levels, Switching to LEVELCOMPLETED twice should trigger GAMEOVER
        GameManager.Instance.SwitchState(GameManager.State.LEVELCOMPLETED, 0.5f);
        yield return new WaitForSeconds(4f);
        GameManager.Instance.SwitchState(GameManager.State.LEVELCOMPLETED, 0.5f);
        yield return new WaitForSeconds(4f);

        // ASSERT
        Assert.AreEqual(GameManager.State.GAMEOVER, GameManager.Instance.getState());

    }

    public void test_setup()
    {
      
        GameManager.Instance.SwitchState(GameManager.State.INIT, 0.5f);
     
    }

    
    //[TearDown]
    //public void TearDown()
    //{
    //    SceneManager.UnloadSceneAsync("SampleScene");
    //}


    //private void transition_to_play_state()
    //{
    //    GameManager.Instance.SwitchState(GameManager.State.INIT, 1.0f);
    //}
}
