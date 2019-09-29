using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] Text scoreDisplay;

    int score;

    private void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        if (FindObjectsOfType<GameSession>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        //scoreDisplay.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //scoreDisplay.text = score.ToString();
    }

    public int GetScore()
    {
        return score;
    }

    public void addToScore(int toAdd)
    {
        score += toAdd;
    }

    public void ResetGame()
	{
		Destroy(gameObject);
	}
}
