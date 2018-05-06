using UnityEngine;
using System.Collections;

public class Cylinder : LCDGameObject {

    private LCDSprite _ownSprite;

    new void Start()
    {
        base.Start();
        FindObjectOfType<Sheriff>().BulletCountHasChanged += UpdateBulletCount;

        _ownSprite = GetComponent<LCDSprite>();
        _ownSprite.Lit = true;

        foreach (var round in Sprites.Values)
        {
            round.Lit = true;
        }
    }

    private void UpdateBulletCount(Sheriff sheriff)
    {
        int bullets = sheriff.Bullets;

        foreach (LCDSprite round in Sprites.Values)
        {
            round.Lit = false;
        }

        for (int i = 0; i < bullets; i++)
        {
            Sprites["Round" + i.ToString()].Lit = true;
        }
    }
}
