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

    float xVel;

    public bool SwimOrGlide = false;

    public string swimButton;
    public string horizontalAxis;
    public string verticalAxis;

    public string[] animationReferences;

    public Vector2 screenLimit;

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
       
        //speed limit
        if (SwimOrGlide) {
            if (Mathf.Abs(rbody.velocity.x) > maxSpeed * 1.5f && playerState == PlayerState.UNDERWATER)
            {
                rbody.velocity = new Vector2(maxSpeed * 1.5f, rbody.velocity.y);
            }

            if (playerState == PlayerState.AIRBORNE)
            {
                rbody.AddForce(Vector2.up * (rbody.gravityScale * 0.25f));
            }
            if(stamina > 0)
                stamina -= decayRate * Time.deltaTime;
        }
        else {
            rbody.velocity = new Vector2(maxSpeed, rbody.velocity.y);
        }
        
        //Check if we are in the water
        if (Physics2D.Linecast(transform.position, waterCheck.position, 1 << LayerMask.NameToLayer("Water"))) {
            //Before we change the state to underwater lets check if the player was airborne last frame
            if (playerState == PlayerState.AIRBORNE)
                rbody.velocity *= 0.2f;
            playerState = PlayerState.UNDERWATER;
        }
        else
            playerState = PlayerState.AIRBORNE;


        //Flip x
        if (facingRight) { 
            if (heading.x < -0.001)
                FlipX();
        }
        else {
            if (heading.x > 0.001)
                FlipX();
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
}
