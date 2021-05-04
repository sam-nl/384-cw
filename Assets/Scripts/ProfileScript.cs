using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProfileScript : MonoBehaviour
{
    int count = 1;
    string newProfName = "";
    public Text currentProfName;
    string currentPlayer = "Guest";
    string currentPlayerID = "P1";
    int currentHighscore = 0;
    public Text textHighScore;
    string nextPlayerID = "P1";
    Dropdown dropdown;

    
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString(currentPlayerID,currentPlayer);
        updateCurrentPlayer();
        dropdown = Dropdown.FindObjectsOfType<Dropdown>()[0];
        loadDropdown();
        loadName();
        
    }

    // Update is called once per frame
    void Update()
    {
        updateCurrentPlayer();
        Debug.Log(PlayerPrefs.GetString("player"));
        Debug.Log(PlayerPrefs.GetString("playerID"));
        Debug.Log(currentHighscore.ToString());
    }

    void updateCurrentPlayer(){
        PlayerPrefs.SetString("player",currentPlayer);
        PlayerPrefs.SetString("playerID",currentPlayerID);
        currentHighscore = PlayerPrefs.GetInt(currentPlayerID+"highscore");
        textHighScore.text = currentHighscore.ToString();
        currentProfName.text = currentPlayer;
    }

    public void ReadInput(string input){
        newProfName = input;
        Debug.Log(newProfName);
    }

    public void MakeNewProfile(){
        if (newProfName != ""){
            PlayerPrefs.SetString(nextPlayerID,newProfName);
            PlayerPrefs.SetInt(nextPlayerID+"levelpref",1);
            loadDropdown();
            
        }
    }

    public void loadName(){
        currentProfName.text = currentPlayer;
        PlayerPrefs.SetString("player",currentPlayer);
    }

    public void loadDropdown(){
        List<string> players = new List<string>();
        count = 1;
        
        dropdown.options.Clear();
        while (PlayerPrefs.GetString("P" + count.ToString()) != ""){
            nextPlayerID = "P" + (count+1).ToString();
            players.Add(PlayerPrefs.GetString("P" + count.ToString()));
            count += 1;
        }

        foreach(string player in players){
            dropdown.options.Add(new Dropdown.OptionData() {text = player});
        }
    }

    public void selectProfile(Dropdown drop)
    {
        currentPlayer = drop.options[drop.value].text;
        count = 1;
        while (PlayerPrefs.GetString("P" + count.ToString()) != currentPlayer){
            count += 1;
            if (count > 1000){
                break;
            }
        }
        currentPlayerID = "P" + count.ToString();
        updateCurrentPlayer();
        loadName();
        
    }

    public void GoToMenu(){
        SceneManager.LoadScene("Menu");
    }
}
