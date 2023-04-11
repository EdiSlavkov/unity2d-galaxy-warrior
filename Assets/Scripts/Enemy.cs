using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private bool isBoss;
    [SerializeField] private float score;
    [SerializeField] private float scorePerLevel;
    private StatsController statsController;
    private CollectablesDrop collectablesDrop;
    private GameController gameController;
    private const string PlayerTag = "Player";
    private const string PlayerBullet = "PlayerBullet";

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        statsController = GetComponent<StatsController>();
        collectablesDrop = GetComponent<CollectablesDrop>();
        if (gameController.ShouldUpgrade())
        {
            score += scorePerLevel * gameController.GetCurrentLevel();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        float damage = 0;
        if (collision.CompareTag(PlayerTag))
        {
            damage = collision.GetComponent<StatsController>().GetCollisionDamage();
        }
        if (collision.CompareTag(PlayerBullet))
        {
            damage = collision.GetComponent<Bullet>().GetDamage();
        }
        statsController.TakeDamage(damage);
        if (statsController.GetHealth() <= 0)
        {
            collectablesDrop.DropCollectables();
            if (isBoss)
            {
                gameController.StartNextLevel();
            }
            gameController.AddScoreToPlayer(score);
            Destroy(gameObject);
        }
    }
}