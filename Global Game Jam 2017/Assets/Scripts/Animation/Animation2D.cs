using UnityEngine;
using System.Collections;

public class Animation2D {
    public string name;         //Name of the animation
    public Sprite[] sprite;     //Holds all the frames for the animation
    private int numFrames;       //Number of frames in the animation
    public int currFrame;       //Current frame of the animation
    public int startFrame;      //Beginning frame of the animation
    public int endFrame;        //Ending frame of the animation
    private long initInterval;     //initial time interval for playback
    public long currInterval;   //Current time interval for playback
    public int priority;        //Determines if this animation can override another

    public bool paused = false;             //Determines if the animation is paused or not
    private float pauseDelay = -1.0f;       //Determines how long the animation should stay paused -1 means it stays paused indefinitly
    private float timeSincePaused = 0.0f;
    public bool playBackward;      //Determines if the animation should be played in reverse
    public bool loop = false;       //Determines if fhte animation should loop
    public bool finished = false;   //Determines if the animation has finished playing


    public Animation2D(Sprite[] sprite, long interval, int priority, string name) {
        this.sprite = sprite;
        initInterval = currInterval = interval;
        numFrames = sprite.Length;
        startFrame = 0;
        endFrame = numFrames - 1;
        currFrame = 0;
        this.priority = priority;
        this.name = name;
    }

    public Animation2D(Sprite[] sprite, long interval, int priority, string name, bool playOnAwake, bool loop)  {
        this.sprite = sprite;
        initInterval = currInterval = interval;
        numFrames = sprite.Length;
        startFrame = 0;
        endFrame = numFrames - 1;
        currFrame = 0;
        this.priority = priority;
        this.name = name;
        this.loop = loop;
        paused = !playOnAwake;
    }

    public Animation2D(Animation2D prevAnim) {
        sprite = prevAnim.sprite;
        initInterval = currInterval = prevAnim.initInterval;
        numFrames = sprite.Length;
        startFrame = 0;
        endFrame = numFrames - 1;
        currFrame = 0;
        priority = prevAnim.priority;
        name = prevAnim.name;
        loop = prevAnim.loop;
        paused = prevAnim.paused;
    }

    //Play must be called outside of this class, and also must be called every frame. (use a loop or the update function of an object that inherits from monobehaviour)
    //Since we only need one timer for a set of animations the update method will take the output from a timer which will be held by the animation controller class or temporary sprite manager
    public void Play(Timer animTimer, SpriteRenderer objRenderer) {
        if (paused && pauseDelay >= 0.0f) {
            timeSincePaused += Time.deltaTime;
            if(timeSincePaused >= pauseDelay) {
                timeSincePaused = 0.0f;
                pauseDelay = -1.0f;
                paused = false;
            }                                            
        }
        if (!paused && !finished) 
            animTimer.Update();

        if (animTimer.GetTotalMilliSeconds() >= currInterval)
        {
            if (!finished)
                UpdateFrame(objRenderer);
            //Playing backward?
            if (playBackward)
            {
                currFrame--;

                //Wrap around the frames to loop
                if (currFrame < startFrame) {
                    if (loop)
                        currFrame = endFrame;
                    else {
                        currFrame = endFrame;
                        finished = true;
                    }
                }
              
            }
            //Nope!
            else
            {

                if (!finished)
                    UpdateFrame(objRenderer);
                currFrame++;

                //Wrap around the frames to loop
                if (currFrame > endFrame) { 
                    if(loop)
                        currFrame = startFrame;
                    else
                    {
                        currFrame = startFrame;
                        finished = true;
                    }
                }
            }

            animTimer.RestartTimer();
        }
    }

    //Syncs the objects renderer with the correct animation frame
    public void UpdateFrame(SpriteRenderer renderer) {
        renderer.sprite = GetCurrFrame();
    }

    //Returns the current frame from the sprite array
    public Sprite GetCurrFrame() {
        return sprite[currFrame];
    }

         //Sets the animation interval to something else
    public void SetAnimInterval(long interval) {
        //NOTE: sav's code has curraniminterval = animinterval = interval why have two intervals if its not ment to hold the previous
        currInterval = interval;
    }
    //Resets the animation interval to the original value
    public void ResetAnimInterval() {
        currInterval = initInterval;
    }

    //Pauses the animation
    public void Pause() {
        paused = true;
    }

    public void Pause(float delay) {
        paused = true;
        pauseDelay = delay;
    }

    //Resumes the animation
    public void Resume() {
        paused = false;
    }

    //Restarts the animation
    public void Restart(Timer animTimer, SpriteRenderer objRenderer)
    {
        paused = false;
        finished = false;
        currFrame = startFrame;
        animTimer.RestartTimer();
        UpdateFrame(objRenderer);
    }

    //Stops the animation and resets the frame to the start
    public void Stop(Timer animTimer, SpriteRenderer objRenderer)
    {
        paused = true;
        currFrame = startFrame;
        animTimer.RestartTimer();
        UpdateFrame(objRenderer);
    }
}
