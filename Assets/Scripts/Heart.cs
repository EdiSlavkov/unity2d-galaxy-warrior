using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float healthAmount;
    [SerializeField] private float healthAmountPerLevel;
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
            healthAmount += healthAmountPerLevel * gameController.GetCurrentLevel();
        }
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(rb.velocity.x, - speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameController.RefillHealth(healthAmount);
        Destroy(gameObject);
    }
}