using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class chainLinker : MonoBehaviour
{
    Transform rotationpos;
    public GameObject chainone;
    // Start is called before the first frame update
    void Start()
    {
        rotationpos = GameObject.FindGameObjectWithTag("rotation").transform;
        InvokeRepeating("resetpos", 0f, 15f);
    }

    // Update is called once per frame
    void Update()
    {
        //chainone.GetComponent<Rigidbody2D>().MovePosition(rotationpos.position);
        //chainone.GetComponent<HingeJoint2D>().connectedAnchor = rotationpos.position;
        //chainone.GetComponent<Rigidbody2D>().MovePosition(rotationpos.position);
        //flail.position.move = rotationpos.position - chainone.transform.position;
    }

    void resetpos(){
        chainone.GetComponent<Rigidbody2D>().MovePosition(rotationpos.position);
    }
}
