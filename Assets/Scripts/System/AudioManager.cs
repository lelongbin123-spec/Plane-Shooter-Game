using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Music")]
    [SerializeField] private AudioClip backgroundMusic;

    [Header("UI")]
    [SerializeField] private AudioClip buttonClick;

    [Header("Gameplay SFX")]
    [SerializeField] private AudioClip playerShoot;
    [SerializeField] private AudioClip enemyShoot;
    [SerializeField] private AudioClip explosion;
    [SerializeField] private AudioClip playerHit;
    [SerializeField] private AudioClip playerDeath;
    [SerializeField] private AudioClip coinPickup;

    public static bool HasInstance => Instance != null;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        EnsureAudioSources();
    }

    private void Start()
    {
        PlayMusic(backgroundMusic);
    }

    private void EnsureAudioSources()
    {
        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.playOnAwake = false;
            musicSource.loop = true;
            musicSource.spatialBlend = 0f;
        }

        if (sfxSource == null)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.playOnAwake = false;
            sfxSource.spatialBlend = 0f;
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (musicSource == null || clip == null)
        {
            return;
        }

        if (musicSource.clip == clip && musicSource.isPlaying)
        {
            return;
        }

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource == null || clip == null)
        {
            return;
        }

        sfxSource.PlayOneShot(clip);
    }

    public void PlayButtonClick()
    {
        PlaySFX(buttonClick);
    }

    public void PlayPlayerShoot()
    {
        PlaySFX(playerShoot);
    }

    public void PlayEnemyShoot()
    {
        PlaySFX(enemyShoot);
    }

    public void PlayExplosion()
    {
        PlaySFX(explosion);
    }

    public void PlayPlayerHit()
    {
        PlaySFX(playerHit);
    }

    public void PlayPlayerDeath()
    {
        PlaySFX(playerDeath != null ? playerDeath : explosion);
    }

    public void PlayCoinPickup()
    {
        PlaySFX(coinPickup);
    }
}
