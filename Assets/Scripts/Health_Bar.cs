using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health_Bar : MonoBehaviour
{
    public Image healthBar;
    private float currentHealth;
    private float maxHealth = 100f;
    private Player _player;
    private void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }
    private void Update()
    {
        currentHealth = _player.health;
        healthBar.fillAmount = _player.health / maxHealth;
    }
}
