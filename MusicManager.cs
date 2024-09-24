using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    public AudioClip back;
    public AudioClip die;
    public AudioClip score;
    public AudioClip spawn;
    public AudioClip click;
    public AudioClip speedUp;

    public int language;

    private void Awake()
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

    private void Start()
    {
       
        musicSource.Play();
        musicSource.clip = back;
    }

    public void PlaySFX(AudioClip clip) 
    {
        sfxSource.PlayOneShot(clip);
    }




}
