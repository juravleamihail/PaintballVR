using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    private NetworkManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<NetworkManager>();
        manager.StartMatchMaker();
        manager.matchMaker.CreateMatch(manager.matchName, manager.matchSize, true, "", "", "", 0, 0, manager.OnMatchCreate);
        //DebugText.text = "network address: " + manager.networkAddress + " , server bind address: " + manager.serverBindAddress + " , match port: " + manager.matchPort + " , network port: " + manager.networkPort;
        //manager.matchMaker.CreateMatch(manager.matchName, manager.matchSize, true, "", "", "", 0, 0, manager.OnMatchCreate);
        //var a = manager.matchMaker.ListMatches(0, 20, "", false, 0, 0, manager.OnMatchList);
        // Debug.Log(a);
    }

    // Update is called once per frame
    void Update()
    {
        //if (manager.matchHost != "")
        //    Debug.Log(manager.matches);

        //if(manager.networkAddress != null)
        //    Debug.Log(manager.networkAddress);
    }
}
