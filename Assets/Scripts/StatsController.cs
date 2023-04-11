using UnityEngine;
using UnityEngine.UI;

public class StatsController : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float healthPerLevel;
    [SerializeField] private float collisionDamage;
    [SerializeField] private float collisionDamagePerLevel;
    [SerializeField] private Slider healthBar;
    private AudioPlayer audioPlayer;
    private GameController gameController;
    private float initialHealth;

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        gameController = FindObjectOfType<GameController>();
    }

    private void Start()
    {
        if (gameController.ShouldUpgrade())
        {
            UpgradeEnemyStatsForLevel(gameController.GetCurrentLevel());

        }
        initialHealth = health;
        healthBar.maxValue = health;
        healthBar.value = health;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.value = health;
        audioPlayer.PlayDamageClip();
    }

    public float GetHealth()
    {
        return health;
    }

    public void AddHealth(float additionalHealth)
    {
        if (health + additionalHealth <= initialHealth)
        {
            health += additionalHealth;
        }
        else
        {
            health = initialHealth;
        }
        healthBar.value = health;
    }

    public void SetHealth(float amount)
    {
        health = amount;
        healthBar.value = amount;
    }

    public float GetCollisionDamage()
    {
        return collisionDamage;
    }

    private void UpgradeEnemyStatsForLevel(float currentGameLevel)
    {
        health += healthPerLevel * currentGameLevel;
        healthBar.maxValue = health;
        healthBar.value = health;
        collisionDamage += collisionDamagePerLevel * currentGameLevel;
    }

    public void UpgradePlayerStatsForLevel()
    {
        initialHealth += healthPerLevel;
        healthBar.maxValue = initialHealth;
        collisionDamage += collisionDamagePerLevel;
    }

    public float GetInitialHealth()
    {
        return initialHealth;
    }
}