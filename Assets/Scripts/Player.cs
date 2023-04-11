using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 minBoundaries;
    private Vector2 maxBoundaries;
    private Vector2 initialPosition;
    private float marginBottom = 1;
    private const string EnemyTag = "Enemy";
    private const string EnemyBulletTag = "EnemyBullet";
    private const string CollectablesTag = "Collectables";
    private GameController gameController;
    private StatsController statsController;

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
    }

    private void Start()
    {
        statsController = GetComponent<StatsController>();
        initialPosition = transform.position;
        minBoundaries = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        maxBoundaries = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
    }

    private void Update()
    {
        Vector2 totalSpeed = new Vector2(joystick.Horizontal, joystick.Vertical) * moveSpeed * Time.deltaTime;
        Vector2 newPosition = new Vector2();
        newPosition.x = Mathf.Clamp(transform.position.x + totalSpeed.x, minBoundaries.x + transform.localScale.x / 2, maxBoundaries.x - transform.localScale.x / 2);
        newPosition.y = Mathf.Clamp(transform.position.y + totalSpeed.y, minBoundaries.y + transform.localScale.y / 2 + marginBottom, maxBoundaries.y - transform.localScale.y / 2);
        transform.position = newPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(CollectablesTag))
        {
            return;
        }
        ScoreDecrease scoreDecrease = collision.GetComponent<ScoreDecrease>();
        float damage = 0;
        if (collision.CompareTag(EnemyTag))
        {
            damage = collision.GetComponent<StatsController>().GetCollisionDamage();
            transform.position = initialPosition;
        }
        if (collision.CompareTag(EnemyBulletTag))
        {
            damage = collision.GetComponent<Bullet>().GetDamage();
        }
        statsController.TakeDamage(damage);
        gameController.HandleHit(scoreDecrease.PlayerScoreDecrease());
        if (statsController.GetHealth() <= 0)
        {
            gameController.HandlePlayerDeath();
        }
    }
}