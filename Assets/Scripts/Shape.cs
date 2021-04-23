using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour{
    float fallTime = 0;
    public float fallRate = 1;
    public float moveSpeed = 1;

    public bool landed = false;
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        if (!landed){
            GetInput();
        }
    }

    void GetInput() {
        if (Input.GetKeyDown(KeyCode.RightArrow)){
            Vector3 vec = new Vector3(moveSpeed,0,0);
            vec = FindObjectOfType<Game>().AlignToGrid(vec);
            if (CheckMoveable(vec)){
                transform.position += vec;
                FindObjectOfType<Game>().UpdateGrid(this);
            }

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)){
            
            Vector3 vec = new Vector3(-moveSpeed,0,0);
            vec = FindObjectOfType<Game>().AlignToGrid(vec);
            if (CheckMoveable(vec)){
                transform.position += vec;
                FindObjectOfType<Game>().UpdateGrid(this);
            }

        }
        if (Input.GetKeyDown(KeyCode.UpArrow)){
            
            transform.Rotate(0,0,90);
            Vector3 vec = new Vector3(0,0,0);
            vec = FindObjectOfType<Game>().AlignToGrid(vec);
            if (!CheckMoveable(vec)){
                transform.Rotate(0,0,-90);
            }else{
                FindObjectOfType<Game>().UpdateGrid(this);
            }

        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time - fallTime >= fallRate){
            
            Vector3 vec =  new Vector3(0,-moveSpeed,0);
            vec = FindObjectOfType<Game>().AlignToGrid(vec);
            fallTime = Time.time;
            if (CheckMoveable(vec)){
                transform.position += vec;
                FindObjectOfType<Game>().UpdateGrid(this);
            }else{
                landed = true;
                FindObjectOfType<Game>().MakeNewPiece();
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
