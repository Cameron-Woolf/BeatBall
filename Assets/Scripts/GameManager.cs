using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject paddlePrefab;
    [SerializeField] private GameObject ballPrefab;

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
       
    public void PlayClicked()
    {
        SwitchState(State.INIT);
    }

    // Start is called before the first frame update
    void Start()
    {
        // On first load of game, see Main Menu
        SwitchState(State.MENU);
    }


    public void SwitchState(State newState)
    {
        EndState();
        BeginState(newState);
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
                //panelLevelCompleted.SetActive(true);
                break;
            case State.PLAY:
                break;
            case State.LEVELCOMPLETED:           
                break;
            case State.LOADLEVEL:
                break;
            case State.GAMEOVER:
              
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
                break;

        }
    }
}
