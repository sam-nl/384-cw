using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Text startLevel;
    // Start is called before the first frame update
    void Start()
    {
        startLevel.text = "1";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLevel(float level){
        Game.startLevel = (int)level;
        startLevel.text = level.ToString();
    }

    public void StartGame(){
        SceneManager.LoadScene("Level");
    }

    public void RunTutorial(){
        Game.tutorial = true;
        SceneManager.LoadScene("Tutorial");
    }

}
