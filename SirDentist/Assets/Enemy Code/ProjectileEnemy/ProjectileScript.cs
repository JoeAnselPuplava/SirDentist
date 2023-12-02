using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
	public Transform target;

	public float speed = 15f;
	public float rotateSpeed = 230f;

	private Rigidbody2D rb;

    GameHandler gameHander;

	// Use this for initialization
	void Start () {
        target = GameObject.FindGameObjectWithTag("Player").transform;
		rb = GetComponent<Rigidbody2D>();
        StartCoroutine(TimeDecay());
        gameHander = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GameHandler>();
	}
	
	void FixedUpdate () {
		Vector2 direction = (Vector2)target.position - rb.position;

		direction.Normalize();

		float rotateAmount = Vector3.Cross(direction, transform.up).z;

		rb.angularVelocity = -rotateAmount * rotateSpeed;

		rb.velocity = transform.up * speed;
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		// Put a particle effect here
		Destroy(gameObject);
        if(other.tag == "Player"){
            gameHander.playerGetHit(7);
        }
	}

    IEnumerator TimeDecay(){
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
