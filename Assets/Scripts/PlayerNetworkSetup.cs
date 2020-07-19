using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerNetworkSetup : Photon.PunBehaviour
{
    public Camera PlayerCamera;
    public Player playerScript;
    private PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        if (photonView.isMine)
        {
            PlayerCamera.enabled = true;
            playerScript.enabled = true;
        }
    }
}
