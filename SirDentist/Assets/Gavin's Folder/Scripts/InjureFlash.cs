using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjureFlash : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer sprite;

    public void injury(){
        StartCoroutine(flash());
    }

    IEnumerator flash(){
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }
}
