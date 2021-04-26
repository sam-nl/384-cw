using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour{
    float fallTime = 0;
    public float fallRate = 1;
    public float moveSpeed = 1;
    int count = 0;
    public bool landed = false;

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        if (!landed){
            GetInput();
            count+=1;
            if (count == 1000){
                count = 0;
                Debug.Log("i am falling the positions for my tiles are: ");
                foreach (Transform tile in this.transform){
                    Vector2 coords = tile.position;
                    FindObjectOfType<Game>().AlignToGrid(coords);
                    Debug.Log("i am a tile the positions: ");
                    Debug.Log((int)System.Math.Round(coords.x, 0));
                    Debug.Log((int)System.Math.Round(coords.y, 0));
                }
            }
            
        }
    }

    void GetInput() {
        if (Input.GetKeyDown(KeyCode.RightArrow)){
            Vector3 vec = new Vector3(moveSpeed,0,0);
            if (CheckMoveable(vec)){
                transform.position += vec;
                FindObjectOfType<Game>().UpdateGrid(this);
            }

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)){
            
            Vector3 vec = new Vector3(-moveSpeed,0,0);
            if (CheckMoveable(vec)){
                transform.position += vec;
                FindObjectOfType<Game>().UpdateGrid(this);
            }

        }
        if (Input.GetKeyDown(KeyCode.UpArrow)){
            
            transform.Rotate(0,0,90);
            Vector3 vec = new Vector3(0,0,0);
            if (!CheckMoveable(vec)){
                transform.Rotate(0,0,-90);
            }else{
                FindObjectOfType<Game>().UpdateGrid(this);
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time - fallTime >= fallRate){
            
            Vector3 vec =  new Vector3(0,-moveSpeed,0);
            fallTime = Time.time;
            if (CheckMoveable(vec)){
                transform.position += vec;
                FindObjectOfType<Game>().UpdateGrid(this);
            }else{
                landed = true;
                FindObjectOfType<Game>().ClearRows();
                if (FindObjectOfType<Game>().CheckHeight(this) == false){
                    FindObjectOfType<Game>().MakeNewPiece();
                }else{
                    FindObjectOfType<Game>().GameOver();
                }
            }
        }

    }
    bool CheckMoveable(Vector3 vec){

        foreach (Transform tile in transform){

            Vector3 coords = FindObjectOfType<Game>().AlignToGrid(tile.position + vec);
            

            if (FindObjectOfType<Game>().CheckBoundries(coords) == false){
                return false;
            }

            if (FindObjectOfType<Game>().GetShapeFromGrid(coords) != null && FindObjectOfType<Game>().GetShapeFromGrid(coords).parent != transform){
                return false;
            }
        }
        return true;
    }
}
