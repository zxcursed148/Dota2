using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Events;

public class GameNetworkManager : MonoBehaviour
{
    public UnityEvent OnGameOver;
    public UnityEvent OnGameWin;
    [SerializeField] private GameObject allPlayerUI;
    private PhotonView pv; // Исправлено (PhotonView с большой V)

    // Start is called before the first frame update
    void Start()
    {
        if (!pv.IsMine)
        {
            allPlayerUI.SetActive(false);
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        pv = GetComponent<PhotonView>(); // Исправлено (правильный способ получить компонент)
    }

    public void OutOfBattle()
    {
        PhotonNetwork.AutomaticallySyncScene = false;
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(0);
    }
}
