using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EggProjectileSpawner : MonoBehaviour
{
    public GameObject EggPrefab;
    public Animator animator;
    public float spawnDist = 1f;
    private bool runAway = false;

    // Start is called before the first frame update
    void Start()
    {
        runAway = gameObject.GetComponent<ProjectileEnemyMovement>().shouldRun;
        StartCoroutine(timeDelay());
    }

    // Update is called once per frame
    void Update()
    {
        runAway = gameObject.GetComponent<ProjectileEnemyMovement>().shouldRun;
    }

    void FireShot(){
        Instantiate(EggPrefab, new Vector3(transform.position.x - spawnDist, transform.position.y, transform.position.z), Quaternion.identity);
        Instantiate(EggPrefab, new Vector3(transform.position.x + spawnDist, transform.position.y, transform.position.z), Quaternion.identity);
    }

    IEnumerator timeDelay(){
        while(true){

            yield return new WaitForSeconds(Random.Range(2f, 5f));
            if (!runAway)
            {
                StartCoroutine(fireEnemies());
            }
            else
            {
                animator.SetBool("FireShot", false);
            }

        }
    }
    IEnumerator fireEnemies()
    {
        animator.SetBool("FireShot", true);
        FireShot();
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("FireShot", false);
    }
}

