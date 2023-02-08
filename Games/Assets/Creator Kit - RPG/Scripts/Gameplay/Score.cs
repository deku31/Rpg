using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static Score instance;

    public static int score;
    public static int finishedquest;
    public static int specialquest;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Score.score = 0;
        Score.finishedquest = 0;
        Score.specialquest = 0;
    }

    public void AddPoint()
    {
        score += 1;
    }

    public void FinishedQuest()
    {
        finishedquest += 1;
    }
    
    public void SpecialQuest()
    {
        specialquest += 1;
    }
}
