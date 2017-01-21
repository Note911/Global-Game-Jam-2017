using UnityEngine;
using System.Collections;

public class TempSprite {

    private Animation2D _anim;      //Reference to the the temporary sprites animation
    private GameObject _obj;
    private SpriteRenderer _renderer;
    private Timer _animTimer;

    public bool finished = false;       

    //Constructor
    public TempSprite(Animation2D anim, Vector2 pos) {
        _obj = new GameObject();
        _obj.transform.position = pos;

        _obj.AddComponent<SpriteRenderer>();
        _renderer = _obj.GetComponent<SpriteRenderer>();

        _anim = anim;
        _animTimer = new Timer();
        _animTimer.StartTimer();
    }

    public TempSprite(Animation2D anim, Vector2 pos, Vector2 scale)
    {
        _obj = new GameObject();
        _obj.transform.position = pos;
        _obj.transform.localScale = scale;

        _obj.AddComponent<SpriteRenderer>();
        _renderer = _obj.GetComponent<SpriteRenderer>();

        _anim = anim;
        _animTimer = new Timer();
        _animTimer.StartTimer();
    }

    public TempSprite( Animation2D anim, Vector2 pos, Vector2 scale, string sortingLayer, int sortingOrder)
    {
        _obj = new GameObject();
        _obj.transform.position = pos;
        _obj.transform.localScale = scale;

        _obj.AddComponent<SpriteRenderer>();
        _renderer = _obj.GetComponent<SpriteRenderer>();
        _renderer.sortingLayerName = sortingLayer;
        _renderer.sortingOrder = sortingOrder;

        _anim = anim;
        _animTimer = new Timer();
        _animTimer.StartTimer();
    }

    public TempSprite(Animation2D anim, Vector2 pos, Vector2 scale, float rotation, string sortingLayer, int sortingOrder)
    {
        _obj = new GameObject();
        _obj.transform.position = pos;
        _obj.transform.localScale = scale;
        _obj.transform.Rotate(new Vector3(0, 0, 1), rotation);

        _obj.AddComponent<SpriteRenderer>();
        _renderer = _obj.GetComponent<SpriteRenderer>();
        _renderer.sortingLayerName = sortingLayer;
        _renderer.sortingOrder = sortingOrder;

        _anim = anim;
        _animTimer = new Timer();
        _animTimer.StartTimer();
    }


    public void Update() {
        _anim.Play(_animTimer, _renderer);
        finished = _anim.finished;
    }

    public void Delete(){
        GameObject.Destroy(_obj);
    }

    public void Delay(float delay) {
        _anim.Pause(delay);
    }
}
