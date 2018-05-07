using UnityEngine;
using TMPro;

public class LCDText : MonoBehaviour {

    public TextMeshPro Text { get; set; }

    private bool lit;
    public bool Lit
    {
        get
        {
            return lit;
        }
        set
        {
            Color c = Text.color;
            c.a = value ? 1f : 0.05f;
            Text.color = c;
            lit = value;
        }
    }

    public string defaultText;
    public bool startsLit;
    
	protected void Start () {
        Text = GetComponent<TextMeshPro>();

		if (!string.IsNullOrEmpty(defaultText))
        {
            Text.text = defaultText;
        }

        Lit = startsLit;
	}
	
}
