using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjecManipulation : MonoBehaviour {
	
	public float rotSpeed = 1;
	public float zoomSpeed;
    private Camera mainCamera;
	public GameObject objectCamera;

	// Use this for initialization
	void Start () {
		mainCamera = objectCamera.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0, -(Input.GetAxis("Mouse X"))* Time.deltaTime* rotSpeed * Mathf.Deg2Rad , 0);
		if (Input.GetAxis("Mouse ScrollWheel") < 0 && mainCamera.fieldOfView < 75)
            {
                mainCamera.fieldOfView += zoomSpeed* Time.deltaTime;
            }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0 && mainCamera.fieldOfView > 45) 
            {
                mainCamera.fieldOfView -= zoomSpeed* Time.deltaTime;
            }
	}
}
