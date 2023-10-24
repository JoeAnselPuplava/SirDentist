using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPlayerRotation : MonoBehaviour
{
    public Rigidbody2D player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
