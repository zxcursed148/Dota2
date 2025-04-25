using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; // ✅ Добавлено
using TMPro; // ✅ Если используется TMP_Text

public class NetworkStatistic : MonoBehaviour
{
    [SerializeField] private TMP_Text onlineCounterText; // ✅ Добавлено

    void Start()
    {
        UpdateOnlineCounter();
    }

    void Update()
    {
        
    }

    private void UpdateOnlineCounter()
    {
        if (onlineCounterText != null)
        {
            onlineCounterText.text = $"Players online: {PhotonNetwork.CountOfPlayers}";
        }
        else
        {
            Debug.LogWarning("onlineCounterText is not assigned in the Inspector!");
        }
    }
}
