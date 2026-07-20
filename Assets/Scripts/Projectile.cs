using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Projectile : NetworkBehaviour
{
    public float force = 20f;
    public NetworkVariable<int> playerIndex = new();
    private void Start()
    {
        GetComponent<Rigidbody>().AddForce(force * transform.forward, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
    }
}
