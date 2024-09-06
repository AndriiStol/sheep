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
        // Убедитесь, что существует только один MusicManager в игре
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Этот объект не будет уничтожен при загрузке новой сцены
        }
        else
        {
            // Если уже существует другой MusicManager, уничтожьте этот
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Здесь ваш код инициализации музыки
        musicSource.Play();
        musicSource.clip = back;
    }

    public void PlaySFX(AudioClip clip) 
    {
        sfxSource.PlayOneShot(clip);
    }




}
