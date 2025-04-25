using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using TMPro;

public class PlayerListItem : MonoBehaviour
{
    [SerializeField] private TMP_Text playerName; // �������� ���� ��� ����������� ���� ������
    private Player player; // ��'��� Player ��� ����� �������� ������

    // ������������ ������ � ������� ������
    public void SetUp(Player player)
    {
        this.player = player;
        playerName.text = this.player.NickName;
    }

    // ����� ��� ��������� ��������, ���� ������� ������ ������
    public void RemoveOnPlayerLeftRoom(Player otherPlayer)
    {
        if (player == otherPlayer)
        {
            Destroy(gameObject);
        }
    }

    // ����� ��� ��������� ��������, ���� �������� ������� ������ ������
    public void RemoveOnLeftRoom()
    {
        Destroy(gameObject);
    }
}
