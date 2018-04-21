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
