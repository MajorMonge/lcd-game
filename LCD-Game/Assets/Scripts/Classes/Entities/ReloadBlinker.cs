using System.Collections;
using UnityEngine;

public class ReloadBlinker : LCDText {

    private IEnumerator Blink()
    {
        while (true)
        {
            Lit = true;
            yield return new WaitForSeconds(0.33f);
            Lit = false;
            yield return new WaitForSeconds(0.33f);
        }
    }

    private void CheckBullets(Sheriff sheriff)
    {
        StopAllCoroutines();

        if (sheriff.Bullets == 0)
        {
            StartCoroutine("Blink");
        }
    }

	new void Start () {
        base.Start();

        FindObjectOfType<Sheriff>().BulletCountHasChanged += CheckBullets;
	}
}
