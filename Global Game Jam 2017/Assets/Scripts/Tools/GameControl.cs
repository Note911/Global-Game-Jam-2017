using UnityEngine;
using UnityEditor.SceneManagement;
using System.Collections;

public class GameControl : MonoBehaviour {

    //Properties
    public static GameControl control;
    
	// Use this for initialization
	void Awake () {
	    if(control == null) {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        //if control exists but isnt equal to this, there is a copy and that copy should distroy itself
        else if (control != this) {
            Destroy(gameObject);
        }
	}

    //After Awake
    void Start() {
        ContentLoader loader = GetComponentInChildren<ContentLoader>();
        loader.LoadContent();
        if (loader.HasLoaded()) {
            Destroy(loader.gameObject);
            EditorSceneManager.LoadScene("GlobalGameJamSplash");
        }
    }
	
	// Update is called once per frame
	void Update () {
	    TempSpriteManager.GetInstance().Update();
	}
}
