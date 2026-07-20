using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubeController : NetworkBehaviour
{
    public GameObject projectile;
    public float velocity = 5f;
    public float lookSensitivity = 0.05f;
    private Vector2 movement;
    private Vector2 look;
    private Rigidbody rigidBody;

    private void Start()
    {
        // Protect from seeing other cameras
        var camera = GetComponentInChildren<Camera>();
        camera.enabled = IsOwner;
        
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Protect from host control
        if( !IsOwner) return;
        
        MoveAndRotate(movement, look.x);
    }


    public void OnMovement(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        look = value.Get<Vector2>();
    }

    private void MoveAndRotate(Vector2 movementDelta, float rotationDelta)
    {
        var position = transform.rotation * (velocity * new Vector3(movementDelta.x, 0f, movementDelta.y)) + transform.position;
        var initialY = transform.position.y;
        position.y = initialY;
        rigidBody.MovePosition(position);

        var rotation = transform.rotation.eulerAngles;
        rotation.y += lookSensitivity * rotationDelta;
        rigidBody.MoveRotation(Quaternion.Euler(rotation));
    }

    public void OnThrow(InputValue value)
    {
        // Must be owner to throw
        // Get player Index
        // Call RequestThrowServerRpc()
        
        if( !IsOwner) return;
        var playerIndex = GetCOmponent<PlayerInput>().playerIndex;
        RequestThrowSeverRpc(playerIndex);
    }
    
    // Only runs on server
    [ServerRpc]    // <--- Attribute
    private void RequestThrowServerRpc(int playerIndex)
    {
        // Instantiate Projectile in front of player position
        // Get projectiles Network Object
        // Spawn network object for everyone
        // Set player Index to who "Threw" 
        
        var instantiatedProjectile = Instantiate(projectile, transform.position + transform.forward, transform.rotation);
        var networkObject = instantiatedProjectile.GetComponent<NetworkObject>();
        networkObject.Spawn();

        var networkProjectile = instantiatedProjectile.GetComponent<Projectile>();
        networkProjectile.playerIndex.value = playerIndex;
    }

    // Only runs on client
    [ClientRpc]
    public void KillPlayerClientRPC(int playerIndex)
    {
        
    }
}
