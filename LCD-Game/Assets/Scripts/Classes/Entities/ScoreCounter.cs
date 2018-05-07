using UnityEngine;
using System.Collections;

public class ScoreCounter : LCDText
{
    private void ChangeScore(Sheriff sheriff)
    {
        Text.text = sheriff.Score.ToString();
    }

    new void Start()
    {
        base.Start();

        FindObjectOfType<Sheriff>().ScoreHasChanged += ChangeScore;
    }

}
