using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    private PhotonView pnView;
    private GameObject controller;

    private void Awake()
    {
        pnView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (pnView.IsMine) // Check if this object belongs to the local player
        {
            CreateController();
        }
    }

    private void CreateController()
    {
        Transform spawnPoint = SpawnManager.instance?.GetSpawnPoint();
        if (spawnPoint == null)
        {
            Debug.LogError("Spawn point is null!");
            return;
        }
        Debug.Log("Creating player controller at: " + spawnPoint.position);
        controller = PhotonNetwork.Instantiate("PlayerController", spawnPoint.position, spawnPoint.rotation, 0, new object[] { pnView.ViewID });
    }


    public void Die()
    {
        PhotonNetwork.Destroy(controller); // Destroy the current controller
        CreateController(); // Respawn the player by creating a new controller
    }
}
