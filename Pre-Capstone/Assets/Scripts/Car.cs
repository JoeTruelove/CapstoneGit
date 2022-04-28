using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    The Car script is used by the Car prefab in order to give the car a
    sphere collider to cause all Zombies within the radius to chase it.
 */
public class Car : MonoBehaviour
{
    public bool alarmOn = false;
    public GameObject thisCar;
    public float range = 30f;

    private SphereCollider sphereCollider;

    private void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }

    void Update()
    {
        if(alarmOn || Input.GetKeyDown(KeyCode.J))
        {
            AlarmTriggered();
        }

        if (range != sphereCollider.radius)
        {
            sphereCollider.radius = range;
        }
    }

    //A set method to set the car alarm to true
    public void AlarmTriggered()
    {
        //Debug.Log("Alarm is On");
        alarmOn = true;
    }
    
    //The OnTriggerStay to check if any objects where already within the collider.
    private void OnTriggerStay(Collider other)
    {
        if (alarmOn)
        {
            if (other.gameObject.CompareTag("Zombie"))
            {
                //Debug.Log("We are in");
                other.gameObject.GetComponent<Zombie>().CarAlarm(thisCar);

            }
        }
    }

    //The OnTriggerEnter to check if any objects have entered the collider.
    private void OnTriggerEnter(Collider other)
    {
        if (alarmOn)
        {
            if (other.gameObject.CompareTag("Zombie"))
            {
                //Debug.Log("We are in");
                other.gameObject.GetComponent<Zombie>().CarAlarm(thisCar);

            }
        }
    }
}
