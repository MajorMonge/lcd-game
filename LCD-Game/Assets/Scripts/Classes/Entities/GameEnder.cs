using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnder : LCDText {

    public string gameEndSound;

	new void Start () {
        base.Start();

        FindObjectOfType<Sheriff>().LivesHasChanged += CheckIfGameIsOver;
	}

    private void CheckIfGameIsOver(Sheriff sheriff)
    {
        if (sheriff.Lives == 0)
        {
            SoundManager.Instance.PlaySound(gameEndSound);
            Time.timeScale = 0;
            Lit = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Start"))
        {
            StartCoroutine("StartResetExecution");
        }
	}

    private IEnumerator StartResetExecution()
    {
        Time.timeScale = 0;
        LCDSprite[] objs = FindObjectsOfType<LCDSprite>();

        foreach (var obj in objs)
        {
            obj.Lit = true;
        }

        yield return new WaitForSecondsRealtime(0.5f);

        foreach (var obj in objs)
        {
            obj.Lit = false;
        }

        LCDGameObject[] gObjs = FindObjectsOfType<LCDGameObject>();

        foreach (var go in gObjs)
        {
            go.Reset();
        }

        Lit = false;

        Time.timeScale = 1f;
    }
}
