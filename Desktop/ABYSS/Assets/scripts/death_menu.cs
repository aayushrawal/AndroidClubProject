using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class death_menu : MonoBehaviour {
    public Text scoretext;
    public Image background;
    private bool show = false;
    public float transition=0.0f;
	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {
        if (!show)
            return;
        transition += Time.deltaTime;
        background.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, transition);

	}
    public void endmenu(float score)
    {
        gameObject.SetActive(true);
        scoretext.text = ((int)score).ToString();
        show = true;
    }
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void menu()
    {
        SceneManager.LoadScene("menu"); 
    }
}