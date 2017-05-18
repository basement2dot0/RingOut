//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class GameManager : MonoBehaviour
// {
//    [SerializeField]
//    private float Rounds;
//    [SerializeField]
//    private float Match;

//    private PlayerOne playerOne;
//    private PlayerTwo playerTwo;

//    [SerializeField]
//    private Text ringOutText;

//    private void Start()
//    {
//        Rounds = 0;
//        ringOutText.text = "";
//        playerOne = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerOne>();
//        playerTwo = GameObject.FindGameObjectWithTag("PlayerTwo").GetComponent<PlayerTwo>();
//    }
//    private void Update()
//    {
//        if(playerOne.currentState == State.Hit || playerTwo.currentState == State.Hit)
//        {
//            ringOutText.text = "RING OUT!";
//        }
//    }

//}

