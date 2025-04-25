using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO; // ✅ Добавлено

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            gameObject.AddComponent<PhotonView>();
            Instance = this;  
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        // Проверяем, что находимся в бою
        if (scene.buildIndex >= 1) // Если сцена больше 1, значит это игровой уровень
        {
            PhotonNetwork.Instantiate(Path.Combine("GameManager"), Vector3.zero, Quaternion.identity);
        }
    }
}
