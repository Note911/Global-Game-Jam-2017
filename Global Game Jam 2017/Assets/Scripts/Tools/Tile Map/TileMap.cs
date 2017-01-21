using UnityEngine;
using System.Collections;
using System.IO;
public class TileMap : MonoBehaviour {

    private int _rows, _cols;
    private float _tileWidth = 3;
    private float _tileHeight = 3;
    public Sprite TestSprite1;

    private GameObject[,] _tileMap;
    private TileMapContainer _xmlContainer;


	// Use this for initialization
	void Start () {
        gameObject.layer = 6;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void initMap(int rows, int cols) {
        _rows = rows;
        _cols = cols;
        _tileMap = new GameObject[_rows, _cols];
    }

    public void SaveMap(string levelFilePath) {
        TileMapContainer newContainer = new TileMapContainer();
        newContainer.rows = _rows;
        newContainer.cols = _cols;
        newContainer.cells = new Cell[_rows * _cols];
        for(int i = 0; i < _rows; ++i) {
            for (int j = 0; j < _cols; ++j) {
                Tile workingTile = _tileMap[i, j].GetComponent<Tile>();
                Cell workingCell = newContainer.cells[i + (j * _rows)] = new Cell();
                workingCell.x = i;
                workingCell.y = j;
                workingCell.scaleX = (int)workingTile.transform.localScale.x;
                workingCell.scaleY = (int)workingTile.transform.localScale.y;
                workingCell.rotation = (int)workingTile.transform.rotation.eulerAngles.z;
                workingCell.tileType = (int)workingTile.GetTileType();
                workingCell.tileShape = (int)workingTile.GetTileShape();
                workingCell.spriteID = (int)workingTile.GetSpriteID();
            }
        }
        _xmlContainer = newContainer;
        _xmlContainer.Save(Path.Combine(Application.dataPath, levelFilePath));
    }

    public void LoadMap(string levelFilePath) {
        ClearMap();
        _xmlContainer = TileMapContainer.Load(Path.Combine(Application.dataPath, levelFilePath));

        initMap(_xmlContainer.rows, _xmlContainer.cols);

        for(int i = 0; i < _rows; ++i) {
            for (int j = 0; j < _cols; ++j) {
                Cell currentCell = _xmlContainer.cells[i + (j * _rows)];
                _tileMap[i,j] = Tile.GenerateTile(TestSprite1, this.transform, _tileWidth, _tileHeight, j, i, currentCell.scaleX, currentCell.scaleY, currentCell.rotation, (Tile.Type)(currentCell.tileType), (Tile.Shape)(currentCell.tileShape), currentCell.spriteID);
            }
        }
    }

    public void LoadEmptyMap(int rows, int cols) {
        ClearMap();
        initMap(rows, cols);        
        for(int i = 0; i < _rows; ++i) {
            for (int j = 0; j < _cols; ++j) {
                _tileMap[i,j] = Tile.GenerateTile(TestSprite1, this.transform, _tileWidth, _tileHeight, j, i, 1, 1, 0, Tile.Type.AIR, Tile.Shape.NONE, 0);
            }
        }
    }

    public void ClearMap() {
         for(int i = 0; i < _rows; ++i) {
            for (int j = 0; j < _cols; ++j) {
                Destroy(_tileMap[i, j]);
            }
        }
    }

    public GameObject[,] GetTileMap() {
        return _tileMap;
    }
}
