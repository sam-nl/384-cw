using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour{

    public static int gridHeight = 20;
    public static int gridWidth = 10;
    public static int score = 0;
    public Text interfaceScore;

    public static int rowsClearedThisTurn = 0;

    public static int singleScore = 40;
    public static int doubleScore = 100;
    public static int tripleScore = 300;
    public static int tetrisScore = 1200;

    public static Transform[,] grid = new Transform[gridWidth,gridHeight];

    // Start is called before the first frame update
    void Start(){
        MakeNewPiece();
    }

    // Update is called once per frame
    void Update() {
        updateScore();
        string s = score.ToString();
        interfaceScore.text = "Score:   "+s;
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
        rowsClearedThisTurn += 1;
    }

    public void ClearRows(){
        bool looping = true;
        while (looping){
            looping = false;
            for (int y = 0; y<gridHeight; y++){
                if (CheckRowFull(y)){
                    ClearRow(y);
                    looping = true;
                }
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
        SceneManager.LoadScene("GameEnd");
    }

    public void updateScore(){
        if (rowsClearedThisTurn > 0){
            switch(rowsClearedThisTurn){
                case 1:
                    addSingle();
                    break;
                case 2:
                    addDouble();
                    break;  
                case 3:
                    addTriple();
                    break;
                case 4:
                    addTetris();
                    break;
            }
            rowsClearedThisTurn = 0;
        }
    }

    public void addSingle(){
        score = score + singleScore;
    }

    public void addDouble(){
        score = score + doubleScore; 
    }

    public void addTriple(){
        score = score + tripleScore;
    }

    public void addTetris(){
        score = score + tetrisScore;
    }
}