using UnityEngine;
using UnityEngine.UI;


public class LifeBehaviour : MonoBehaviour
{
    [SerializeField] private RespawnManager respawnManager;
    public int maxHealth = 3;
    [SerializeField] private int currentHealth;
    public int CurrentHealth
    {
        set
        {
            currentHealth = value;
        }
        get => currentHealth;
    }

    void Start()
    {
        CurrentHealth = maxHealth;
    }

    public void DamagePlayer()
    {
        CurrentHealth--;
        if(CurrentHealth <= 0)
        {
            respawnManager.RestartCheckpoint();
        }
    }

    public void HealPlayer()
    {
        if (CurrentHealth < maxHealth)
            CurrentHealth++;
    }

    public void RefillLife()
    {
        CurrentHealth = maxHealth;
    }
    
}
