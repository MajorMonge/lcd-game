using UnityEngine;

public class LCDSprite : MonoBehaviour {

    public SpriteRenderer Sprite { get; set; }

    private bool lit;
    public bool Lit
    {
        get
        {
            return lit;
        }
        set
        {
            Color c = Sprite.color;
            c.a = value ? 1f : 0.05f;
            Sprite.color = c;
            lit = value;
        }
    }
    
	void Start () {
        Sprite = GetComponent<SpriteRenderer>();
        Lit = false;
	}
}
