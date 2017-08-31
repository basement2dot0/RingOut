using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //this class should be loaded with the stage in an empty game object
    [SerializeField]
    private float Rounds;
    [SerializeField]
    private float Match;
    [SerializeField]
    private Player[] players;
    [SerializeField]
    private Text ringOutText;
    private InputManager inputManager;
    private bool isPaused;
    private Text pauseText;


    private void Awake()
    {
        players = new Player[2];
        foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (player.GetComponent<Player>().ID == 1)
                players[0] = player.GetComponent<Player>();
            else
                players[1] = player.GetComponent<Player>();
        }
    }
    private void Start()
    {
        Rounds = 0;
        ringOutText.text = "";
        
    }
    private void Update()
    {
        PauseMenu();
       if (players[0].IsHypeHit|| players[1].IsHypeHit)
            ringOutText.text = "RING OUT!";


    }



    private void PauseMenu()
    {
        if (inputManager.PauseButton(players[0].ID) || inputManager.PauseButton(players[1].ID))
        {
           if(!isPaused)
            {
                Time.timeScale = 0.0f;
                pauseText.text = string.Format("PAUSE");
                isPaused = true;
            }
            else
            {
                Time.timeScale = 1.0f;
                isPaused = false;
            }
        }
    }

    private void RingOutVictory()
    {
        if (players[0].IsHypeHit || players[1].IsHypeHit)
            ringOutText.text = "RING OUT!";
    }
}

