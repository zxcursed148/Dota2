using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using ExitGames.Client.Photon;
public class PlayerSetting : MonoBehaviourPunCallbacks
{
    private const byte GAME_IS_WIN = 0;
    private GameNetworkManager gameManager;
    [SerializeField] public int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private Slider healthBar;
    private PhotonView pv;
    [PunRPC]
    public void UpdateHealth(int value){
        health -= value;
        if(health <= 0){
            health = maxHealth;
            transform.GetComponentInChildren<PlayerController>().Respawn();
           
        }
        healthBar.value = health;
    }
   private void Awake()
    {
        pv = GetComponentInParent<PhotonView>();
        gameManager = gameObject.GetComponentInParent<GameNetworkManager>();

    }
    // Start is called before the first frame update
    private void Start()
    {
        health = maxHealth;
        healthBar.value = health;
    }
    public void TakeDamage(int value){
        pv.RPC("UpdateHealth", RpcTarget.All, value);
    }
    // Update is called once per frame
    void Update()
    {
    }
    private void OnEnable()
    {
     PhotonNetwork.NetworkingClient.EventReceived += OnNetworkEventCome;
    }

    public override void OnDisable()
    {
     PhotonNetwork.NetworkingClient.EventReceived -= OnNetworkEventCome;
    }

    private void OnNetworkEventCome(EventData obj)
    {
        if(obj.Code == GAME_IS_WIN)
        {
            if (!pv.IsMine) return;


            gameManager.OnGameWin.Invoke();
        }
    }

    private void SendWinEvent()
    {
        object[] datas = null;
        PhotonNetwork.RaiseEvent(GAME_IS_WIN, datas, RaiseEventOptions.Default, SendOptions.SendReliable);



    }

   
}