using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    The PipeBomb script is used by the Pipebomb prefab to create
    the explosion animation and to create the collider to see if it
    hits anything within a set radius and does damage to any Zombies
 */
public class PipeBomb : MonoBehaviour
{
    public GameObject explosionEffect;
    public float delay = 8f;
    public GameObject thisBomb;

    public int damage = 10;

    public float explosionForce = 10f;
    public float radius = 15f;

    private void Start()
    {
        thisBomb = this.gameObject;
        Invoke("Explode", delay);
    }

    //The method that controls the Explosions collider
    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider near in colliders)
        {
            Rigidbody rig = near.GetComponent<Rigidbody>();

            if (rig != null)
            {
                rig.AddExplosionForce(explosionForce, transform.position, radius, 1f, ForceMode.Impulse);
                if(near.CompareTag("Zombie"))
                {
                    near.GetComponent<Zombie>().health -= damage;
                }
            }
        }

        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    //The OnTrigger for the PipeBomb to give any Zombies within the radius the information of this PipeBomb.
    private void OnTriggerEnter(Collider other)
    {
        {
            if (other.gameObject.CompareTag("Zombie"))
            {
                //Debug.Log("We are in");
                other.gameObject.GetComponent<Zombie>().PipeBomb(thisBomb.transform);

            }
        }
    }
}
