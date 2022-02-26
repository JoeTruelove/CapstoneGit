using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public void Update()
    {
        /*if(Input.GetMouseButtonDown(0))
        {
            Fire();
        }*/
    }

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
