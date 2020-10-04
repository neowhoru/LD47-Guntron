using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{
    // Update is called once per frame

    private void Start()
    {
        Invoke(nameof(StartGame),2);
    }

    void StartGame()
    {
        SceneManager.LoadSceneAsync("GameScene");
    }
}