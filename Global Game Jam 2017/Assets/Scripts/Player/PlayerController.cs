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
        //TO DO:  Determine the state of the player
        //are they in water or air

        //Get the horizontal and verticle axis input
        float h = Input.GetAxis(horizontalAxis);
        float v = Input.GetAxis(verticalAxis);
        player.heading = new Vector2(h, v);
        if (player.heading.magnitude > 1)
            player.heading.Normalize();


        //Flag the player to swim faster if button is pressed
        if (Input.GetButtonDown(swimButton))
            player.SwimOrGlide = true;
        else if (Input.GetButtonUp(swimButton))
            player.SwimOrGlide = false;
	}
}
