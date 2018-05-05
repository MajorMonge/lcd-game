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
        }
    }

    public int maxBullets = 6;

    private int _bullets;
    public int Bullets
    {
        get
        {
            return _bullets;
        }
        set
        {
            if (value < 0)
            {
                //Warn player to reload
            }

            if (value <= maxBullets)
            {
                _bullets = value;
            }
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
