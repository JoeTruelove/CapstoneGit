using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    The CameraFollow script is used by the camera to 
    follow the Player Object in a TopDown view.
 */
public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float cameraHeight = 20f;

    private void Update()
    {
        Vector3 pos = player.transform.position;
        pos.y += cameraHeight;
        pos.z -= 6;
        transform.position = pos;
    }
}
