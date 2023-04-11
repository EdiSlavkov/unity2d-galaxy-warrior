using UnityEngine;

public class DamageBoost : MonoBehaviour
{
    [SerializeField] private float damageBoost;
    [SerializeField] private float damageBoostPerLevel;
    [SerializeField] private float duration;
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private GameController gameController;

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
    }

    private void Start()
    {
        if (gameController.ShouldUpgrade())
        {
            damageBoost += damageBoostPerLevel * gameController.GetCurrentLevel();
        }
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(rb.velocity.x, -speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameController.BoostDamage(damageBoost, duration);
        Destroy(gameObject);
    }
}