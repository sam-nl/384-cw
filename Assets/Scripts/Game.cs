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
        count = count + 1;
        if (count == 2000){
            count = 0;
            string str = ""; 
            int total = 0;
            for (int y = 0; y < gridHeight; ++y){
                for (int x = 0; x < gridWidth; ++x){
                    if (grid[x,y] == null){
                        str = str + "0";
                    }else{
                        str = str + "X";
                    }
                    total +=1;
                }
                str = str + " | ";
            }
            Debug.Log(str);
            Debug.Log(total);
        }
        
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
            AlignToGrid(coords);
            if (coords.y < gridHeight) {
                grid[(int)System.Math.Round(coords.x, 0), (int)System.Math.Round(coords.y, 0)] = tile;
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

    public bool CheckRowFull(int y){
        for (int x = 0; x<gridWidth; x++){
            if (grid[x,y] == null){
                return false;
            }
        }
        return true;
    }

    public void ClearRow(int y){
        for (int x = 0; x<gridWidth; x++){
            Destroy (grid[x,y].gameObject);
            grid[x,y] = null;
        }
        ShiftRowsAboveDown(y);
    }

    public void ClearRows(){
        bool looping = true;
        while (looping){
            for (int y = 0; y<gridHeight; y++){
                if (CheckRowFull(y)){
                    ClearRow(y);
                    break;
                }
                looping = false;
            }
        }
        
    }

    public void ShiftRowDown(int y){
        for (int x = 0; x<gridWidth; x++){
            if (grid[x,y]!=null){
                grid[x,y-1] = grid[x,y];
                grid[x,y] = null;
                grid[x,y-1].position += new Vector3(0, -1, 0);
            }
        }
    }

    public void ShiftRowsAboveDown(int y){
        for (int i = y+1; i<gridHeight; i++){
            ShiftRowDown(i);
        }
    }

    public bool CheckHeight(Shape shape){
        foreach (Transform tile in shape.transform){
            Vector2 coords = tile.position;
            AlignToGrid(coords);
            if (coords.y > gridHeight) {
                return true;
            }
        }
        return false;
    }

    public void GameOver(){
        Application.LoadLevel("GameOver");
    }
    
}
