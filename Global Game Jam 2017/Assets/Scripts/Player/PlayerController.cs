using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Player player;

    public string swimButton;
    public string verticalAxis;

    
    public float turnSpeed = 200;
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
                if (Input.GetButtonUp(swimButton))
                    player.SwimOrGlide = false;

                break;
            case (Player.PlayerState.UNDERWATER):
                //set gravity scale to very little
                 player.rbody.gravityScale = 0.01f;
                
                if (player.heading.magnitude > 1)
                    player.heading.Normalize();

                //Flag the player to swim faster if button is pressed
                if (Input.GetButtonDown(swimButton) && player.stamina > 0)
                    player.SwimOrGlide = true;
                if (Input.GetButtonUp(swimButton))
                    player.SwimOrGlide = false;
            break;
        }
        //Get the horizontal and verticle axis input
        float v = 0;
        float theta = 0;
        if (player.playerState == Player.PlayerState.UNDERWATER)
        {
            v = Input.GetAxis(verticalAxis);


            player.transform.Rotate(0, 0, v * turnSpeed * Time.deltaTime);

            float angle = player.transform.eulerAngles.z;//deg;
            player.transform.eulerAngles = new Vector3(0, 0, ClampAngle(angle, -85, 85));

            //Set the players heading toward the input
            player.heading = new Vector2(Mathf.Cos(Mathf.Deg2Rad * player.transform.rotation.eulerAngles.z), Mathf.Sin(Mathf.Deg2Rad * player.transform.rotation.eulerAngles.z));
        }
        else if(player.playerState == Player.PlayerState.AIRBORNE)
        {

            Vector2 vel = player.rbody.velocity.normalized;
            theta = Mathf.Rad2Deg * Mathf.Acos((Vector2.Dot(Vector2.right, vel)));
            Debug.Log(theta);
            if (player.rbody.velocity.y < 0)
                player.transform.rotation = Quaternion.Euler(0, 0, ClampAngle(-theta, -85, 85));
            else
                player.transform.rotation = Quaternion.Euler(0, 0, ClampAngle(theta, -85, 85));

        }
        
    }
    private float ClampAngle(float angle, float min, float max)
    {
        
        if (angle < 90f || angle > 270f)
        {
            if (angle > 180)
                angle -= 360f;
            if (max > 180)
                max -= 360f;
            if (min > 180)
                min -= 360f;

        }
        angle = Mathf.Clamp(angle, min, max);
        if (angle < 0)
        {
            
            angle += 360f;
            
        }
        
        return angle;
    }
}
