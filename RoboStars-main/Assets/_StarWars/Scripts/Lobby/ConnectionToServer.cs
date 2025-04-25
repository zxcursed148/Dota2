using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class ConnectionToServer : MonoBehaviourPunCallbacks
{
    public static ConnectionToServer Instance;

    [SerializeField] private TMP_InputField inputRoomName;
    [SerializeField] private TMP_Text roomName;

    [SerializeField] private Transform transformRoomList;
    [SerializeField] private GameObject roomItemPrefab;

    [SerializeField] private GameObject playerListItem;
    [SerializeField] private Transform transformPlayerList;

    [SerializeField] private GameObject startGameButton;

    private void Awake()
    {
        // Перевірка Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        Debug.Log("Connected to Master Server");
        PhotonNetwork.NickName = "Player" + Random.Range(0, 1000);
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        WindowsManager.Layout.OpenLayout("MainMenu");
        Debug.Log("Connected to Lobby");

    }

    public void CreateNewRoom()
    {
        if (!PhotonNetwork.IsConnectedAndReady || PhotonNetwork.Server != ServerConnection.MasterServer)
        {
            Debug.LogError("Cannot create room: Not connected to Master Server or not ready.");
            return;
        }

        if (string.IsNullOrEmpty(inputRoomName.text))
        {
            Debug.LogWarning("Room name is empty!");
            return;
        }

        PhotonNetwork.CreateRoom(inputRoomName.text);
    }

    public override void OnJoinedRoom()
    {
        WindowsManager.Layout.OpenLayout("GameRoom");
        roomName.text = PhotonNetwork.CurrentRoom.Name;
        Debug.Log($"Joined room: {PhotonNetwork.CurrentRoom.Name}");

        if (PhotonNetwork.IsMasterClient) startGameButton.SetActive(true);
        else startGameButton.SetActive(false);

        roomName.text = PhotonNetwork.CurrentRoom.Name;

        UpdatePlayerList();
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        WindowsManager.Layout.OpenLayout("MainMenu");

        // Очищення списку гравців
        foreach (Transform trns in transformPlayerList)
        {
            Destroy(trns.gameObject);
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        // Очищення списку кімнат
        foreach (Transform trns in transformRoomList)
        {
            Destroy(trns.gameObject);
        }

        // Оновлення списку кімнат
        foreach (RoomInfo room in roomList)
        {
            if (!room.RemovedFromList)
            {
                Instantiate(roomItemPrefab, transformRoomList).GetComponent<RoomItem>().SetUp(room);
            }
        }
    }

    public void JoinRoom(RoomInfo info)
    {
        if (!PhotonNetwork.IsConnectedAndReady || PhotonNetwork.Server != ServerConnection.MasterServer)
        {
            Debug.LogError("Cannot join room: Not connected to Master Server or not ready.");
            return;
        }

        PhotonNetwork.JoinRoom(info.Name);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
    }

    private void UpdatePlayerList()
    {
        // Очищення списку гравців
        foreach (Transform trns in transformPlayerList)
        {
            Destroy(trns.gameObject);
        }

        // Оновлення списку гравців
        Player[] players = PhotonNetwork.PlayerList;
        foreach (Player player in players)
        {
            Instantiate(playerListItem, transformPlayerList).GetComponent<PlayerListItem>().SetUp(player);
        }
    }
    public void ConnectToRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if (PhotonNetwork.IsMasterClient) startGameButton.SetActive(true);
        else startGameButton.SetActive(false);
    }
    public void StartGameLevel (int LevelIndex)
    {
        PhotonNetwork.LoadLevel(LevelIndex);
    }
}
