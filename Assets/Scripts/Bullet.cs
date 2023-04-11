using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private float damagePerLevel;
    [SerializeField] private bool shouldGoDown;
    [SerializeField] private bool isPlayerBullet;
    [SerializeField] AnimationClip explosionAnimation;
    private Rigidbody2D rb;
    private Animator animator;
    private const string BulletAnimationTrigger = "play";
    private GameController gameController;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(rb.velocity.x, shouldGoDown ? -speed : speed);
        if (gameController.ShouldUpgrade())
        {
            damage += damagePerLevel * gameController.GetCurrentLevel();
        }
        if (isPlayerBullet && gameController.ShouldBuff())
        {
            damage += gameController.GetBuffAmount();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        rb.velocity = new Vector2(0f, 0f);
        animator.SetTrigger(BulletAnimationTrigger);
        Destroy(gameObject, explosionAnimation.length);
    }

    public float GetDamage()
    {
        return damage;
    }
}