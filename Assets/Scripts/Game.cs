using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    public static int gridHeight = 20;
    public static int gridWidth = 10;

    public static Transform[,] grid = new Transform[gridHeight,gridWidth];

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

        return (coords.x > -gridWidth/2 && coords.x <= gridWidth/2 && coords.y > -gridHeight/2);

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
        GameObject piece = (GameObject)Instantiate(Resources.Load(pieceName,typeof(GameObject)),new Vector2(0,12), Quaternion.identity);
    }

    public void UpdateGrid(Shape shape){

        for (int y = 0; x < gridHeight; y++){
            for (int y = 0; x < gridHeight; y++){
                if (grid[x,y] != null){
                    
                }
            }
        }

    }
}
