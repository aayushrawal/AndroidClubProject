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
