using UnityEngine;
using Photon.Pun;
using TMPro;

public class SetPlayerName : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField nickNameField;

    public override void OnConnectedToMaster()
    {
        LoadNickName();
    }

    private void LoadNickName()
    {
        // ������������ ���� � PlayerPrefs ��� ��������� �����������
        string playerName = PlayerPrefs.GetString("SaveNickName", string.Empty);
        if (string.IsNullOrEmpty(playerName))
        {
            playerName = "Player" + Random.Range(0, 1000);
        }

        PhotonNetwork.NickName = playerName;
        if (nickNameField != null)
        {
            nickNameField.text = playerName;
        }
    }

    public void ChangeName()
    {
        // �������� ��������� ���� �� ��������
        if (string.IsNullOrEmpty(nickNameField.text))
        {
            Debug.LogWarning("Name cannot be empty!");
            return;
        }

        PlayerPrefs.SetString("SaveNickName", nickNameField.text);
        PhotonNetwork.NickName = nickNameField.text;
        Debug.Log($"Nickname changed to: {nickNameField.text}");
    }
}
