using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    The ResetPosition Script is a fix script to reset the local
    position of an object to 0,0,0
 */
public class ResetPosition : MonoBehaviour
{
    void Update()
    {
        transform.localPosition = new Vector3(0, 0, 0);
    }
}
