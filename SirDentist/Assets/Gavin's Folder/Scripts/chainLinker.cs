using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class chainLinker : MonoBehaviour
{
    Transform rotationpos;
    public GameObject chainone;

    private bool delay = true;
    // Start is called before the first frame update
    void Start()
    {
        rotationpos = GameObject.FindGameObjectWithTag("rotation").transform;
        InvokeRepeating("resetpos", 0f, 15f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(rotationpos.position,chainone.transform.position) > 2f && delay){
            Debug.Log("Stretch");
            StartCoroutine(pause());
        }

        if(!delay){
            chainone.GetComponent<Rigidbody2D>().MovePosition(rotationpos.position);
        }
        //chainone.GetComponent<Rigidbody2D>().MovePosition(rotationpos.position);
        //chainone.GetComponent<HingeJoint2D>().connectedAnchor = rotationpos.position;
        //chainone.GetComponent<Rigidbody2D>().MovePosition(rotationpos.position);
        //flail.position.move = rotationpos.position - chainone.transform.position;
    }

    IEnumerator pause(){
        delay = false;
        chainone.GetComponent<HingeJoint2D>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        delay = true;
        chainone.GetComponent<HingeJoint2D>().enabled = true;
    }
}
