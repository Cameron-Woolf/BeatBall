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

    [SerializeField] private GameObject panelMenu;
    [SerializeField] private GameObject panelPlay;
    [SerializeField] private GameObject panelLevelCompleted;
    [SerializeField] private GameObject panelGameOver;

    // What exactly are enums again?
    [SerializeField] public enum State { MENU, INIT, PLAY, LEVELCOMPLETED, LOADLEVEL, GAMEOVER}

    private State _state;
    private GameObject _currentBall;
    private GameObject _currentLevel;
    private bool _isSwitchingState;

    // Getters and Setters for the fields
    // Using getters and setters can increase performance. Update fields only when needed and not every Update loop.
    private int _score;
    public int Score { get { return _score; } set { _score = value; scoreText.text = $"Score: {_score}"; } }

  
    private int _level;
    public int Level { get { return _level;} set { _level = value; levelText.text = $"Level: {_level}"; } }

    private int _balls;
    public int Balls { get { return _balls;} set { _balls = value; ballsText.text = $"Balls: {_balls}"; } }

    // Singleton of the GameManager
    // Easy way to allow other GameObjects to access to memeber vairables
    // Not the "best" design pattern, but works for simple games.
    public static GameManager Instance { get; private set; }
    

    public void PlayClicked()
    {
        SwitchState(State.INIT);
        
    }


    // Start is called before the first frame update
    void Start()
    {
        // Intialize the Singleton of the Gamemanger
        Instance = this;
        // On first load of game, see Main Menu
        SwitchState(State.MENU);
    }


    public void SwitchState(State newState)
    {
        //SwitchDelay(newState, 2);
        EndState();
        _state = newState;
        BeginState(newState);
    }


    // Coroutine example!
    // Adds a little delay between Game States
    IEnumerator SwitchDelay(State newState, float delay)
    {
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
                panelMenu.SetActive(true);
                break;
            case State.INIT:
                panelPlay.SetActive(true);
                Score = 0;
                Level = 0;
                Balls = 3;
                Instantiate(paddlePrefab);
                SwitchState(State.LOADLEVEL);
                break;
            case State.PLAY:
                break;
            case State.LEVELCOMPLETED:
                panelLevelCompleted.SetActive(true);
                break;
            case State.LOADLEVEL:
                if(Level >= levels.Length)
                {
                    // User has finished the game
                    SwitchState(State.GAMEOVER);
                }
                else
                {
                    _currentLevel = Instantiate(levels[Level]);
                    SwitchState(State.PLAY);
                }

                break;
            case State.GAMEOVER:
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
                if(_currentBall == null)
                {
                    if(Balls > 0)
                    {
                       _currentBall = Instantiate(ballPrefab);
                    }
                    else
                    {
                        SwitchState(State.GAMEOVER);
                    }
                }
                break;
            case State.INIT:
                break;
            case State.LEVELCOMPLETED:
                break;
            case State.LOADLEVEL:
                break;
            case State.GAMEOVER:
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
