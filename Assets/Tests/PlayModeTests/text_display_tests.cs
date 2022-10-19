using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class text_display_tests
{

    [OneTimeSetUp]
    public void sceneSetUp()
    {
        SceneManager.LoadScene("SampleScene");

    }

    [UnityTest]
    public IEnumerator correct_ball_count_is_displayed_on_play_panel()
    {
        // ARRANGE
        test_setup();
        yield return new WaitForSeconds(1f);

        // ACT
        GameManager.Instance.Balls = 2;
        var currentBallCount = GameManager.Instance.Balls;
        var ballCountTMPro = GameObject.Find("TextBalls");
        var ballCountText = ballCountTMPro.GetComponent<TextMeshProUGUI>().text;

        // ARRANGE
        Assert.AreEqual("Balls: " + currentBallCount, ballCountText);
    }

    [UnityTest]
    public IEnumerator correct_level_is_displayed_on_play_panel()
    {
        // ARRANGE
        test_setup();
        yield return new WaitForSeconds(1f);
        load_level_1();
        yield return new WaitForSeconds(1f);

        // ACT
        var currentLevel = GameManager.Instance.Level;
        var levelTMPro = GameObject.Find("TextLevel");
        var levelText = levelTMPro.GetComponent<TextMeshProUGUI>().text;

        // ASSERT
        Assert.NotNull(levelTMPro);
        Assert.AreEqual("Level: " + currentLevel, levelText);

    }

    [UnityTest]
    public IEnumerator correct_score_is_displayed_on_play_panel()
    {
        // ARRANGE
        test_setup();
        yield return new WaitForSeconds(1f);
       
        // ACT
        var score = GameManager.Instance.Score = 500;
        var scoreTMPro = GameObject.Find("TextScore");
        var scoreText = scoreTMPro.GetComponent<TextMeshProUGUI>().text;

        // ASSERT
        Assert.AreEqual("Score: " + score, scoreText);

    }

    [UnityTest]
    public IEnumerator correct_high_score_is_shown_on_menu_panel()
    {
        // ARRANGE
        test_setup();
        yield return new WaitForSeconds(1f);

        // ACT
        GameManager.Instance.Score = 500;
        GameManager.Instance.SwitchState(GameManager.State.GAMEOVER, 1f);
        yield return new WaitForSeconds(1f);
        GameManager.Instance.SwitchState(GameManager.State.MENU, 1f);
        yield return new WaitForSeconds(3f);

        // Get the score from the GUI
        var highScoreTMPro = GameObject.Find("TextHighScore");
        var scoreText = highScoreTMPro.GetComponent<TextMeshProUGUI>().text;


        // ASSERT
        Assert.AreEqual("HIGHSCORE: " + GameManager.Instance.Score, scoreText);



    }

    public void test_setup()
    {
        GameManager.Instance.SwitchState(GameManager.State.MENU, 0.5f);
        GameManager.Instance.SwitchState(GameManager.State.INIT, 0.5f);

    }

    public void load_level_1()
    {

        GameManager.Instance.Level = 1;
        GameManager.Instance.SwitchState(GameManager.State.LOADLEVEL, 0.5f);

    }
}

