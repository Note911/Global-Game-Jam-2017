using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerFactory : MonoBehaviour {
    List<PowerUp> powerupList;

    public Camera cam;
    public GameObject PUType;
    public Player player;

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


        InvokeRepeating("SpawnPowerUps", 0.0f, 2.0f);
	}
	
    private void SpawnPowerUps() {
        Debug.Log("Spawning");
        int i = Random.Range(0, 2);
        switch(i) {
            case (0):
                SpawnScatter();
                break;
            case (1):
                SpawnSineWave();
                break;
            case (2):
                break;
            case (3):
                break;
        }
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
        return new Vector2(Random.Range(transform.position.x, transform.position.x + 175.0f), Random.Range(-1.5f, 12.0f));
    }

    void SpawnScatter()
    {

    }

    void SpawnSineWave()
    {
        for (int i = 0; i < 10; ++i)
        {
            GameObject newPowerUp = GameObject.Instantiate(PUType);
            newPowerUp.transform.position = new Vector2(2.5f * i + 125.0f, Mathf.Sin(2f * i));
            newPowerUp.transform.position += transform.position;
            newPowerUp.GetComponent<PowerUp>().player = player;
            powerupList.Add(PUType.GetComponent<PowerUp>());
          

        }



    }
    void SpawnTanWave()
    {

    }
    void SpawnLoop()
    {
        //Instead of Instantiating- Take 15 unused objects
        for (int i = 0; i < 15; ++i)
        {
            GameObject newPowerUp = GameObject.Instantiate(PUType);
            powerupList.Add(PUType.GetComponent<PowerUp>());
            newPowerUp.GetComponent<PowerUp>().player = player;
        }
        foreach (PowerUp powerup in powerupList)
        {
            powerup.transform.position = RandomPositionVector();
            powerup.transform.position += transform.position;
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
}
