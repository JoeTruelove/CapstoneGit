using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float cameraHeight = 20f;

    private void Update()
    {
        Vector3 pos = player.transform.position;
        pos.y += cameraHeight;
        transform.position = pos;
    }
}
