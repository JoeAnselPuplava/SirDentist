using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2DLERP : MonoBehaviour
{
    public GameObject target;
    public float camSpeed = 4.0f;

    public float up = 4f;

    void Start(){
        target = GameObject.FindWithTag("Player");
    }

    void FixedUpdate () {
        Vector2 pos = Vector2.Lerp ((Vector2)transform.position, (Vector2)target.transform.position, camSpeed * Time.fixedDeltaTime);
        transform.position = new Vector3 (pos.x, pos.y+up, transform.position.z);
    }
}
