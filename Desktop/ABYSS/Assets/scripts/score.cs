using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class score : MonoBehaviour {
    public float scorepoint= 0.0f;
    public Text scoretext;
    public int difficulty_level = 1;
    public int max_level = 10;
    public int score_to_nextlevel = 10;
    public bool death_s = false;
    public death_menu death;
        // Use this for initialization
	void Start () {
        scoretext.text = "GAME BEGINS";
	}
	
	// Update is called once per frame
	void Update () {
        if (death_s)
            return;
        if (scorepoint >= score_to_nextlevel)
        {
            LevelUp();
            
        }
        scorepoint += Time.deltaTime;
        scoretext.text = ((int)scorepoint).ToString();
	}
    void LevelUp()
    {
        if (difficulty_level == max_level)
            return;

        score_to_nextlevel *= 2;
        difficulty_level++;
        GetComponent<movement>().speedset(difficulty_level);

    }
    public void death_score()
    {
        death_s = true;
        if(PlayerPrefs.GetFloat("highscore") < scorepoint)
             PlayerPrefs.SetFloat("highscore", scorepoint);
        death.endmenu(scorepoint);
    }
}
 