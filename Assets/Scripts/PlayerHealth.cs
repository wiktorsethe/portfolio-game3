using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Image hpBar;
    [SerializeField] private PlayerStats playerStats;
    private void Start()
    {
        SetMaxHealth();
    }
    public void SetMaxHealth()
    {
        hpBar.fillAmount = 1f;
        playerStats.playerCurrentHealth = playerStats.playerMaxHealth;
    }
    public void Damage(float damage)
    {
        playerStats.playerCurrentHealth -= damage;
        DOTween.To(() => hpBar.fillAmount, x => hpBar.fillAmount = x, playerStats.playerCurrentHealth / playerStats.playerMaxHealth, 1.5f)
            .SetDelay(0.8f);
    }
}
