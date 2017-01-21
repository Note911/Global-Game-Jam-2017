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

    private float stamina;
    public float maxStamina;

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
        if(SwimOrGlide) { 
            if (Mathf.Abs(rbody.velocity.x) > maxSpeed) 
                rbody.velocity = new Vector2(Mathf.Sign(rbody.velocity.x) * maxSpeed, rbody.velocity.y);
        }
        else {
            if (Mathf.Abs(rbody.velocity.x) > maxSpeed * 2)
                rbody.velocity = new Vector2(Mathf.Sign(rbody.velocity.x) * maxSpeed, rbody.velocity.y);
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

        //confine to screen
      /* if (transform.position.x > screenLimit.x)
            transform.position = new Vector3(screenLimit.x, transform.position.y, transform.position.z);
        if (transform.position.x < 0)
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
         if (transform.position.y > screenLimit.y)
            transform.position = new Vector3(transform.position.x, screenLimit.y, transform.position.z);
        if (transform.position.y < 0)
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);*/


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
            moveSpeed = baseMoveSpeed * 2;
        else
            moveSpeed = baseMoveSpeed;

        //rotate to face heading
        if (heading != Vector2.zero)
        {
            float angle = 0;
            if (facingRight) { 
                angle = Mathf.Acos(Vector2.Dot(heading.normalized, new Vector2(1,0)));
                if (heading.y > 0)
                    transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle);
                else
                    transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * -angle);
            }
            else { 
                 angle = Mathf.Acos(Vector2.Dot(heading.normalized, new Vector2(-1,0)));
                 if (heading.y > 0)
                    transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * -angle);
                 else
                    transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle);
            }
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
