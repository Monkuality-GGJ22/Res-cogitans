using UnityEngine;
using UnityEngine.UI;


public class LifeBehaviour : MonoBehaviour
{

    [SerializeField]
    private int maxHealth = 3;

    [SerializeField]
    private Text lifeText;

    [SerializeField]
    private RespawnManager respawnManager;

    private int currentHealth;
    private int CurrentHealth
    {
        set
        {
            currentHealth = value;
            if(lifeText!=null)
                lifeText.text = "Current Health = " + currentHealth;
        }
        get => currentHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject textGameObject;
        textGameObject = GameObject.Find("PlayerLivesText");
        if (textGameObject != null)
            lifeText = textGameObject.GetComponent<Text>();
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
