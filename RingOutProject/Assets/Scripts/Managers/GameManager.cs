using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float Rounds;
    [SerializeField]
    private float Match;

    private Player playerOne;
    private Player playerTwo;

    [SerializeField]
    private Text ringOutText;
    private void Awake()
    {
        foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (player.GetComponent<Player>().ID == 1)
                playerOne = player.GetComponent<Player>();
            else
                playerTwo = player.GetComponent<Player>();
        }
    }
    private void Start()
    {
        Rounds = 0;
        ringOutText.text = "";
        
    }
    private void Update()
    {
        if (playerOne.CurrentState == State.HIT || playerTwo.CurrentState == State.HIT)
        {
            ringOutText.text = "RING OUT!";
        }
    }

}

