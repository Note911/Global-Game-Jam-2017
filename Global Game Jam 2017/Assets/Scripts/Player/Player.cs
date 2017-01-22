using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : GameEntity {
    //Water check transform for raycasting
    public Transform waterCheck;
    public Transform wakeCheck;

    public GameObject scoreManager;
    
    public GameObject splash, splash2, splash3, splash4, wake;
    public TrailRenderer trailRenderer;
    public InspirationalMessager inspire;

    private float wakeTimer;

    public enum PlayerAnimState { IDLE, SWIM, GLIDE, FALL }
    public enum PlayerState { UNDERWATER, AIRBORNE }

    public PlayerAnimState playerAnimState = PlayerAnimState.IDLE;
    public PlayerState playerState = PlayerState.AIRBORNE;

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
        trailRenderer.sortingOrder = 4;
	}
	
	// Update is called once per frame
	protected override void Update () {

        if(playerState == PlayerState.UNDERWATER)
        {
            rbody.AddForce(heading * moveSpeed);
        }

        if (stamina == 0.0f)
            SwimOrGlide = false;
        //speed limit
        if (SwimOrGlide) {
            if (Mathf.Abs(rbody.velocity.x) > maxSpeed * 1.5f && playerState == PlayerState.UNDERWATER)
            {
                rbody.velocity = new Vector2(maxSpeed * 1.5f, rbody.velocity.y);
            }

            stamina -= decayRate * Time.deltaTime;
            Mathf.Clamp(stamina, 0.0f, maxStamina);
            if (stamina == 0.0f)
                SwimOrGlide = false;
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
                Dive();
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
                wakeTimer = 0;
                if (SwimOrGlide)
                    playerAnimState = PlayerAnimState.SWIM;
                else
                    playerAnimState = PlayerAnimState.IDLE;
                break;
            case (PlayerState.AIRBORNE):
                Vector3 wakePos = new Vector3(wakeCheck.position.x - 4.0f, wakeCheck.position.y, wakeCheck.position.z);
                if (Physics2D.Linecast(new Vector3(transform.position.x, transform.position.y - 2.0f, transform.position.z), wakePos, 1 << LayerMask.NameToLayer("Water")))
                {
                    if (rbody.velocity.x > 20.0f && rbody.velocity.y < 2.5 && rbody.velocity.y > -2.5)
                    {
                        Destroy(GameObject.Instantiate(wake, wakeCheck.position, Quaternion.Euler(0, 0, 0)), 0.5f);

                        wakeTimer += Time.deltaTime;
                        scoreManager.GetComponent<ScoreManager>().AddStaticScore((int)(wakeTimer * 100));
                    }
                }
                else
                    wakeTimer = 0;
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

        //wake messages
        if (wakeTimer > 2.0f)
            inspire.WakeMessages(4);
        else if (wakeTimer > 1.0f)
            inspire.WakeMessages(3);
        else if (wakeTimer > 0.5f)
            inspire.WakeMessages(2);
        else if (wakeTimer > 0.1f)
            inspire.WakeMessages(1);
        else if (wakeTimer > 0.05f)
            inspire.WakeMessages(0);
        

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

       // Debug.Log("Impact Angle: " + impactAngle);
        if(impactAngle < 20.0f) {
            inspire.CustomMessage("Get Wrekt!", Color.red, new Vector2(scoreManager.GetComponent<Text>().transform.position.x , scoreManager.GetComponent<Text>().transform.position.y - 25));
            scoreManager.GetComponent<ScoreManager>().ChangeCurrentScoreByMultiplier(0.0f);
            rbody.velocity = new Vector2(rbody.velocity.x, Mathf.Abs(rbody.velocity.y)).normalized * (breechVelocity * 0.8f);
            Destroy(GameObject.Instantiate(splash3, transform.position, Quaternion.Euler(0, 0, 0)), 3.0f);
            inspire.BadDiveMessages();
            
        }
        else if(impactAngle >= 20.0f && impactAngle < 35.0f) {
             Destroy(GameObject.Instantiate(splash2, transform.position, Quaternion.Euler(0, 0, 0)), 3.0f);
             rbody.velocity = heading.normalized * breechVelocity * 0.9f;
        }
        else if(impactAngle >= 35.0f && impactAngle < 75.0f) {
            Destroy(GameObject.Instantiate(splash, transform.position, Quaternion.Euler(0, 0, impactAngle)), 3.0f);
            inspire.DiveMessages();
            rbody.velocity = heading.normalized * breechVelocity;
        }
        else {
            scoreManager.GetComponent<ScoreManager>().AddMultiplier(2.0f);
            Destroy(GameObject.Instantiate(splash, transform.position, Quaternion.Euler(0, 0, 0)), 3.0f);
            inspire.GreatDiveMessages();
            rbody.velocity = heading.normalized * maxSpeed;

        }
    }

    private void Breech() {
        float breechAngle = Mathf.Rad2Deg * Mathf.Acos(Vector2.Dot(heading, Vector2.right));

        if(rbody.velocity.magnitude > maxSpeed - 2.0f)
            Destroy(GameObject.Instantiate(splash4, transform.position, Quaternion.Euler(0, 0, breechAngle)), 3.0f);
        else
            Destroy(GameObject.Instantiate(splash3, transform.position, Quaternion.Euler(0, 0, 0)), 3.0f);
        breechVelocity = rbody.velocity.magnitude;
    }

    
}
