using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

    GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        // Temporary vector
        Vector2 temp = player.transform.position;
        // Assign value to Camera position
        transform.position = new Vector3(temp.x, temp.y, -10);
    }
}
