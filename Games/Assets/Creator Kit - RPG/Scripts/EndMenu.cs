using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndMenu : MonoBehaviour
{
    public TextMeshProUGUI Result;
    public TextMeshProUGUI Description;

    // Start is called before the first frame update
    void Start()
    {
        Result.text = "";
    }

    public void EndGame()
    {
        if(Score.score < 3)
        {
            Result.text = "You are INTP!";
            Description.text = "INTP";
        }
    }
}
