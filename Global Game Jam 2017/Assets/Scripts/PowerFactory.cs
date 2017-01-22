﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerFactory : MonoBehaviour {
    List<PowerUp> powerupList;

    public Camera cam;
    public PowerUp PUType;

    enum SpawnPattern
    {
        Scatter,
        SineWave,
        TanWave,
        LoopDaLoop
    };
	// Use this for initialization
	void Start ()
    {
        powerupList = new List<PowerUp>();


        SpawnSineWave(2.5f, 2.0f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        
    }
    public void RemoveFromWorld(GameObject _powerUp)
    {
        
    }

    Vector2 RandomPositionVector()
    {
        return new Vector2(Random.Range(cam.transform.position.x + 125.0f, cam.transform.position.x + 175.0f), Random.Range(-1.5f, 12.0f));
    }

    void SpawnScatter(int numPowers)
    {
        //Instead of Instantiating- Take 15 unused objects
        for (int i = 0; i < ; ++i)
        {
            powerupList.Add(Instantiate(PUType));
        }
        foreach (PowerUp powerup in powerupList)
        {
            powerup.transform.position = RandomPositionVector();
        }
        for (int i = 0; i < powerupList.Capacity; i++)
        {
            for (int j = i + 1; j < powerupList.Capacity; j++)
            {
                if (Mathf.Abs((powerupList[j].transform.position - powerupList[i].transform.position).magnitude) < 5.0f)
                {
                    powerupList[i].transform.position = RandomPositionVector();
                }
            }
        }
    }

    void SpawnSineWave(float a, float f)
    {
        for (int i = 0; i < 10; ++i)
        {
            powerupList.Add(Instantiate(PUType));
            powerupList[i].transform.position = new Vector2(a * i + 125.0f, Mathf.Sin(f * i));

        }



    }
    void SpawnTanWave()
    {

    }
    void SpawnLoop()
    {
       
    }
}
