using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followTransform;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector3 initialOffset;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        initialOffset = followTransform.position - initialPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, followTransform.position - initialOffset, Time.deltaTime);
    }
}
