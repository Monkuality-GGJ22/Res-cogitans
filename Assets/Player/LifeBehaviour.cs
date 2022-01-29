using UnityEngine;
using UnityEngine.UI;


public class LifeBehaviour : MonoBehaviour
{
    [SerializeField] private RespawnManager respawnManager;
    public int maxHealth = 3;
    [SerializeField] private int currentHealth;
    public bool deadOnce;
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
        deadOnce = false;
    }

    public void DamagePlayer()
    {
        CurrentHealth--;
        if(CurrentHealth <= 0)
        {
            respawnManager.RestartCheckpoint();
            deadOnce = true;
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
