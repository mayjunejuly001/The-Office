using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Stressbar : MonoBehaviour
{
    public TMP_Text stressBarText;
    public Slider stressSlider;
    Damageable playerDamageable;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("No Player found!");
            return;
        }
        playerDamageable = player.GetComponent<Damageable>();
    }

    void Start()
    {
        UpdateStressUI(playerDamageable.Health, playerDamageable.MaxHealth);
    }

    private float CalculateStressPercentage(float currentHealth, float maxHealth)
    {
        return (1 - (currentHealth / maxHealth)) * 100; // Converts to percentage (0 - 100)
    }

    private void OnEnable()
    {
        playerDamageable.healthChanged.AddListener(OnPlayerHealthChange);
    }

    private void OnDisable()
    {
        playerDamageable.healthChanged.RemoveListener(OnPlayerHealthChange);
    }

    private void OnPlayerHealthChange(int newHealth, int maxHealth)
    {
        UpdateStressUI(newHealth, maxHealth);
    }

    private void UpdateStressUI(int currentHealth, int maxHealth)
    {
        float stressPercentage = CalculateStressPercentage(currentHealth, maxHealth);
        stressSlider.value = stressPercentage / 100; // Slider expects value between 0 and 1
        stressBarText.text = "Stress: " + Mathf.RoundToInt(stressPercentage) + " / 100";

        Debug.Log("Stress Level: " + Mathf.RoundToInt(stressPercentage) + "%");
    }
}
