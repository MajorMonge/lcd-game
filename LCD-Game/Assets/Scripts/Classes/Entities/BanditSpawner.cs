using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class BanditSpawner : LCDGameObject
{
    private static BanditSpawner _instance;
    public static BanditSpawner Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<BanditSpawner>();
            }

            return _instance;
        }
    }

    private int _banditsSpawned = 0;
    public int BanditsSpawned
    {
        get
        {
            return _banditsSpawned;
        }
        set
        {
            value = Mathf.Clamp(value, 0, maxBandits);

            _banditsSpawned = value;
        }
    }

    private static Dictionary<Position, bool> _allocatedPositions;
    private static Dictionary<Position, bool> AllocatedPositions
    {
        get
        {
            if (_allocatedPositions == null)
                InitializePositions();

            return _allocatedPositions;
        }
        set
        {
            if (_allocatedPositions == null)
                InitializePositions();

            _allocatedPositions = value;
        }
    }

    [Range(0,3)]
    public int maxBandits = 2;
    public float spawnFirstBanditTime = 4f;
    public float spawnNextBanditsTime = 5f;

    private float spawnTime;

    new void Start()
    {
        base.Start();

        spawnTime = spawnFirstBanditTime;
    }

    private void Update()
    {
        if (spawnTime <= 0 && BanditsSpawned < maxBandits)
        {
            Position[] pos = GetAvailablePositions();
            Spawn(pos[UnityEngine.Random.Range(0, pos.Length)]);
            spawnTime = spawnNextBanditsTime;
        }

        if (BanditsSpawned != maxBandits)
        {
            spawnTime -= Time.deltaTime;
        }
    }

    private void Spawn(Position position)
    {
        GameObject bandit = Instantiate(new GameObject("Bandit" + BanditsSpawned++.ToString()), transform);
        bandit.AddComponent<Bandit>().Position = position;
    }

    public void ManageGraphic(Position oldPos, Position newPos)
    {
        Sprites["Bandit" + (char)newPos].Lit = true;
        AllocatedPositions[newPos] = true;

        if (Enum.IsDefined(typeof(Position), oldPos))
        {
            Sprites["Bandit" + (char)oldPos].Lit = false;
            AllocatedPositions[oldPos] = false;
        }
    }

    private static void InitializePositions()
    {
        _allocatedPositions = new Dictionary<Position, bool>
        {
            { Position.LEFT, false },
            { Position.MIDDLE, false },
            { Position.RIGHT, false }
        };
    }

    public Position[] GetAvailablePositions()
    {
        List<Position> positions = new List<Position>();

        foreach (var position in AllocatedPositions)
        {
            if (position.Value == false)
            {
                positions.Add(position.Key);
            }
        }

        return positions.ToArray();
    }

    public void DisallocatePosition(Position pos)
    {
        AllocatedPositions[pos] = false;
        Sprites["Bandit" + (char)pos].Lit = false;
    }

    public override void Reset()
    {
        Bandit[] bandits = FindObjectsOfType<Bandit>();

        foreach (var bandit in bandits)
        {
            Destroy(bandit.gameObject);
        }

        _allocatedPositions = new Dictionary<Position, bool>
        {
            { Position.LEFT, false },
            { Position.MIDDLE, false },
            { Position.RIGHT, false }
        };
    }
}
