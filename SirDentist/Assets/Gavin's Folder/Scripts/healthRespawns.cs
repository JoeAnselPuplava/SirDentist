using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthRespawns : MonoBehaviour
{
    public float respawntime = 20f;
    public GameObject currhealthup;
    public GameObject healthprefab;

    bool spawning = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currhealthup == null && !spawning){
            spawning = true;
            StartCoroutine(healthrespawn());
        }
    }

    IEnumerator healthrespawn(){
        yield return new WaitForSeconds(respawntime);
        currhealthup = Instantiate(healthprefab,transform.position,Quaternion.identity);
        spawning = false;
    }
}
