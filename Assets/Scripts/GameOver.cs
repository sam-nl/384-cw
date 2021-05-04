using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void LoadGame(){
        SceneManager.LoadScene("Level");
    }
    public void LoadMenu(){
        SceneManager.LoadScene("Menu");
    }
}
