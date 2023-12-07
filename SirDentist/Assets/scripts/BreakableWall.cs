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
    private Collider myCollider;

    public GameObject art;

    void Start()
    {
        anim = GetComponent<Animator>();
        //boxColliderObj.SetActive(true);
        myCollider = GetComponent<Collider>();
        myCollider = gameObject.AddComponent<BoxCollider2D>();
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
            print("half_break");
            //anim.SetBool("wallHalf", true);
            //anim.SetBool("wallGone", false);
        }
        else if (hitNum <= 0)
        {
            anim.SetTrigger("break");
            //anim.SetBool("break", true);
            print("break");
            //anim.SetBool("wallGone", true);
            //boxColliderObj.SetActive(false);
            art.SetActive(false);
            myCollider.enabled = !myCollider.enabled;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("hit");
        if (collision.gameObject.tag == "Flail")
        {
            hitNum--;
        }
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