using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    The GrenadeThrow script is used by the Player to instantiate a
    Pipebomb prefab to throw foward from the Player.
 */
public class GrenadeThrow : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject grenade;

    float range = 10f;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            Launch();
        }
    }

    //This method is used to Instantiate the bomb in front of the Player and to send it forward.
    private void Launch()
    {
        GameObject grenadeInstance = Instantiate(grenade, spawnPoint.position, spawnPoint.rotation);
        grenadeInstance.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * range, ForceMode.Impulse);
    }
}
