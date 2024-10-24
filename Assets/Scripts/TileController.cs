using System;
using System.Collections;
using UnityEngine;

public class TileController : MonoBehaviour
{
    private Collider tileCollider; // Reference to Collider
    private Rigidbody rb; // Reference to Rigidbody
    private bool canFall = false; // Check if this tile can fall

    void Start()
    {
        tileCollider = GetComponent<Collider>(); // Get the Collider component
        if(canFall){
            tileCollider.isTrigger = true;
        }

        // Check if the Rigidbody already exists, otherwise add it
        rb = GetComponent<Rigidbody>() ?? gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true; // Initially set to kinematic to prevent falling

    }

    public void SetFallThrough()
    {
        canFall = true; // Allow this tile to fall through
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canFall && other.CompareTag("Player") )
        {
            Debug.Log("Player stepped on the tile!"); // Debug output
            StartCoroutine(FallThrough());
        }
    }

    private IEnumerator FallThrough()
    {
        // Disable the collider to allow the player to fall through
        tileCollider.enabled = false;

        // Wait a moment to allow the player to fall through
        yield return new WaitForSeconds(0.5f);

        // Set Rigidbody to non-kinematic to let it fall
        rb.isKinematic = false; // Disable kinematic to allow falling
        rb.useGravity = true; // Ensure gravity is applied

        // Optionally, you might want to add some initial downward force
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

        // Wait for 2 seconds before destroying the tile
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}