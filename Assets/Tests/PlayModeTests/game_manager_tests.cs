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
    public IEnumerator a_game_manager_loads_successfully()
    {
        var gameManger = GameManager.Instance;

        Assert.NotNull(gameManger);


        yield return new WaitForSeconds(1f);
    }

    [UnityTest]
    public IEnumerator b_game_manager_transitions_to_play_succesfully()
    {

        GameManager.Instance.SwitchState(GameManager.State.INIT, 0.5f);


        yield return new WaitForSeconds(1f);

        Assert.AreEqual(GameManager.State.PLAY, GameManager.Instance.getState());

    }

    [UnityTest]
    public IEnumerator c_game_manager_initilizes_ball_succesfully()
    {

        yield return new WaitForSeconds(0.5f);
        var _ballGameObject = GameObject.FindObjectOfType<Ball>();
        Assert.NotNull(_ballGameObject);

    }

    [UnityTest]
    public IEnumerator d_game_manager_initilizes_paddle_successfully()
    {

        yield return new WaitForSeconds(1f);
   

        var _paddleGameObject = GameObject.FindObjectOfType<Paddel>();
        Assert.NotNull(_paddleGameObject);
    
    }

    [UnityTest]
    public IEnumerator e_game_manager_initilizes_walls_successfully()
    {

        yield return new WaitForSeconds(1f);

        var _floorGameObject = GameObject.Find("Floor");
        var _leftWallGameObject = GameObject.Find("LWall");
        var _rightWallGameObject = GameObject.Find("RWall");
        var _ceilingGameObject = GameObject.Find("Ceiling");
        Assert.NotNull(_floorGameObject);
        Assert.NotNull(_leftWallGameObject);
        Assert.NotNull(_rightWallGameObject);
        Assert.NotNull(_ceilingGameObject);
        
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
