using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    public Text userName;
    public Text score;

    public static string playerName;

    private int killCounter;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        userName.text = playerName;
        killCounter = 0;   
    }

    public void IncrementPoints()
    {
        killCounter++;
    }

    void Update()
    {
        score.text = "Kill Counter: " + killCounter;
    }
}
