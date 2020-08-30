using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon;

public class PlayerGameNetwork : PunBehaviour
{
    public static PlayerGameNetwork Instance;
    public string PlayerName { get; private set; }
    private PhotonView PhotonView;
    private int PlayersInGame = 0;
    private ExitGames.Client.Photon.Hashtable m_playerCustomProperties = new ExitGames.Client.Photon.Hashtable();
    private Coroutine m_pingCoroutine;

    // Use this for initialization
    private void Awake()
    {
        Instance = this;
        PhotonView = GetComponent<PhotonView>();

        PlayerName = "Distul#" + Random.Range(1000, 9999);

        PhotonNetwork.sendRate = 60;
        PhotonNetwork.sendRateOnSerialize = 30;

        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "PaintBallArenaDemo")
        {
            if (PhotonNetwork.isMasterClient)
                MasterLoadedGame();
            else
                NonMasterLoadedGame();
        }
    }

    private void MasterLoadedGame()
    {
        PhotonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient, PhotonNetwork.player);
        PhotonView.RPC("RPC_LoadGameOthers", PhotonTargets.Others);
    }

    private void NonMasterLoadedGame()
    {
        PhotonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient, PhotonNetwork.player);
    }

    [PunRPC]
    private void RPC_LoadGameOthers()
    {
        PhotonNetwork.LoadLevel(1);
    }

    [PunRPC]
    private void RPC_LoadedGameScene(PhotonPlayer photonPlayer)
    {
        PlayersInGame++;
        if (PlayersInGame == PhotonNetwork.playerList.Length)
        {
            print("All players are in the game scene.");
            PhotonView.RPC("RPC_CreatePlayer", PhotonTargets.All);
        }
    }

    public void NewHealth(PhotonPlayer photonPlayer, int health)
    {
        PhotonView.RPC("RPC_NewHealth", photonPlayer, health);
    }

    [PunRPC]
    private void RPC_CreatePlayer()
    {
        GameObject obj = PhotonNetwork.Instantiate("Player", new Vector3(-3,7, Random.Range(1, 40)), Quaternion.identity, 0);
    }

    private IEnumerator C_SetPing()
    {
        while (PhotonNetwork.connected)
        {
            m_playerCustomProperties["Ping"] = PhotonNetwork.GetPing();
            PhotonNetwork.player.SetCustomProperties(m_playerCustomProperties);

            yield return new WaitForSeconds(5f);
        }

        yield break;
    }

    //public override void OnJoinedRoom()
    //{
    //    PhotonPlayer[] photonPlayers = PhotonNetwork.playerList;
    //    for (int i = 0; i < photonPlayers.Length; i++)
    //    {
    //        PlayerJoinedRoom(photonPlayers[i]);
    //    }

   //    PhotonNetwork.LoadLevel(1);
   // }

    public override void OnConnectedToMaster()
    {
        if (m_pingCoroutine != null)
            StopCoroutine(m_pingCoroutine);
        m_pingCoroutine = StartCoroutine(C_SetPing());
    }
}
