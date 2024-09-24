using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Threading.Tasks;

public class UIManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    
    public Button mainMenuButton; 
    public SheepControl[] sheepControl;
    public Button pauseButton;
    private bool isPaused = false;

    [SerializeField] RectTransform pausePanelRect;
    [SerializeField] float topPosY, middlePosY;
    [SerializeField] float tweenDuration;
    [SerializeField] CanvasGroup canvas;

    MusicManager musicManager;


    private void Start()
    {
        musicManager = GameObject.FindGameObjectWithTag("audio").GetComponent<MusicManager>();


        
        gameOverPanel.SetActive(false);
        Time.timeScale = 1;

        
        mainMenuButton.onClick.AddListener(GoToMainMenu);
        pauseButton.onClick.AddListener(TogglePause);
    }


    public void clickButton()
    {
        musicManager.PlaySFX(musicManager.click);
    }

    async void TogglePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
            PausePanelIntro();
        }
        else
        {
          
            gameOverPanel.SetActive(false);
            Time.timeScale = 1;
            await PausePanelOutro();

        }
    }

    void PausePanelIntro()
    {
        canvas.DOFade(1, tweenDuration).SetUpdate(true);
        pausePanelRect.DOAnchorPosY(middlePosY, tweenDuration). SetUpdate(true);
    }

    async Task PausePanelOutro()
    {
        canvas.DOFade(0, tweenDuration).SetUpdate(true);

       await pausePanelRect.DOAnchorPosY(topPosY, tweenDuration).SetUpdate(true). AsyncWaitForCompletion();
    }



    void GoToMainMenu()
    {

        SceneManager.LoadScene("main");
    }
}
