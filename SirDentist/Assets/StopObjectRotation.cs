using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopObjectRotation : MonoBehaviour
{

    void Start()
    {
        transform.rotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        //player.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
