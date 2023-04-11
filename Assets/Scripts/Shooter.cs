using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float shootDelay;
    private AudioPlayer audioPlayer;
    private GameController gameController;

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        gameController = FindObjectOfType<GameController>();
    }

    private void Start()
    {
        StartCoroutine(Shoot());
    }

    public IEnumerator Shoot()
    {
        do
        {
            if (gameController.IsPlayerDead())
            {
                break;
            }
            Instantiate(bullet, transform.position, Quaternion.identity);
            audioPlayer.PlayShootingClip();
            yield return new WaitForSeconds(shootDelay);
        }
        while (true);  
    }
}