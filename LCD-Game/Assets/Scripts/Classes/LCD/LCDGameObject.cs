using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LCDGameObject : MonoBehaviour {

    public Dictionary<string, LCDSprite> Sprites;

	// Use this for initialization
	protected void Start () {
        Sprites = new Dictionary<string, LCDSprite>();
        foreach (LCDSprite sprite in GetComponentsInChildren<LCDSprite>())
        {
            Sprites.Add(sprite.name, sprite);
        }
	}
}
