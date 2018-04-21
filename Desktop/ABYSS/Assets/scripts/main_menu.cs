using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
public class main_menu : MonoBehaviour {
    public Text highscore; 
	// Use this for initialization
	void Start () {

        highscore.text = "HIGHSCORE : " + (int)PlayerPrefs.GetFloat("highscore");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void play()
    {
        SceneManager.LoadScene("abyss");
    }
}
