using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MeniPlayerStatisticComponent : MonoBehaviour
{
    [SerializeField] private TMP_Text countBuffPointsText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text currentEXP_Text;
    [SerializeField] private Image fillEXP_Area;

    public PlayerStatisticComponent playerStats; // Ссылка на компонент статистики

    void Start()
    {
        playerStats = GetComponent<PlayerStatisticComponent>();

        if (playerStats == null)
        {
            Debug.LogError("PlayerStatisticComponent not found on this GameObject!");
            return;
        }

        UpdateUI();

        countBuffPointsText.text = $"Update({playerStats.GetCountBuffPoints()})";
    }

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        if (playerStats == null) return;

        levelText.text = $"Level: {playerStats.PlayerLevel}";
        currentEXP_Text.text = $"{playerStats.CurrentEXP} / {playerStats.EXPtoNextLevel}";

        fillEXP_Area.fillAmount = playerStats.EXPtoNextLevel > 0
            ? (float)playerStats.CurrentEXP / playerStats.EXPtoNextLevel
            : 0f;
    }

    public void TakeCountBuffPoints()
    {
        playerStats.TakeCountyBuffPoints(); // Вызов метода из PlayerStatisticComponent
        countBuffPointsText.text = $"Update({playerStats.GetCountBuffPoints()})";
    }
}
