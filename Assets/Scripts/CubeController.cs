using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubeController : NetworkBehaviour
{
    public float velocity = 5f;
    public float lookSensitivity = 0.05f;
    private Vector2 movement;
    private Vector2 look;
    private Rigidbody rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
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

    [ServerRpc]
    private void RequestThrowServerRpc(int playerIndex)
    {
    }

    [ClientRpc]
    public void KillPlayerClientRPC(int playerIndex)
    {
    }
}
