using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class within_range : MonoBehaviour
{
    public Transform other;
    public GameObject myButton1;
    public float dist_to=2;
    public bool showDist;
    // Start is called before the first frame update
    void Start()
    {
        showDist=false;
        
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(other.position, transform.position);
        if(showDist){
            print(dist);
        }
        if(dist<dist_to){
            //print("house within range!");
            myButton1.SetActive(true);
        }
        else{
            myButton1.SetActive(false);
        }
        
    }
}


