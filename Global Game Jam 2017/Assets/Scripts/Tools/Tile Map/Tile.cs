using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class Tile : MonoBehaviour {

    private bool _needsUpdate;

    public enum Type { AIR, GROUND }
    private Type tileType = Type.AIR;
    public Type GetTileType() { return tileType; }
    public void SetTileType(Type type) { tileType = type; _needsUpdate = true; }

    public enum Shape { FULL, FULL_TRI, HALF, TOP_HALF_TRI, BOTTOM_HALF_TRI, NONE }
    private Shape tileShape = Shape.FULL;
    public Shape GetTileShape() { return tileShape; }
    public void SetTileShape(Shape shape) { tileShape = shape; _needsUpdate = true; }

    //sprite ref
    private int spriteID;
    public string GetSpriteName() { return "Tile_" + (int)tileType + "_" + (int)tileShape + "_" + spriteID; }
    public int GetSpriteID() { return spriteID; }
    public void SetSpriteID(int id) { spriteID = id; _needsUpdate = true; }

    public void Init(Type type, Shape shape, int spriteID){
        tileType = type;
        tileShape = shape;
        this.spriteID = spriteID;
    }

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
        if(_needsUpdate) {  
            string spriteName = "Tile_" + (int)tileType + "_" + (int)tileShape + "_" + spriteID;
            GetComponent<SpriteRenderer>().sprite = ResourceManager.GetInstance().GetSpriteManager().GetSprite(spriteName);
            ChangeColliderShape(GetComponent<PolygonCollider2D>(), tileShape);
            if (GetTileType() == Type.AIR) {
                gameObject.layer = 4;
                GetComponent<PolygonCollider2D>().isTrigger = true;
            }
            else {
                gameObject.layer = gameObject.transform.parent.gameObject.layer;
                GetComponent<PolygonCollider2D>().isTrigger = false;
            }
            _needsUpdate = false;
        }
	
	}
    public static GameObject GenerateTile(Sprite testSprite, Transform parent, float tileWidth, float tileHeight, int x, int y, int scaleX, int scaleY, int rotation, Type type, Shape shape, int spriteID) {
        //Create a new game object
        GameObject newCell = new GameObject("Tile: " + x + " " + y);
        //Attach sprite renderer and tile components, colliders are attached in a switch
        newCell.AddComponent<SpriteRenderer>();
        newCell.AddComponent<Tile>();
        Tile newTile = newCell.GetComponent<Tile>();
        //Init the tile
        newTile.Init(type, shape, spriteID);
        //We are going to add the polygon collider BEFORE the sprite.  This way we can save load time because unity wont try to infer the shape
        newCell.AddComponent<PolygonCollider2D>();
        newTile.ChangeColliderShape(newCell.GetComponent<PolygonCollider2D>(), newTile.tileShape);

        newTile.Init(type, shape, spriteID);
        newCell.transform.SetParent(parent);
        newCell.transform.position = new Vector3(parent.position.x + (x * tileWidth), parent.position.y - (y * tileHeight), parent.position.z);
        newCell.transform.localScale = new Vector3(scaleX, scaleY, 1.0f);
        newCell.transform.Rotate(new Vector3(0.0f, 0.0f, rotation));
        newCell.layer = parent.gameObject.layer;

        //Now we infer which sprite we need
        string spriteName = "Tile_" + (int)newTile.tileType + "_" + (int)newTile.tileShape + "_" + newTile.spriteID;
        newTile.GetComponent<SpriteRenderer>().sprite = ResourceManager.GetInstance().GetSpriteManager().GetSprite(spriteName);

        if (newTile.GetTileType() == Type.AIR) {
            newCell.layer = 4; //ignore raycast layer
            newCell.GetComponent<PolygonCollider2D>().isTrigger = true;
        }

        return newCell;
    }

    void ChangeColliderShape(PolygonCollider2D collider, Shape newTileShape) {
          //Hard code the shapes for better speed 
        Vector2[] path;
        switch (tileShape)
        {
            case (Tile.Shape.NONE):
                //The shape is none so the collider shape doesnt matter, just set it to a trigger so we dont collide with it      
                collider.isTrigger = true;
                break;
            case (Tile.Shape.FULL):
                path = new Vector2[4];
                path[0] = new Vector2(-1.5f, 1.5f);
                path[1] = new Vector2(-1.5f, -1.5f);
                path[2] = new Vector2(1.5f, -1.5f);
                path[3] = new Vector2(1.5f, 1.5f);
                collider.SetPath(0, path);
                break;
            case (Tile.Shape.FULL_TRI):
                path = new Vector2[3];
                path[0] = new Vector2(-1.5f, 1.5f);
                path[1] = new Vector2(-1.5f, -1.5f);
                path[2] = new Vector2(1.5f, -1.5f);
                collider.SetPath(0, path);
                break;
            case (Tile.Shape.HALF):
                path = new Vector2[4];
                path[0] = new Vector2(-1.5f, -1.5f);
                path[1] = new Vector2(1.5f, -1.5f);
                path[2] = new Vector2(1.5f, 0.0f);
                path[3] = new Vector2(-1.5f, 0.0f);
                collider.SetPath(0, path);
                break;
            case (Tile.Shape.TOP_HALF_TRI):
                path = new Vector2[4];
                path[0] = new Vector2(-1.5f, 1.5f);
                path[1] = new Vector2(-1.5f, -1.5f);
                path[2] = new Vector2(1.5f, -1.5f);
                path[3] = new Vector2(1.5f, 0.0f);
                collider.SetPath(0, path);
                break;
            case (Tile.Shape.BOTTOM_HALF_TRI):
                path = new Vector2[3];
                path[0] = new Vector2(-1.5f, -1.5f);
                path[1] = new Vector2(1.5f, -1.5f);
                path[2] = new Vector2(-1.5f, 0.0f);
                collider.SetPath(0, path);
                break;
        }
    }
}
