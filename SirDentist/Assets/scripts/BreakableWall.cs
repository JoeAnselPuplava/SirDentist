using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{

    private Animator anim;
    //public GameObject ParticleVFX;
    //public AudioSource breakSFX;
    public int hitNum = 2; // how many times the object can be hit before it disappears.
    //public GameObject boxColliderObj; // a child collider that can be turned off
    private Renderer myRend;
    private Color defaultColor;
    private BoxCollider2D myCollider;
    private bool once = true;
    public GameObject art;

    void Start()
    {
        anim = GetComponent<Animator>();
        myCollider = gameObject.GetComponent<BoxCollider2D>();
        myRend = gameObject.GetComponentInChildren<Renderer>();
        defaultColor = myRend.material.color;
    }

    void Update()
    {
        if (hitNum == 2)
        {
            //anim.SetBool("wallHalf", false);
            //anim.SetBool("wallGone", false);
        }
        else if (hitNum == 1)
        {
            anim.SetTrigger("half_break");
        }
        else if (hitNum <= 0 && once)
        {
            once = false;
            anim.SetTrigger("break");
            art.SetActive(false);
            myCollider.enabled = false;

        }
    }

    public void wallDamage()
    {
        // this is the function that the player attack script would access
        if (hitNum > 0)
            //if (!breakSFX.isPlaying) { breakSFX.Play(); }
        if (hitNum == 2) { //anim.SetTrigger("break"); 
    }
        //else if (hitNum == 1) { anim.SetTrigger("cutHalf"); }
        StartCoroutine(wallHitReturn());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Flail" || collision.gameObject.tag == "Sword")
        {
            hitByWeapon();
        }
    }

    public void hitByWeapon()
    {
        hitNum--;
    }

    IEnumerator wallHitReturn()
    {
        myRend.material.color = new Color(1.0f, 1.0f, 2.5f);
        yield return new WaitForSeconds(0.5f);
        hitNum -= 1;
        myRend.material.color = defaultColor;
        //breakSFX.Stop();
    }
}