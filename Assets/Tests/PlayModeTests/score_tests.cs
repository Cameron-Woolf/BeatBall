using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class score_tests
{
    [OneTimeSetUp]
    public void sceneSetUp()
    {
        SceneManager.LoadScene("SampleScene");

    }

    // A Test behaves as an ordinary method
    [UnityTest]
    public IEnumerator score_increase_by_100_with_1_level_0_brick_break()
    {
        // ARRANGE
        test_setup();
        yield return new WaitForSeconds(1f);
        var score = GameManager.Instance.Score;
        var brick = GameObject.FindObjectOfType<Brick>();

        // ACT
        brick.breakBrick();

        // ASSERT
        Assert.AreEqual(score + 100, GameManager.Instance.Score);



    }

    [UnityTest]
    public IEnumerator score_increases_by_500_with_5_level_1_brick_breaks()
    {
        // ARRANGE
        test_setup();
        yield return new WaitForSeconds(0.5f);
        load_level_1();
        yield return new WaitForSeconds(1f);
        var score = GameManager.Instance.Score;

        // ACT
        int brick_count = 5;
        int score_increase = 500;
        while (brick_count > 0)
        {
            //var brick = GameObject.Find($"Brick ({brick_count})");
            var brick = GameObject.FindObjectOfType<Brick>();
            brick.breakBrick();
            brick_count--;
        }

        // ASSERT
        Assert.AreEqual(score + score_increase, GameManager.Instance.Score);
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
