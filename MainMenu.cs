using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text totalScoreText; 
    public Text highScoreText;
    public int language;
    MusicManager musicManager;

    private void Start()
    {

        musicManager = GameObject.FindGameObjectWithTag("audio").GetComponent<MusicManager>();

        language = PlayerPrefs.GetInt("language", language);


        
        int totalScore = PlayerPrefs.GetInt("TotalScore", 0);
        if (language == 0)
        {
            totalScoreText.text = "Total Score " + totalScore.ToString();
        }
        else if (language == 1)
        {
            totalScoreText.text = "Î÷ê³ " + totalScore.ToString();
        }

        
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (language == 0)
        {
            highScoreText.text = "High Score " + highScore.ToString();
        }
        else if (language == 1)
        {
            highScoreText.text = "Ðåêîðä " + highScore.ToString();
        }
        
    }

    public void clickButton()
    {
        musicManager.PlaySFX(musicManager.click);
    }


    public void ResetScores()
    {
        
        PlayerPrefs.SetInt("HighScore", 0);

        
        highScoreText.text = "High Score 0";
    }


    
    public void StartGame()
    {
        
        SceneManager.LoadScene("game");
        musicManager.PlaySFX(musicManager.click);
    }

    public void GoToMarket()
    {
        SceneManager.LoadScene("market");
        musicManager.PlaySFX(musicManager.click);
    }




    public void UkrainianLanguage()
    {
        language = 1;
        PlayerPrefs.SetInt("language", language);
        SceneManager.LoadScene("main");
    }

    public void EnglishLanguage()
    {
        language = 0;
        PlayerPrefs.SetInt("language", language);
        SceneManager.LoadScene("main");
    }


    public void ExitGame()
    {

            Application.Quit();

    }

}
