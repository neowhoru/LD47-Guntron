using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishedGameController : MonoBehaviour
{
    public Text scoreText;

    public Text timeText;
    // Start is called before the first frame update
    void Start()
    {
        int score = PlayerPrefs.GetInt("SCORE", 0);
        scoreText.text = "YOUR SCORE: " + score.ToString().PadLeft(8, '0');
        
        // ToDo: if Time add tmer here

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButton("Fire1"))
        {
            SceneManager.LoadSceneAsync("TitleScene");
        } 
    }
}
