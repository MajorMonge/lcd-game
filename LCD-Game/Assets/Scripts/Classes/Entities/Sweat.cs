using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sweat : LCDGameObject {

    private Sheriff _sheriff;

	private IEnumerator Blink()
    {
        while (true)
        {
            foreach (var drop in Sprites.Values)
            {
                drop.Lit = true;
            }

            yield return new WaitForSeconds(0.3f);

            foreach (var drop in Sprites.Values)
            {
                drop.Lit = false;
            }

            yield return new WaitForSeconds(0.3f);
        }
    }

    private void DisplaySweat(Sheriff sheriff)
    {
        StopAllCoroutines();

        foreach (var drop in Sprites.Values)
        {
            drop.Lit = false;
        }

        if (sheriff.Panic == false)
        {
            return;
        }

        if (name == "SweatDrops" + (char)sheriff.Position)
        {
            StartCoroutine("Blink");
        }
    }

    new void Start()
    {
        base.Start();
        _sheriff = FindObjectOfType<Sheriff>();
        _sheriff.PositionHasChanged += DisplaySweat;
        _sheriff.IsInPanic += DisplaySweat;
    }

    public override void Reset()
    {
        foreach (var sprite in Sprites.Values)
        {
            sprite.Lit = false;
        }
    }
}
