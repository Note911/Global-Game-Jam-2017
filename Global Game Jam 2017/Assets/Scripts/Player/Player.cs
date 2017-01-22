using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : GameEntity {
    //Water check transform for raycasting
    public Transform waterCheck;

    public enum PlayerAnimState { IDLE, SWIM, GLIDE, FALL }
    public enum PlayerState { UNDERWATER, AIRBORNE }

    public PlayerAnimState playerAnimState = PlayerAnimState.IDLE;
    public PlayerState playerState = PlayerState.UNDERWATER;

    public float stamina = 100;
    public float maxStamina = 100;
    public float decayRate = 1.0f;
    public float glideUpForce = 0.5f;
    public bool SwimOrGlide = false;
    public float turnSpeed = 75;
    public float airControl = 0.5f;
    public string swimButton;
    public string horizontalAxis;
    public string verticalAxis;

    public float breechVelocity;  //tracks the initial velocity of the fish as they leave the water  (Scalar value to multiply onto the heading)

    public string[] animationReferences;

    public Vector2 screenLimit;

    //reference to wave controller for skipping off waves
    public WaveController waveController;

    protected override void Awake() {
        base.Awake();
    }

    // Use this for initialization
    protected override void Start () {
        base.Start();
        GenerateAnimationList();
        animator = new AnimationController2D(renderer, animationList);
        animator.ChangeAnimation((int)PlayerAnimState.IDLE);
        stamina = maxStamina;
	}
	
	// Update is called once per frame
	protected override void Update () {
        if(playerState == PlayerState.UNDERWATER)
        {
            rbody.AddForce(heading * moveSpeed);
        }
        //speed limit
        if (SwimOrGlide) {
            if (Mathf.Abs(rbody.velocity.x) > maxSpeed * 1.5f && playerState == PlayerState.UNDERWATER)
            {
                rbody.velocity = new Vector2(maxSpeed * 1.5f, rbody.velocity.y);
            }

            if(stamina > 0)
                stamina -= decayRate * Time.deltaTime;
        }
        else if(!SwimOrGlide && playerState == PlayerState.UNDERWATER)
        {
            rbody.velocity = new Vector2(Mathf.Lerp(maxSpeed * 1.5f, maxSpeed, 1f), rbody.velocity.y);
        }


        //Check if we are in the water
        if (Physics2D.Linecast(transform.position, waterCheck.position, 1 << LayerMask.NameToLayer("Water")))
        {
            //Before we change the state to underwater lets check if the player was airborne last frame
            if (playerState == PlayerState.AIRBORNE)
<<<<<<< HEAD
                Dive();
=======
            {
                rbody.velocity *= 0.2f;
            }
>>>>>>> refs/remotes/origin/Development
            playerState = PlayerState.UNDERWATER;
        }
        else
        {
            if (playerState == PlayerState.UNDERWATER)
                Breech();
            playerState = PlayerState.AIRBORNE;
            
        } 

        switch (playerState) {
            case (PlayerState.UNDERWATER):
                if (SwimOrGlide)
                    playerAnimState = PlayerAnimState.SWIM;
                else
                    playerAnimState = PlayerAnimState.IDLE;
                break;
            case (PlayerState.AIRBORNE):
                 if (SwimOrGlide)
                    playerAnimState = PlayerAnimState.GLIDE;
                else
                    playerAnimState = PlayerAnimState.FALL;
                break;
        }
        if ((int)playerAnimState != animator.animState)
            animator.ChangeAnimation((int)playerAnimState);
        //Are we swimming fast?!  flagged from player controller
        if (SwimOrGlide)
        {
            if(playerState == PlayerState.UNDERWATER)
                moveSpeed = baseMoveSpeed * 1.5f;
        }
        else
        {
            if (playerState == PlayerState.UNDERWATER)
                moveSpeed = baseMoveSpeed;
        }
        
        base.Update();
	}

        private void GenerateAnimationList() {
        AnimationManager animManager = ResourceManager.GetInstance().GetAnimationManager();
        foreach (string animName in animationReferences) {
            print("Asking for Animation.... " + animName);
            animationList.Add(animManager.GetAnimation(animName));
        }
    }    

    private void Dive() {
        //The point of our player is going to be our reference for the point of impact here
        Vector2 pointOfImpact = transform.position;
        //We are going to find another point on the wave to get a line between the one just under us and one behind to get the angle of the water
        //If we are traveling right the offset will be negitive and vice versa
        //the offset is equal to 1/8 PI
        float waveOffset = (1.0f / 8.0f) * Mathf.PI;
        //Check if we are going right or left, probably right but you never know
        //oops were going left!
        if (heading.x < 0)
            waveOffset = -waveOffset;
        Vector2 wavePointRef = new Vector2(pointOfImpact.x + waveOffset, waveController.amp * Mathf.Sin(pointOfImpact.x + waveOffset * waveController.freq));
        //With this point on the wave we can get a vector that describes where the waters surface is
        Vector2 waveAngle = wavePointRef - pointOfImpact;

        //Now that wae have both the heading and the angle of the wave we can calculate the angle of incedence.
        float impactAngle = Mathf.Acos(Vector2.Dot(heading.normalized, waveAngle.normalized));
        //Now we have our impact angle in radians!
        //Lets convert to degrees for ease of use
        impactAngle *= Mathf.Rad2Deg;
        impactAngle -= 90.0f;

        Debug.Log("Impact Angle: " + impactAngle);

        if(impactAngle < 20.0f) {
            rbody.velocity = new Vector2(rbody.velocity.x, Mathf.Abs(rbody.velocity.y)).normalized * breechVelocity;
            rbody.AddForce(Vector2.up * 20.0f);
        }
        else if(impactAngle >= 20.0f && impactAngle < 75.0f) {
            heading.y -= 1.0f;
            rbody.velocity = heading.normalized * breechVelocity * 0.4f;
        }
        else {
            rbody.velocity = heading.normalized * breechVelocity * 0.7f;
        }
    }

    private void Breech() {
        breechVelocity = rbody.velocity.magnitude;
    }
}
