using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{    
    public GameObject pickupEffect;

    public float multiplier = 1.4f;
    public float duration = 4f;

    private void OnTriggerEnter(Collider other)
    {
        //do something interesting to the ball, paddle, or some other game element
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Pickup(other));
        }
    }

    IEnumerator Pickup(Collider player)
    {
        // Spawn cool effect
        Instantiate(pickupEffect, transform.position, transform.rotation);

        //Apply effect to the player
        player.transform.localScale /= multiplier;

        // Wait x amount of seconds
        yield return new WaitForSeconds(duration);

        // Disable graphics
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        // Reverse effects on player
        player.transform.localScale *= multiplier;

        //Remove power up object
        Destroy(gameObject);
    }


}
