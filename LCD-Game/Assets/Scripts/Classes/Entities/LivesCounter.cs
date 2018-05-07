using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesCounter : LCDGameObject {

    private void ChangeCounter(Sheriff sheriff)
    {
        foreach (var sprite in Sprites.Values)
        {
            sprite.Lit = false;
        }

        for (int i = 0; i < sheriff.Lives; i++)
        {
            Sprites["Star" + i].Lit = true;
        }
    }

	new void Start()
    {
        base.Start();

        FindObjectOfType<Sheriff>().LivesHasChanged += ChangeCounter;
    }

    public override void Reset()
    {
        foreach (var sprite in Sprites.Values)
        {
            sprite.Lit = true;
        }
    }
}
