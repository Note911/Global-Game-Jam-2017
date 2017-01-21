using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Player player;

    public string swimButton;
    public string horizontalAxis;
    public string verticalAxis;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        switch(player.playerState) {
            case (Player.PlayerState.AIRBORNE):
                //Set gravity scale to full
                player.rbody.gravityScale = 1.0f;
                //Flag the player to swim faster if button is pressed
                if (Input.GetButtonDown(swimButton))
                    player.SwimOrGlide = true;
                else if (Input.GetButtonUp(swimButton))
                    player.SwimOrGlide = false;
                break;
            case (Player.PlayerState.UNDERWATER):
                //set gravity scale to very little
                 player.rbody.gravityScale = 0.01f;
                //Get the horizontal and verticle axis input
                float h = Input.GetAxis(horizontalAxis);
                float v = Input.GetAxis(verticalAxis);

                //Set the players heading toward the input
                player.heading = new Vector2(h, v);
                if (player.heading.magnitude > 1)
                    player.heading.Normalize();

                //Flag the player to swim faster if button is pressed
                if (Input.GetButtonDown(swimButton))
                    player.SwimOrGlide = true;
                else if (Input.GetButtonUp(swimButton))
                    player.SwimOrGlide = false;
            break;
        }
	}
}
