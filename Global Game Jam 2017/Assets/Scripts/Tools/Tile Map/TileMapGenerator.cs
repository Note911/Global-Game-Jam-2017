using UnityEngine;
using System.Collections;

public class TileMapGenerator : TileMap {

    public string levelName;

	// Use this for initialization
	void Start () {
        LoadMap(levelName);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
