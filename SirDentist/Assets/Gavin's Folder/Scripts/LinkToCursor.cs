using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkToCursor : MonoBehaviour
{
    Vector2 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
    }
}
