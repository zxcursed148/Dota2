using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatisticComponent : MonoBehaviour
{
    [SerializeField] private TMP_Text countBuffPointsText;
    
    protected int playerLevel;
    protected int currentEXP;
    protected int expToNextLevel;
    protected const int START_EXP_VALUE = 500;

    protected int countBuffPoints;

    public int PlayerLevel => playerLevel;
    public int CurrentEXP => currentEXP;
    public int EXPtoNextLevel => expToNextLevel;

    void Start()
    {
        countBuffPointsText.text = $"Update({countBuffPoints})";
    }

    void Update()
    {
        // Здесь может быть логика обновления статистики игрока
    }

    public virtual void Awake()
    {
        playerLevel = PlayerPrefs.GetInt("PlayerLevel", 1);
        currentEXP = PlayerPrefs.GetInt("CurrentEXP", 0);
        UpdateEXPtoNextLevel();
        countBuffPoints = PlayerPrefs.GetInt("CountBuffPoints", 0);
    }

    protected void UpdateEXPtoNextLevel()
    {
        expToNextLevel = START_EXP_VALUE * playerLevel;
    }

    public void SavePlayerStats()
    {
        PlayerPrefs.SetInt("PlayerLevel", playerLevel);
        PlayerPrefs.SetInt("CurrentEXP", currentEXP);
        PlayerPrefs.Save();
    }

    public void AddEXP(int amount)
    {
        currentEXP += amount;
        if (currentEXP >= expToNextLevel)
        {
            LevelUp();
        }
        SavePlayerStats();
    }

    private void LevelUp()
    {
        playerLevel++;
        currentEXP -= expToNextLevel;
        UpdateEXPtoNextLevel();
        SavePlayerStats();
    }

    protected void SaveData()
    {
        PlayerPrefs.SetInt("CountBuffPoints", countBuffPoints);
    }

    public int GetCountBuffPoints()
    {
        return countBuffPoints;
    }

    public virtual void TakeCountyBuffPoints()
    {
        countBuffPoints--;
        SaveData();
    }
}
