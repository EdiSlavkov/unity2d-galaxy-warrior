using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] private AudioClip shootingClip;
    [SerializeField] [Range(0, 1)] private float shootingVolume;
    [Header("Damage")]
    [SerializeField] private AudioClip damageClip;
    [SerializeField] [Range(0, 1)] private float damageVolume;
    private static AudioPlayer audioPlayer;
    private bool shouldVibrate = true;
    private Vector3 cameraPosition;

    private void Awake()
    {
        if (audioPlayer != null)
        {
            Destroy(gameObject);
        }
        else
        {
            audioPlayer = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        cameraPosition = Camera.main.transform.position;
    }

    public void PlayShootingClip()
    {
        PlayClip(shootingClip, shootingVolume);
    }

    public void PlayDamageClip()
    {
        PlayClip(damageClip, damageVolume);
    }

    public void PlayClip(AudioClip audio, float volume)
    {
        AudioSource.PlayClipAtPoint(audio, cameraPosition, volume);
    }

    public void Vibrate()
    {
        if (Application.platform == RuntimePlatform.Android && shouldVibrate)
        {
            Handheld.Vibrate();
        }
    }

    public void SetShouldVibrate(bool state)
    {
        shouldVibrate = state;
    }
}
