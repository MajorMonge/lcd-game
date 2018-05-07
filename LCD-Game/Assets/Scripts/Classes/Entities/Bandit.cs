using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit : MonoBehaviour {

    private Sheriff sheriff;

    private Position _position;
    public Position Position
    {
        get
        {
            return _position;
        }
        set
        {
            BanditSpawner.Instance.ManageGraphic(_position, value);
            _position = value;
        }
    }

    private void Start()
    {
        StartCoroutine("Act");
        sheriff = FindObjectOfType<Sheriff>();
        sheriff.HasFired += CheckBulletRange;
    }

    private IEnumerator Act()
    {
        yield return new WaitForSeconds(0.7f);
        while (true)
        {
            Fire();
            yield return new WaitForSeconds(0.7f);
            Move();
            yield return new WaitForSeconds(0.7f);
        }
    }

    private void Fire()
    {
        BulletManager.Instance.SpawnBullet(Position, Direction.FORWARD);
    }

    private void Move()
    {
        Position[] pos = BanditSpawner.Instance.GetAvailablePositions();

        if (pos.Length > 0)
        {
            Position = pos[Random.Range(0, pos.Length)];
        }
    }

    private void CheckBulletRange(Sheriff sheriff)
    {
        if (Position == sheriff.Position)
        {
            sheriff.Score += 100;
            Die();
        }
    }

    public void Die()
    {
        SoundManager.Instance.PlaySound("bleep");
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
        BanditSpawner.Instance.BanditsSpawned--;
        BanditSpawner.Instance.DisallocatePosition(Position);
        sheriff.HasFired -= CheckBulletRange;
    }
}
