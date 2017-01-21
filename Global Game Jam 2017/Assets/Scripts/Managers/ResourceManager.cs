using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceManager {
    private static ResourceManager _instance;

    private AnimationManager _animationManager;
    public AnimationManager GetAnimationManager() { return _animationManager; }

    private AudioManager _audioManager;
    public AudioManager GetAudioManager() { return _audioManager; }

    private SpriteManager _spriteManager;
    public SpriteManager GetSpriteManager() { return _spriteManager; }


    private ResourceManager(){
        _animationManager = new AnimationManager();
        _audioManager = new AudioManager();
        _spriteManager = new SpriteManager();

    }

    public static ResourceManager GetInstance() {
        if (_instance == null)
            _instance = new ResourceManager();
        return _instance;
    }


}
