using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class cameraCursorMover : MonoBehaviour
{
    [SerializeField]
    float sensitivity;

    private float mouseX;

    void Update()
    {
        //horizontal movement
        mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);
    }
}
