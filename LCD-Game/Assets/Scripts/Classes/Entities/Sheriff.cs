using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheriff : LCDGameObject
{

    private Position _position;
    public Position Position
    {
        get
        {
            return _position;
        }
        set
        {
            if (FromToPositionChanged != null)
            {
                FromToPositionChanged(_position, value);
            }

            _position = value;
            ManageGraphics();

            if (PositionHasChanged != null)
            {
                PositionHasChanged(this);
            }
        }
    }

    public delegate void SheriffDelegate(Sheriff sheriff);
    public delegate void SheriffComparePositionDelegate(Position oldPos, Position newPos);
    public event SheriffDelegate PositionHasChanged;
    public event SheriffComparePositionDelegate FromToPositionChanged;
    public event SheriffDelegate BulletCountHasChanged;
    public event SheriffDelegate IsInPanic;
    public event SheriffDelegate ScoreHasChanged;
    public event SheriffDelegate LivesHasChanged;
    public event SheriffDelegate HasFired;

    public int maxBullets = 6;
    private bool _panic = false;
    public bool Panic
    {
        get
        {
            return _panic;
        }
        set
        {
            _panic = value;

            if (IsInPanic != null)
            {
                IsInPanic(this);
            }
        }
    }

    private int _score = 0;
    public int Score
    {
        get
        {
            return _score;
        }
        set
        {
            value = Mathf.Clamp(value, 0, int.MaxValue);

            _score = value;

            if (ScoreHasChanged != null)
            {
                ScoreHasChanged(this);
            }
        }
    }

    private int _lives;
    public int Lives
    {
        get
        {
            return _lives;
        }
        set
        {
            value = Mathf.Clamp(value, 0, 3);

            bool changed = (value != _lives && LivesHasChanged != null) ? true : false;

            _lives = value;

            if (changed) LivesHasChanged(this);
        }
    }

    private bool isReloading = false;
    private bool lockHorizontal = false;

    private int _bullets;
    public int Bullets
    {
        get
        {
            return _bullets;
        }
        set
        {
            bool changed = false;
            value = Mathf.Clamp(value, 0, maxBullets);

            if (value != _bullets && BulletCountHasChanged != null)
            {
                changed = true;
            }

            if (value == 0)
            {
                Panic = true;
            }

            _bullets = value;

            if (changed)
            {
                BulletCountHasChanged(this);
            }
        }
    }

    public string moveSoundName;
    public string shootSoundName;
    public string reloadSoundName;
    public string damagedSoundName;

    // Use this for initialization
    new void Start()
    {
        base.Start();
        Position = Position.MIDDLE;
        Bullets = maxBullets;
        Lives = 3;
    }

    void Update()
    {
        ManageInputs();
    }

    private void MoveLeft()
    {
        if (Position == Position.MIDDLE)
        {
            Position = Position.LEFT;
            SoundManager.Instance.PlaySound(moveSoundName);
            return;
        }

        if (Position == Position.RIGHT)
        {
            Position = Position.MIDDLE;
            SoundManager.Instance.PlaySound(moveSoundName);
        }
    }

    private void MoveRight()
    {
        if (Position == Position.MIDDLE)
        {
            Position = Position.RIGHT;
            SoundManager.Instance.PlaySound(moveSoundName);
            return;
        }

        if (Position == Position.LEFT)
        {
            Position = Position.MIDDLE;
            SoundManager.Instance.PlaySound(moveSoundName);
        }
    }

    private void Fire()
    {
        SoundManager.Instance.PlaySound(shootSoundName);
        Bullets -= 1;

        if (HasFired != null)
            HasFired(this);
    }

    private IEnumerator Reload()
    {
        while (Bullets < maxBullets)
        {
            Bullets += 1;
            SoundManager.Instance.PlaySound(reloadSoundName);

            if (Bullets != maxBullets)
                yield return new WaitForSeconds(0.33f);
        }
        isReloading = false;
    }

    private void ManageGraphics()
    {
        foreach (LCDSprite sprite in Sprites.Values)
        {
            sprite.Lit = false;
        }

        Sprites["Sheriff" + (char)Position].Lit = true;
    }

    private void ManageInputs()
    {
        if (Time.timeScale != 0)
        {
            if (Input.GetAxisRaw("Horizontal") == 0)
            {
                lockHorizontal = false;
            }

            if (Input.GetAxisRaw("Horizontal") < 0 && !lockHorizontal)
            {
                lockHorizontal = true;
                MoveLeft();
            }

            if (Input.GetAxisRaw("Horizontal") > 0 && !lockHorizontal)
            {
                lockHorizontal = true;
                MoveRight();
            }

            if (Input.GetButtonDown("Fire2") && !isReloading && Bullets > 0)
            {
                Fire();
            }

            if (Input.GetButtonDown("Fire1") && !isReloading)
            {
                if (Bullets == 0)
                {
                    Panic = false;
                }
                isReloading = true;
                StartCoroutine("Reload");
            }
        }
    }

    public void TakeDamage()
    {
        SoundManager.Instance.PlaySound(damagedSoundName);
        Lives -= 1;
    }

    public override void Reset()
    {
        Position = Position.MIDDLE;
        Bullets = maxBullets;
        Lives = 3;
        Score = 0;
    }
}
