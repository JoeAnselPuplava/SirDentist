using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityDetect : MonoBehaviour
{
   public float detectionRadius = 5f; // Adjust the radius as needed
    public LayerMask playerLayer; // Set the layer for the player in the Inspector

    public DialogueTrigger script; // Drag and drop the GameObject with the script you want to call

    bool detected = false; 

    private void Update()
    {
        // Check for player proximity
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, playerLayer);

        // If player is in proximity, call the script
        if (colliders.Length > 0 && !detected)
        {
            detected = true;
            // Check if the scriptToCall GameObject has the script you want to call
            if (script != null)
            {

                // Call the method in YourScriptName
                if (script != null)
                {
                    script.TriggerDialogue();
                }
                else
                {
                    Debug.LogError("Script not found on the specified GameObject.");
                }
            }
            else
            {
                Debug.LogError("ScriptToCall GameObject not assigned.");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a wire sphere in the editor to visualize the detection radius
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
