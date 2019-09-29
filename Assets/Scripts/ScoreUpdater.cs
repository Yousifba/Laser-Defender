using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdater : MonoBehaviour
{
    Text scoreDisplay;

    // Start is called before the first frame update
    void Start()
    {
        scoreDisplay = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreDisplay.text = FindObjectOfType<GameSession>().GetScore().ToString();
    }
}
