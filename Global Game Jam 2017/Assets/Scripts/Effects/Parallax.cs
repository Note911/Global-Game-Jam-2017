using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {

    public Transform anchor;
    public Vector3 startPos;
    public float speed;

    public GameObject image;
    private GameObject image2;

    public float offset = 15.0f;

    public Sprite sprite;
    public Camera cameraRef;

    void Start() {
        image2 = new GameObject();
        image2.AddComponent<SpriteRenderer>();
        image2.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;
        image2.GetComponent<SpriteRenderer>().sprite = sprite;
        image2.transform.position = new Vector3 (anchor.position.x - offset, anchor.position.y, 11);
        image2.transform.parent = cameraRef.transform;
        startPos = anchor.position;
    }               

    void Update() {
        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        image2.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);

        if(speed > 0) { 
            if ((transform.position.x - sprite.bounds.max.x) > cameraRef.ScreenToWorldPoint(new Vector3(cameraRef.pixelWidth, 0, 0)).x)
                transform.position =  image2.transform.position - new Vector3(offset, 0, 0);
            if ((image2.transform.position.x - sprite.bounds.max.x) > cameraRef.ScreenToWorldPoint(new Vector3(cameraRef.pixelWidth, 0, 0)).x)
                image2.transform.position = transform.position - new Vector3(offset, 0, 0);
        }
        else {
            if (transform.position.x < cameraRef.ScreenToWorldPoint(new Vector3(0, 0, 0)).x)
                transform.position = image2.transform.position + new Vector3(offset, 0, 0);
            if (image2.transform.position.x < cameraRef.ScreenToWorldPoint(new Vector3(0, 0, 0)).x)
                image2.transform.position = transform.position  + new Vector3(offset, 0, 0);

        }
    }
}
