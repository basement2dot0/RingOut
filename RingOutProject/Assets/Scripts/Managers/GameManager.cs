using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float matchTimer;
    [SerializeField]
    private Text matchTimerText;
    [SerializeField]
    private float Rounds;
    [SerializeField]
    private float Match;
    [SerializeField]
    private Player[] players;
    [SerializeField]
    private Text ringOutText;

    private void Awake()
    {
        matchTimerText = GetComponent<Text>();
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
        matchTimer = 60.0f;
        Rounds = 0;
        ringOutText.text = "";
        
    }
    private void Update()
    {
        matchTimerText.text = matchTimer.ToString();

        if (players[1].IsHypeHit)
            Debug.Log(players[1].IsHypeHit.ToString());

        if (players[0].IsHypeHit|| players[1].IsHypeHit)
            ringOutText.text = "RING OUT!";


    }

    private void RoundTimer()
    {
        matchTimer -= (Time.time);
    }

}

