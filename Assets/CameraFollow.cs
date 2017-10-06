using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		print (player.ToString ());
	}
	
	// LateUpdate is called after player position is calculated
	void LateUpdate () {
		transform.position = player.transform.position;
	}
}
