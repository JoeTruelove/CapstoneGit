using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    The Bullet Script is used by the Bullet prefab to store it's variables and
    to determine if the bullet has hit something or if it should destroy itself.
 */
public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 7f;
    public float maxDistance = 0;
    public int damage = 1;

    private GameObject triggeringEnemy;

    private void Update()
    {
        //The bullets movement
        transform.Translate(Vector3.forward * Time.deltaTime * bulletSpeed);
        maxDistance += 1 * Time.deltaTime;

        if(maxDistance >= 5)
        {
            Destroy(this.gameObject);
        }
    }

    //The OnTrigger for the bullet to see if it collides with something.
    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Got Triggered " + other.gameObject.tag);

        if (other.tag == "Zombie")
        {
            triggeringEnemy = other.gameObject;
            triggeringEnemy.GetComponent<Zombie>().health -= damage;
            Destroy(this.gameObject);
        }
    }

}
