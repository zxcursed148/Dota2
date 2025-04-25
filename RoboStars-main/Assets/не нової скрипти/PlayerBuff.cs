using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerBuff : MonoBehaviourPunCallbacks
{
    [SerializeField] protected int addHealt;
    protected int currentCountAddHealt;

    void Start()
    {
        LoadBuffs();
    }

    void Update()
    {
        
    }

    public int GetCurrentCountAddHealt()
    {
        return currentCountAddHealt;
    }

    protected void LoadBuffs()
    {
        currentCountAddHealt = PlayerPrefs.GetInt("CurrentCountAddHealt");
    }

    protected void SaveBuffs()
    {
        PlayerPrefs.SetInt("CurrentCountAddHealt", currentCountAddHealt);
    }
}
