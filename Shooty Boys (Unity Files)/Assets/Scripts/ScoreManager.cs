using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour {

    TextMeshProUGUI scoreText;
    GameManager gameManager;
    public GameObject gameManagerObject;

	// Use this for initialization
	void Start () {
        scoreText = gameObject.GetComponent<TextMeshProUGUI>();
        gameManager = gameManagerObject.GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
        scoreText.SetText("Score: " + gameManager.getScore().ToString());
	}
}
