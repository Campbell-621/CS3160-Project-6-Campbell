using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(NetworkManager))]
public class NetworkStarter : MonoBehaviour
{
    NetworkManager networkManager;
    private bool networkStarted = false;
    private PlayerInputActions playerInputActions;
    public InputField ipAddress;
    public InputField port;
    public GameObject connectCanvas;
    void Start()
    {
        networkManager = GetComponent<NetworkManager>();
        playerInputActions = new();
        playerInputActions.Enable();
    }

    public void Host()
    {
        if (networkStarted || ipAddress.text == "" || port.text == "" || !ushort.TryParse(port.text, out var portValue)) return;

        networkManager.GetComponent<UnityTransport>().SetConnectionData(ipAddress.text, portValue);
        networkManager.StartHost();
        networkStarted = true;
        FindAnyObjectByType<SpectatorCamera>().DisableCamera();
        connectCanvas.SetActive(false);
    }

    public void Client()
    {
        if (networkStarted || ipAddress.text == "" || port.text == "" || !ushort.TryParse(port.text, out var portValue)) return;

        networkManager.GetComponent<UnityTransport>().SetConnectionData(ipAddress.text, portValue);
        networkManager.StartClient();
        networkStarted = true;
        FindAnyObjectByType<SpectatorCamera>().DisableCamera();
        connectCanvas.SetActive(false);
    }
}
