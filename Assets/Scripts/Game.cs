using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    public static int gridHeight = 20;
    public static int gridWidth = 10;
    public static int count = 0;

    public static Transform[,] grid = new Transform[gridWidth,gridHeight];

    // Start is called before the first frame update
    void Start(){
        MakeNewPiece();
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void test(){

    }
    public bool CheckBoundries(Vector3 coords){

        return (coords.x >= 0 && coords.x < gridWidth && coords.y >= 0);

    }

    public Vector3 AlignToGrid (Vector2 coords){
        return new Vector3 (Mathf.Round(coords.x),Mathf.Round(coords.y),0);
    }

    public string GetRandomPiece(){
        int randNum = Random.Range(0,7);
        string pieceName = "YellowT";

        switch (randNum){

            case 0:
                pieceName = "BlueSquare";
                break;
            case 1:
                pieceName = "GreenLine";
                break;
            case 2:
                pieceName = "OrangeL";
                break;
            case 3:
                pieceName = "PinkJ";
                break;
            case 4:
                pieceName = "PurpleS";
                break;
            case 5:
                pieceName = "RedZ";
                break;
            case 6:
                pieceName = "YellowT";
                break;                        
        }
        return "prefabs/" + pieceName;
    }
    public void MakeNewPiece(){
        string pieceName = GetRandomPiece();
        GameObject piece = (GameObject)Instantiate(Resources.Load(pieceName,typeof(GameObject)),new Vector2(4,20), Quaternion.identity);
    }

    public void UpdateGrid(Shape shape){

        
        for (int y = 0; y < gridHeight; ++y){
            for (int x = 0; x < gridWidth; ++x){
                if (grid[x,y] != null){
                    if (grid[x,y].parent == shape.transform){
                        grid[x,y] = null;
                    }
                }
            }
        }
        foreach (Transform tile in shape.transform){
            Vector2 coords = tile.position;

            if (coords.y < gridHeight) {
                grid[(int)coords.x, (int)coords.y] = tile;
            }
        }
        
    }

    public Transform GetShapeFromGrid (Vector2 coords){
        if (coords.y >= gridHeight || coords.x >= gridWidth || coords.y < 0 || coords.x < 0){
            return null;
        } else {
            return grid[(int)coords.x,(int)coords.y];
        }
    }
}
