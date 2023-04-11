using System.Collections;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float damagePerLevel;
    [SerializeField] private AudioClip laserClip;
    [SerializeField] [Range(0, 1)] private float volume;
    private LineRenderer lineRenderer;
    private RaycastHit2D hit;
    private float size = 50;
    private bool shouldShoot = true;
    private AudioSource audioSource;
    private GameController gameController;
    private ScoreDecrease scoreDecrease;
    private const string PlayerLayerMask = "Player";

    private void Awake()
    {
        scoreDecrease = GetComponent<ScoreDecrease>();
        audioSource = GetComponent<AudioSource>();
        lineRenderer = GetComponent<LineRenderer>();
        gameController = FindObjectOfType<GameController>();
    }

    private void Start()
    {
        if (gameController.ShouldUpgrade())
        {
            damage += damagePerLevel * gameController.GetCurrentLevel();
        }
        StartCoroutine(ShootWithLaser());
    }

    private void Update()
    {
        if (shouldShoot)
        {
            audioSource.PlayOneShot(laserClip, volume);
            hit = Physics2D.Raycast(transform.position, transform.up, size, LayerMask.GetMask(PlayerLayerMask));
            if (hit.collider != null)
            {
                gameController.PlayerGetHitByLaser(damage, scoreDecrease.PlayerScoreDecrease());
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, new Vector3(transform.position.x,transform.position.y - hit.distance));
            }
            else
            {
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, new Vector3(transform.position.x,transform.position.y - size));
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    private IEnumerator ShootWithLaser()
    {
        do
        {
            yield return new WaitForSeconds(3f);
            shouldShoot = false;
            lineRenderer.enabled = false;
            yield return new WaitForSeconds(3f);
            shouldShoot = true;
            lineRenderer.enabled = true;
        }
        while (true);
    }
}
