public class Cylinder : LCDGameObject {

    private LCDSprite _ownSprite;

    new void Start()
    {
        base.Start();
        Sprites.Remove("Cylinder");
        FindObjectOfType<Sheriff>().BulletCountHasChanged += UpdateBulletCount;

        _ownSprite = GetComponent<LCDSprite>();
        _ownSprite.Lit = true;
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

    public override void Reset()
    {
        _ownSprite.Lit = true;

        foreach (var sprite in Sprites.Values)
        {
            sprite.Lit = true;
        }
    }
}
