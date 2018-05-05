using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedBg : MonoBehaviour {

	public float animSpeedX = 0.2f, animSpeedY = 0.4f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 offset = new Vector2 (Time.time * animSpeedX, Time.time * animSpeedY);
		GetComponent<Renderer>().material.mainTextureOffset = offset;
	}
}
