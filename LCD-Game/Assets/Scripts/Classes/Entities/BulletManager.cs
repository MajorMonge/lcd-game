using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : LCDGameObject
{

    private static BulletManager _instance;
    public static BulletManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<BulletManager>();
            }

            return _instance;
        }
    }

    private Sheriff sheriff;

    private List<string> travellingBullets;

    public float initialBulletTravelTime = 0.7f;
    public float bulletTravelTimeDecrease = 0.0005f;
    public float minBulletTravelTime = 0.4f;

    private float _bulletTravelTime;

    public string dodgeSoundName;

    new void Start()
    {
        base.Start();

        travellingBullets = new List<string>();

        StartCoroutine("UpdateBulletPositions");
        sheriff = FindObjectOfType<Sheriff>();
        sheriff.FromToPositionChanged += CheckIfDodgedBullet;
        _bulletTravelTime = initialBulletTravelTime;
    }

    public void SpawnBullet(Position hPos, Direction dir)
    {
        string b = (char)hPos + "T" + (char)dir;
        travellingBullets.Add(b);
        Sprites["Bullet" + b].Lit = true;
    }

    private IEnumerator UpdateBulletPositions()
    {
        while (true)
        {
            foreach (var sprite in Sprites.Values)
            {
                sprite.Lit = false;
            }

            for (int i = 0; i < travellingBullets.Count; i++)
            {
                travellingBullets[i] = CalculatePosition(travellingBullets[i]);

                if (travellingBullets[i] == "D")
                {
                    continue;
                }

                Sprites["Bullet" + travellingBullets[i]].Lit = true;
            }

            travellingBullets.RemoveAll(x => x == "D");
            
            yield return new WaitForSeconds(_bulletTravelTime);
            _bulletTravelTime -= bulletTravelTimeDecrease;
        }
    }

    private string CalculatePosition(string pos)
    {
        string newPos;
        char[] coords = pos.ToCharArray();

        switch (coords[1])
        {
            case 'T':
                coords[1] = 'M';
                break;
            case 'M':
                coords[1] = 'B';
                break;
            case 'B':
                CheckIfSheriffIsInRange(coords[0]);
                return "D";
            default:
                break;
        }

        newPos = new string(coords);

        return newPos;
    }

    private void CheckIfSheriffIsInRange(char p)
    {
        if ((char)sheriff.Position == p)
        {
            sheriff.TakeDamage();
        }
    }

    private void CheckIfDodgedBullet(Position from, Position to)
    {
        if (travellingBullets.Contains((char)from + "BF"))
        {
            if (!travellingBullets.Contains((char)to + "BF"))
            {
                sheriff.Score += 50;
                SoundManager.Instance.PlaySound(dodgeSoundName);
            }
        }
    }

    public override void Reset()
    {
        travellingBullets = new List<string>();
        _bulletTravelTime = initialBulletTravelTime;
    }
}
