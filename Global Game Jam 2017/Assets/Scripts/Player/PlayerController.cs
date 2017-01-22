using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Player player;

    public string swimButton;
    public string verticalAxis;

    
    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
         //Set the players heading toward the input
        player.heading = new Vector2(Mathf.Cos(Mathf.Deg2Rad * player.transform.rotation.eulerAngles.z), Mathf.Sin(Mathf.Deg2Rad * player.transform.rotation.eulerAngles.z));
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
        v = Input.GetAxis(verticalAxis);
        float theta = 0;
        if (player.playerState == Player.PlayerState.UNDERWATER)
        {
            //Redirect the momentum based on heading
            player.rbody.velocity = player.heading.normalized * player.rbody.velocity.magnitude;

           
            player.transform.Rotate(0, 0, v * player.turnSpeed * Time.deltaTime);

            float angle = player.transform.eulerAngles.z;//deg;
            //player.transform.eulerAngles = new Vector3(0, 0, ClampAngle(angle, -85, 85));

            if (v == 0){
                player.transform.Rotate(0, 0, (player.turnSpeed * 0.5f) * Time.deltaTime);
            }

           
        }
        else if(player.playerState == Player.PlayerState.AIRBORNE)
        {
            player.breechVelocity -= 0.1f * Time.deltaTime;
            Mathf.Clamp(player.breechVelocity, 0.0f, player.breechVelocity);
            if(v != 0 && player.SwimOrGlide) {
                //Get the angle between the right vector and the heading
                float headingAngle = Mathf.Rad2Deg * Mathf.Acos(Vector2.Dot(player.heading.normalized, Vector2.right));
                        
                if(headingAngle < 90.0f) {
                    float liftPercentage = 1.0f - (headingAngle / 90.0f);
                    player.transform.Rotate(0, 0, v * (player.turnSpeed) * Time.deltaTime);             
                    player.rbody.velocity = player.heading.normalized * (player.breechVelocity * liftPercentage);

                    Vector2 lift = Vector2.up * player.glideUpForce * liftPercentage;
                    Debug.Log(player.rbody.velocity.magnitude);
                    Debug.Log(player.rbody.velocity);
                    player.rbody.AddForce(lift);
                }
                else if(headingAngle > 90.0f && headingAngle < 270){
                    player.transform.Rotate(0, 0, v * (player.turnSpeed * 3.0f) * Time.deltaTime); 
                }
                else
                    player.transform.Rotate(0, 0, v * (player.turnSpeed * 5.0f) * Time.deltaTime); 
            }
            else { 

                Vector2 vel = player.rbody.velocity.normalized;
                theta = Mathf.Rad2Deg * Mathf.Acos((Vector2.Dot(Vector2.right, vel)));

                if (player.rbody.velocity.y < 0)
                    player.transform.rotation = Quaternion.Euler(0, 0, -theta);
                else
                    player.transform.rotation = Quaternion.Euler(0, 0, theta);
            }

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
