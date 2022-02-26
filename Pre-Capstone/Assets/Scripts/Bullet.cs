using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 7f;
    public float maxDistance = 0;
    public int damage = 1;

    private GameObject triggeringEnemy;

    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * bulletSpeed);
        maxDistance += 1 * Time.deltaTime;

        if(maxDistance >= 5)
        {
            Destroy(this.gameObject);
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Got Triggered " + other.gameObject.tag);

        if (other.tag == "Zombie")
        {
            triggeringEnemy = other.gameObject;
            triggeringEnemy.GetComponent<Zombie>().health -= damage;
            Destroy(this.gameObject);
        }
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Got Triggered " + collision.gameObject.tag);

        if (collision.gameObject.tag == "Zombie")
        {
            triggeringEnemy = collision.gameObject;
            triggeringEnemy.GetComponent<Zombie>().health -= damage;
        }
    }*/
}
