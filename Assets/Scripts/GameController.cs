using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("WaveConfig SO")]
    [SerializeField] private List<WaveConfig> waveConfigList;
    [SerializeField] private List<WaveConfig> optionalWaveConfigList;
    [SerializeField] private WaveConfig bossWave;
    [Header("Spawn Options")]
    [SerializeField] private float timeBetweenSpawnMin;
    [SerializeField] private float timeBetweenSpawnMax;
    [SerializeField] private float timeBetweenWavesMin;
    [SerializeField] private float timeBetweenWavesMax;
    [SerializeField] private bool shouldSpawn;
    [SerializeField] private GameObject endgameText;
    [SerializeField] private Animator animator;
    private WaveConfig currentWave;
    private DamageBuffController damageBuffController;
    private SceneController sceneController;
    private GameObject boss;
    private AudioPlayer audioPlayer;
    private CameraShake cameraShake;
    private StatsController playerStats;
    private ScoreKeeper scoreKeeper;
    private float currentLevel;
    private Player player;
    private int initialGameLoops;
    private float initialTimeBetweenSpawnMax;
    private float initialTimeBetweenWavesMax;
    private int loopsBeforeBossSpawn;
    private bool shouldUpgrade;
    private bool shouldBuff;
    private bool isPlayerDead;
    private float buffAmount;

    private const string sceneAnimationTrigger = "Start";

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        damageBuffController = FindObjectOfType<DamageBuffController>();
        sceneController = FindObjectOfType<SceneController>();
        loopsBeforeBossSpawn = waveConfigList.Count;
        initialTimeBetweenSpawnMax = timeBetweenSpawnMax;
        initialTimeBetweenWavesMax = timeBetweenWavesMax;
        player = FindObjectOfType<Player>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        playerStats = player.GetComponent<StatsController>();
        cameraShake = FindObjectOfType<CameraShake>();
    }

    private void Start()
    {
        animator.SetTrigger(sceneAnimationTrigger);
        float playerHealth = playerStats.GetHealth();
        scoreKeeper.UpdateHPText(playerHealth);
        scoreKeeper.UpdateMaxHealthText(playerHealth);
        StartCoroutine(Spawn());
    }

    private void Update()
    {
        if (!damageBuffController.IsBuffed())
        {
            shouldBuff = false;
        }
    }

    public IEnumerator Spawn()
    {
        do
        {
            if (currentLevel > 0)
            {
                shouldUpgrade = true;
            }
            if (loopsBeforeBossSpawn == 0)
            {
                SpawnBoss();
                currentLevel++;
                AddRandomOptionalWave();
                loopsBeforeBossSpawn = waveConfigList.Count;
                initialGameLoops = loopsBeforeBossSpawn;
                break;
            }

            foreach (WaveConfig wave in waveConfigList)
            {
                currentWave = wave;
                List<GameObject> enemies = wave.GetEnemies();
                for (int i = 0; i < enemies.Count; i++)
                {
                    GameObject enemy = Instantiate(enemies[i], wave.GetStartingWayPoint().position, Quaternion.identity);
                    yield return new WaitForSeconds(Random.Range(timeBetweenSpawnMin, timeBetweenSpawnMax));
                }
                yield return new WaitForSeconds(Random.Range(timeBetweenWavesMin, timeBetweenWavesMax));
                loopsBeforeBossSpawn--;
            }
            if (timeBetweenSpawnMax > 1
                && timeBetweenWavesMax > 1)
            {
                timeBetweenWavesMax--;
                timeBetweenSpawnMax--;
            }
            else
            {
                timeBetweenWavesMax = initialTimeBetweenWavesMax;
                timeBetweenSpawnMax = initialTimeBetweenSpawnMax;
            }
        }
        while (shouldSpawn);
    }

    public bool ShouldUpgrade()
    {
        return shouldUpgrade;
    }

    private void AddRandomOptionalWave()
    {
        if (optionalWaveConfigList.Count > 0)
        {
            WaveConfig newWave = optionalWaveConfigList[Random.Range(0, optionalWaveConfigList.Count)];
            waveConfigList.Add(newWave);
            optionalWaveConfigList.Remove(newWave);
        }
    }

    private void SpawnBoss()
    {
        currentWave = bossWave;
        boss = Instantiate(bossWave.GetEnemies()[0],
                    bossWave.GetStartingWayPoint().position,
                    Quaternion.Euler(0, 0, 180));
    }

    public WaveConfig GetCurrentWave()
    {
        return currentWave;
    }

    public void StartNextLevel()
    {
        playerStats.UpgradePlayerStatsForLevel();
        scoreKeeper.UpdateMaxHealthText(playerStats.GetInitialHealth());
        StartCoroutine(Spawn());
    }

    public bool ShouldBuff()
    {
        return shouldBuff;
    }

    public float GetBuffAmount()
    {
        return buffAmount;
    }

    public float GetCurrentLevel()
    {
        return currentLevel;
    }

    public bool IsPlayerDead()
    {
        return isPlayerDead;
    }

    public void HandleHit(float penalty)
    {
        scoreKeeper.RemoveFromScore(penalty);
        scoreKeeper.UpdateScoreText();
        scoreKeeper.UpdateHPText(playerStats.GetHealth());
        StartCoroutine(cameraShake.Shake());
        audioPlayer.Vibrate();
    }

    public void RefillHealth(float refillAmount)
    {
        playerStats.AddHealth(refillAmount);
        scoreKeeper.UpdateHPText(playerStats.GetHealth());
    }

    public void HandlePlayerDeath()
    {
        endgameText.SetActive(true);
        isPlayerDead = true;
        scoreKeeper.UpdateHPText(0);
        scoreKeeper.SetMaxScore();
        StartCoroutine(sceneController.LoadEndScreen());
    }

    public void AddScoreToPlayer(float score)
    {
        scoreKeeper.AddToScore(score);
        scoreKeeper.UpdateScoreText();
    }

    public void PlayerGetHitByLaser(float laserDamage, float laserPenalty)
    {
        playerStats.TakeDamage(laserDamage);
        HandleHit(laserPenalty);
    }

    public void BoostDamage(float damage, float time)
    {
        damageBuffController.StartTimer(time);
        buffAmount = damage;
        shouldBuff = true;
    }
}