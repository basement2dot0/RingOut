using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounceBack : MonoBehaviour {
    Player player;
    private string body;
    [SerializeField]
    float dist;
	// Use this for initialization
	void Start () {
        player = GetComponentInParent<Player>();
        body = "Body" + player.Opponent.ID.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerStay(Collider other)
    {
        if(other.name == "Ground" && !player.IsGrounded)
        {
            Debug.Log(other.name);
            float distance = Vector3.Distance(other.transform.position, player.transform.position) + dist;
            Vector3 target = -player.transform.forward;
            //Vector3 Position = player.transform.position;
            player.transform.Translate(target * distance* Time.deltaTime);
        }
    }

}
