using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    The PlayerGun script is used to give the Player's gun sound and mimmic a 
    sound sphere around the Player for the Zombie's to "hear" the gunshots.
 */
public class PlayerGun : MonoBehaviour
{
    public AudioClip shootSound;
    public float soundIntensity = 100f;
    public LayerMask zombieLayer;

    private AudioSource audioSource;


    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //Creates a sphere collider to mimmic a sound field for the Player's gun
    public void Fire()
    {
        audioSource.PlayOneShot(shootSound);
        Collider[] zombies = Physics.OverlapSphere(transform.position, soundIntensity, zombieLayer);
        //Debug.Log(zombies.Length);
        for(int i = 0; i < zombies.Length; i++)
        {
            zombies[i].GetComponent<Zombie>().IsAware();
            //Debug.Log("is in loop");
        }
    }
}
