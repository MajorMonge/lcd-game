using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheriff : LCDGameObject {

    private Position _position = Position.MIDDLE;
    public Position Position
    {
        get
        {
            return _position;
        }
        set
        {
            _position = value;
            ManageGraphics();
            
            if (PositionHasChanged != null)
            {
                PositionHasChanged(this);
            }
        }
    }

    public delegate void SheriffDelegate(Sheriff sheriff);
    public event SheriffDelegate PositionHasChanged;
    public event SheriffDelegate BulletCountHasChanged;
    public event SheriffDelegate IsInPanic;

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

    private int _bullets;
    public int Bullets
    {
        get
        {
            return _bullets;
        }
        set
        {
            value = Mathf.Clamp(value, 0, maxBullets);

            if (value != _bullets && BulletCountHasChanged != null)
            {
                BulletCountHasChanged(this);
            }

            if (value == 0)
            {
                Panic = true;
            }

            _bullets = value;
        }
    }

    public string moveSoundName;
    public string shootSoundName;

	// Use this for initialization
	new void Start () {
        base.Start();
        Position = Position.MIDDLE;
	}
	
	void Update () {
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
        //pewpewpew
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
        float axisH = 0;

        if (Input.GetButtonDown("Horizontal"))
        {
            axisH = Input.GetAxisRaw("Horizontal");
        }

        if (axisH < 0f)
        {
            MoveLeft();
        }

        if (axisH > 0f)
        {
            MoveRight();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }
}
