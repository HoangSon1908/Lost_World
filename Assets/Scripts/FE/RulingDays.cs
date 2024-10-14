using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class RulingDays : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rulingDayText;
    [SerializeField] private TextMeshProUGUI currentYearText;
    [SerializeField] private TextMeshProUGUI top1RulingDayText;
    [SerializeField] private TextMeshProUGUI top2RulingDayText;
    [SerializeField] private TextMeshProUGUI top3RulingDayText;

    public static RulingDays instance;

    private int previousRulingDays = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    public void GameOver() 
    {
        CheckAndUpdateTopRulingDays();
        LoadTopRulingDays();
    }

    void CheckAndUpdateTopRulingDays() {
        int currentRulingDays = GameManager.Instance.rulingDays;
        GameManager.Instance.topRulingDays.Add(currentRulingDays);
        GameManager.Instance.topRulingDays.Sort((a, b) => b.CompareTo(a));

        if (GameManager.Instance.topRulingDays.Count > 3) 
        {
            GameManager.Instance.topRulingDays.RemoveRange(3, GameManager.Instance.topRulingDays.Count - 3);
        }

        SaveTopRulingDays();
    }

    void SaveTopRulingDays() 
    {
        for (int i = 0; i < GameManager.Instance.topRulingDays.Count; i++)
        {
            PlayerPrefs.SetInt($"TopRulingDay{i+1}", GameManager.Instance.topRulingDays[i]);
        }
        PlayerPrefs.Save();
    }

    void LoadTopRulingDays() 
    {
        GameManager.Instance.topRulingDays.Clear();

        for (int i = 1; i <= 3; i++)
        {
            GameManager.Instance.topRulingDays.Add(PlayerPrefs.GetInt($"TopRulingDay{i}"));
        }

        UpdateTopRulingDayText();
    }

    public void UpdateTopRulingDayText() 
    {
        if (top1RulingDayText != null) 
        {
            top1RulingDayText.text = GameManager.Instance.topRulingDays.Count > 0 
            ? $" Cầm quyền trong vòng\n {GameManager.Instance.topRulingDays[0]} ngày\n" : "N/A";
        }

        if (top2RulingDayText != null) 
        {
            top1RulingDayText.text = GameManager.Instance.topRulingDays.Count > 1 
            ? $" Cầm quyền trong vòng\n {GameManager.Instance.topRulingDays[1]} ngày\n" : "N/A";
        }

        if (top3RulingDayText != null)
        {
            top1RulingDayText.text = GameManager.Instance.topRulingDays.Count > 2 
            ? $" Cầm quyền trong vòng\n {GameManager.Instance.topRulingDays[2]} ngày\n" : "N/A";
        }
    }

    public void UpdateYearsAndDaysUI(int targetDays)
    {
        int currentYears =GameManager.Instance.currentYears;
        int rulingYears = GameManager.Instance.rulingYears;

        DOTween.To(() => previousRulingDays, x => previousRulingDays = x, targetDays, 1f)
            .OnUpdate(() =>
            {
                // Update year text
                currentYearText.text = $"Năm {currentYears+rulingYears}";

                // Calculate the ruling years and display ruling days incrementally
                if (previousRulingDays > 365)
                    rulingDayText.text = $"{previousRulingDays / 365} năm và {previousRulingDays%365} ngày";
                else
                    rulingDayText.text = $"{previousRulingDays} ngày";
            })
            .OnComplete(() =>
            {
                previousRulingDays = targetDays; // Finalize the value
            });
    }
}
