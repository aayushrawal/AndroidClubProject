# ANDROIDCLUBPROJECT
This repository is for an android application made using unity engine for Android Club Project submission 2017-18.

The application is an android game using unity engine and android studio, this is an arcade game which involves single player that will be participating to crack the designed levels of the game.
The game design is to be an infinite runner game (temple run), this involves many changeling levels for player to crack. As the game progresses the speed as well increases as time making it difficult to play ahead.

MAIN COMPONENTS:

PLAYER MOVEMENT
using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour
{
    private CharacterController controller;
    public float speed = 5.0f;
    public Vector3 moves;
    public float vertical_velocity = 0.0f;
    public float gravity = 12.0f;
    public bool isDead = false;
    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
      if(isDead)
            return;
        moves = Vector3.zero;
        //AXIS CONTROLS
        if (controller.isGrounded)
        {
            vertical_velocity = 0.0f;
        }
        else
        {
            vertical_velocity -= gravity * Time.deltaTime;
        }
        // X-LEFT AND RIGHT
        moves.x = Input.GetAxisRaw("Horizontal") * speed;
        // setting controls for touch screen using mouse event.
        if (Input.GetMouseButton(0))
        {

            if (Input.mousePosition.x > Screen.width / 2)
                moves.x =speed;
            else
                moves.x = -speed;
        }

        // Y- UP AND DOWN
        moves.y = vertical_velocity;
        // Z- FORWARD AND BACKWARD
        moves.z = speed;
        controller.Move(moves * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.C))
        {
           /// vertical_velocity = 0;
            vertical_velocity = 8;
            moves.y = vertical_velocity;
            vertical_velocity = 0;
        }

    }
    public void speedset(float setspeed)
    {
        speed = 5.0f + (setspeed*3);
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.point.z > transform.position.z + controller.radius)
        {
            death();
        }
    }
  private void death()
    {
        isDead = true;
       GetComponent<score>().death_score();
    }
}


Camera 
using UnityEngine;
using System.Collections;

public class cameramotor : MonoBehaviour {
    private Transform lookAt;
    private Vector3 startoffset;
    private Vector3 movevector;
    private float transition = 0.0f;
    private float animationduration = 2.0f;
    private Vector3 animationoffset = new Vector3(0, 3, 3);

	// Use this for initialization
	void Start () {
        lookAt= GameObject.FindGameObjectWithTag("Player").transform;
        startoffset = transform.position - lookAt.position;
	}
	
	// Update is called once per frame
	void Update () {
        movevector = lookAt.position+ startoffset;

        // X camera
        movevector.x = 0;
         
        // Y camera
        movevector.y = Mathf.Clamp(movevector.y, 3, 9);

        // camera

        if (transition > 1.0f)
        {
            transform.position = movevector;
        }

        else
        {
            transform.position = Vector3.Lerp(movevector + animationoffset, movevector, transition);
            transition += Time.deltaTime * 1 / animationduration;
            transform.LookAt(lookAt.position + Vector3.up);
        }
        
	}
}


Death collision

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

MAIN MENU
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

SCORE
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
 

TILE MANAGER
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TILE_MANAGER : MonoBehaviour
{
    public GameObject[] tileprefabs;

    private Transform playerTransform;

    public float spwanZ = 20.0f;
    public float length = 20.0f;
    public int no_tiles = 7;
    private float safezone = 30.0f;
    private List<GameObject> active;
    private int lastprefb = 0;
    // Use this for initialization
    private void Start()
    {
        active = new List<GameObject>();

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i = 0; i < no_tiles; i++)
        {
            spawn();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (playerTransform.position.z - safezone > (spwanZ - (no_tiles * length)))
        {
            spawn();
            delete();
        }
    }
    private void spawn(int prefabindex = -1)
    {
        GameObject go;
        go = Instantiate(tileprefabs[random()]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spwanZ;
        spwanZ += length;
        active.Add(go);
    }
    private void delete()
    {
        Destroy(active[0]);
        active.RemoveAt(0);
    }
    public int random()
    {
        if (tileprefabs.Length <= 1)
            return 0;
        int randomindex = lastprefb;

        while (randomindex == lastprefb)
        {
            randomindex = Random.Range(0, tileprefabs.Length);

        }
        lastprefb = randomindex;
        return randomindex;

    }
}
