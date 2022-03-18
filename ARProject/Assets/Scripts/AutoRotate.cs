using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    public float speed = -5f;

    // Update is called once per frame
    void Update()
    {
        // Auto Rotate the UI in Z axis
        transform.Rotate(Vector3.forward * speed);
    }
}
