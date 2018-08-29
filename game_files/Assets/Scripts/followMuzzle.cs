using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followMuzzle : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject flare = GameObject.FindGameObjectWithTag("Flare");
        transform.position = flare.transform.position;
    }
	
	// Update is called once per frame
	void Update () {

	}
}
