using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject paddlePrefab;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private GameObject[] levels;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text ballsText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text highScore;

    [SerializeField] private GameObject panelMenu;
    [SerializeField] private GameObject panelPlay;
    [SerializeField] private GameObject panelLevelCompleted;
    [SerializeField] private GameObject panelGameOver;

    // What exactly are enums again?
    [SerializeField] public enum State { MENU, INIT, PLAY, LEVELCOMPLETED, LOADLEVEL, GAMEOVER}

    private State _state;
    private GameObject _currentBall;
    private GameObject _currentLevel;
    private GameObject _paddle;
    private bool _isSwitchingState;

    // Getters and Setters for the fields
    // Using getters and setters can increase performance. Update fields only when needed and not every Update loop.
    private int _score;
    public int Score { get { return _score; } set { _score = value; scoreText.text = $"Score: {_score}"; } }

  
    private int _level;
    public int Level { get { return _level;} set { _level = value; levelText.text = $"Level: {_level}"; } }

    private int _balls;
    public int Balls { get { return _balls;} set { _balls = value; ballsText.text = $"Balls: {_balls}"; } }

   // private int _highScore;
    //public int HighScore { get { return _highScore; } set { _highScore = value; highScore.text = $"HIGHSCORE: {_highScore}"; } }

    // Singleton of the GameManager
    // Easy way to allow other GameObjects to access to memeber vairables
    // Not the "best" design pattern, but works for simple games.
    public static GameManager Instance { get; private set; }
    

    public void PlayClicked()
    {
        SwitchState(State.INIT, 0f);
        
    }


    // Start is called before the first frame update
    void Start()
    {
        // Intialize the Singleton of the Gamemanger
        Instance = this;
        PlayerPrefs.DeleteKey("high_score"); // Clears the highscore for the session
        // On first load of game, see Main Menu
        SwitchState(State.MENU, 0f);
    }


    public void SwitchState(State newState, float delay)
    {
        
        StartCoroutine(SwitchDelay(newState, delay));
        //EndState();
        //_state = newState;
        //BeginState(newState);
        //_isSwitchingState = false;

    }


    // Coroutine example!
    // Adds a little delay between Game States
    IEnumerator SwitchDelay(State newState, float delay)
    {

        Debug.Log("SwitchDelay called");
        _isSwitchingState = true;
        yield return new WaitForSeconds(delay);
        EndState();
        _state = newState;
        BeginState(newState);
        _isSwitchingState = false;
    }

    private void BeginState(State newState)
    {
        switch (newState)
        {
            case State.MENU:
                Cursor.visible = true; // Hides the mouse cursor
                highScore.text = "HIGHSCORE: " + PlayerPrefs.GetInt("high_score");  
                panelMenu.SetActive(true);
                break;
            case State.INIT:
                Cursor.visible = false; // Hides the mouse cursor
                panelPlay.SetActive(true);
                Score = 0;
                Level = 0;
                Balls = 3;

                if(_currentLevel != null)
                {
                    Destroy(_currentLevel);
                }
                _paddle = Instantiate(paddlePrefab);
                SwitchState(State.LOADLEVEL, 0f);
                break;
            case State.PLAY:
                break;
            case State.LEVELCOMPLETED:
                Destroy(_currentBall);
                Destroy(_currentLevel);
                Level++;
                panelLevelCompleted.SetActive(true);
                SwitchState(State.LOADLEVEL, 2f);
                break;
            case State.LOADLEVEL:
                if(Level >= levels.Length)
                {
                    // User has finished the game
                    SwitchState(State.GAMEOVER, 0f);
                }
                else
                {
                    _currentLevel = Instantiate(levels[Level]);
                    SwitchState(State.PLAY, 0f);
                }

                break;
            case State.GAMEOVER:
                // Essentially SharedPres for Unity
                if(Score > PlayerPrefs.GetInt("high_score"))
                {
                    PlayerPrefs.SetInt("high_score", Score);
                }
                panelGameOver.SetActive(true);

                break;       

        }

      
    }

    // Update is called once per frame
    void Update()
    {
        switch (_state)
        {
            case State.MENU:
                break;
            case State.PLAY:
                Debug.Log("Inisde Update Play");
                Debug.Log($"Level child count = {_currentLevel.transform.childCount}");
                if (_currentBall == null)
                {
                    if(Balls > 0)
                    {
                       _currentBall = Instantiate(ballPrefab);
                    }
                    else
                    {
                        SwitchState(State.GAMEOVER, 0f);
                    }
                }

                //transform.childCount returns the number of childern under any Game Object
                // For me the Levels have Rows, which have all the Bricks.. So it's tricky
                // All Game Objects have a Transform.
                if(_currentLevel != null && _currentLevel.transform.GetChild(0).childCount == 0 && !_isSwitchingState)
                {
                    SwitchState(State.LEVELCOMPLETED, 0f);
                }
                break;
            case State.INIT:
                break;
            case State.LEVELCOMPLETED:
                break;
            case State.LOADLEVEL:
                break;
            case State.GAMEOVER:
                if(Input.anyKeyDown)
                {
                   
                    //Destroy(_currentBall);
                    //Destroy(_paddle);
                    //Destroy(_currentLevel);
                    SwitchState(State.MENU, 0f);
                }
                break;

        }
    }

    private void EndState()
    {
        switch (_state)
        {
            case State.MENU:
                panelMenu.SetActive(false);
                break;
            case State.PLAY:
                break;
            case State.LEVELCOMPLETED:
                panelLevelCompleted.SetActive(false);
                break;
            case State.LOADLEVEL:
                break;
            case State.GAMEOVER:
                panelPlay.SetActive(false);
                panelGameOver.SetActive(false);
                break;

        }
    }
}
