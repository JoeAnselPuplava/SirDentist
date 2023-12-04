using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerBetterFall : MonoBehaviour {

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    public float downforceMulti = 1f;
    private PlayerJump jumpScript;
    Rigidbody2D rb;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        jumpScript = gameObject.GetComponent<PlayerJump>();
    }

    void Update() {
        //Debug.Log(rb.velocity);
        if (!(Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) || rb.velocity.y < 35) {
            rb.velocity += Vector2.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } else if (rb.velocity.y > 0 && !Input.GetButton("Jump")) {
            //rb.velocity += Vector2.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if (rb.velocity.y < 17 && (!jumpScript.canJump || !jumpScript.IsGrounded()))
        {
            //rb.velocity += Vector2.up * Physics.gravity.y * (lowJumpMultiplier + 1) * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)){
            rb.velocity += Vector2.down * downforceMulti * Time.deltaTime;
        }
    }
}