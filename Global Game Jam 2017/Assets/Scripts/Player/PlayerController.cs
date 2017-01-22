using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Player player;

    public string swimButton;

    // Use this for initialization
    void Start () {
        
	}
    private void FixedUpdate()
    {

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
                if (Input.GetButton(swimButton)&& player.stamina > 0)
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
                if (Input.GetButton(swimButton) && player.stamina > 0)
                    player.SwimOrGlide = true;
                if (Input.GetButtonUp(swimButton))
                    player.SwimOrGlide = false;
            break;
        }
        float theta = 0;
        if (player.playerState == Player.PlayerState.UNDERWATER)
        {
            //Redirect the momentum based on heading
            player.rbody.velocity = player.heading.normalized * player.rbody.velocity.magnitude;

            player.transform.Rotate(0, 0, (player.turnSpeed * 0.5f) * Time.deltaTime);

           
        }
        else if(player.playerState == Player.PlayerState.AIRBORNE)
        {
            player.breechVelocity -= 1.0f * Time.deltaTime;
            if(player.SwimOrGlide) {
            
                Vector2 lift = Vector3.Cross(new Vector3(0,0,1), new Vector3(player.heading.x, player.heading.y, 0));

                float headingUpDot = Vector2.Dot(player.rbody.velocity.normalized, Vector2.up);
                player.breechVelocity -= (headingUpDot * 2.0f);
                player.rbody.velocity = player.rbody.velocity.normalized * player.breechVelocity * 0.80f + (lift.normalized * player.breechVelocity * 0.05f);
                player.rbody.AddForce(lift);
        
            }
                
                Vector2 vel = player.rbody.velocity.normalized;
                theta = Mathf.Rad2Deg * Mathf.Acos((Vector2.Dot(Vector2.right, vel)));

                if (player.rbody.velocity.y < 0)
                    player.transform.rotation = Quaternion.Euler(0, 0, -theta);
                else
                    player.transform.rotation = Quaternion.Euler(0, 0, theta);


        } 
        
    }
}
