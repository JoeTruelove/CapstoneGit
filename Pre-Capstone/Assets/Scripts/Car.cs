using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void AlarmTriggered()
    {
        //Debug.Log("Alarm is On");
        alarmOn = true;
    }
    
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
