using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;   

    public AudioClip backgroundMusic;
    public AudioClip[] jumpSounds;
    public AudioClip pickUpSound;
    public AudioClip moneySound;
    public AudioClip buttonSound;

    void Start()
    {
        PlayMusic(backgroundMusic);
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }
    public void PlaySound(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
    public void PlayMusic(AudioClip clip)
    {
        musicSource.Play();
    }
    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void HandleJump(){
        int randomNum = Random.Range(0, jumpSounds.Length);
        PlaySound(jumpSounds[randomNum]);
    }

    public void HandlePickUp(){
        PlaySound(pickUpSound);
    }

    public void HandleButtonClick(){
        PlaySound(buttonSound);
    }

    public void HandleMoney(){
        PlaySound(moneySound);
    }
}
