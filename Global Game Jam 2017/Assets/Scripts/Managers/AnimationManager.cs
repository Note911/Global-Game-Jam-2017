using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimationManager {

 //singleton class
    private Dictionary<string, Animation2D> _animationList;

    //singleton so private constructor
    public AnimationManager() {
        _animationList = new Dictionary<string, Animation2D>();
    }
    public void AddAnimation(Animation2D animation) {
        _animationList.Add(animation.name, animation);
    }

    //public void AddAnimationSet(string name, Animation2D[] animations) {
    //    for(int i = 0; i < animations.Length; ++i) {
    //        string _name = name;
    //        _name = name + "_" + i;
    //        _animationList.Add(_name, animations[i]);
    //    }
    //}

    public Animation2D GetAnimation(string name) {
        return new Animation2D(_animationList[name]);
    }
}
