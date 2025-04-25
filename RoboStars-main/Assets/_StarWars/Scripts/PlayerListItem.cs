using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using TMPro;

public class PlayerListItem : MonoBehaviour
{
    [SerializeField] private TMP_Text playerName; // Текстове поле для відображення імені гравця
    private Player player; // Об'єкт Player для цього елемента списку

    // Встановлення гравця в елемент списку
    public void SetUp(Player player)
    {
        this.player = player;
        playerName.text = this.player.NickName;
    }

    // Метод для видалення елемента, коли гравець покидає кімнату
    public void RemoveOnPlayerLeftRoom(Player otherPlayer)
    {
        if (player == otherPlayer)
        {
            Destroy(gameObject);
        }
    }

    // Метод для видалення елемента, коли поточний гравець покидає кімнату
    public void RemoveOnLeftRoom()
    {
        Destroy(gameObject);
    }
}
