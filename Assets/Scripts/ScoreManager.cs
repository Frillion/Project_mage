using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int score;
    Text scoretext;


    void Awake()
    {
        scoretext = GetComponent<Text>();
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoretext.text = "Score: " + score;
    }
}
