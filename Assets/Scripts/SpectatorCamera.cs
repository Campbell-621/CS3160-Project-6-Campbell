using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectatorCamera : MonoBehaviour
{
    private Camera spectatorCamera;
    void Start()
    {
        spectatorCamera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spectatorCamera.enabled)
        {
            var rotation = transform.rotation.eulerAngles;
            rotation.y += 20f * Time.deltaTime;
            transform.rotation = Quaternion.Euler(rotation);
        }
    }

    public void EnableCamera()
    {
        spectatorCamera.enabled = true;
    }

    public void DisableCamera()
    {
        spectatorCamera.enabled = false;
    }
}
