using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    Camera cam;
    private void Awake()
    {
            cam = Camera.main;

    }
    void Update()
    {
        transform.LookAt(cam.transform.position);
    }
}
