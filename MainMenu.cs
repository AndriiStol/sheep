using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text totalScoreText; // Ссылка на текст для отображения общего счета.
    public Text highScoreText; // Ссылка на текст для отображения рекорда.
    public int language;
    MusicManager musicManager;

    private void Start()
    {

        musicManager = GameObject.FindGameObjectWithTag("audio").GetComponent<MusicManager>();

        language = PlayerPrefs.GetInt("language", language);


        // Загружаем общий счет из PlayerPrefs и отображаем его.
        int totalScore = PlayerPrefs.GetInt("TotalScore", 0);
        if (language == 0)
        {
            totalScoreText.text = "Total Score " + totalScore.ToString();
        }
        else if (language == 1)
        {
            totalScoreText.text = "Очкі " + totalScore.ToString();
        }

        // Загружаем рекорд из PlayerPrefs и отображаем его.
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (language == 0)
        {
            highScoreText.text = "High Score " + highScore.ToString();
        }
        else if (language == 1)
        {
            highScoreText.text = "Рекорд " + highScore.ToString();
        }
        
    }

    public void clickButton()
    {
        musicManager.PlaySFX(musicManager.click);
    }


    public void ResetScores()
    {
        // Сброс рекорда в PlayerPrefs.
        PlayerPrefs.SetInt("HighScore", 0);

        // Обновляем отображаемый текст на экране.
        highScoreText.text = "High Score 0";
    }


    // Метод, вызываемый при нажатии на кнопку "Play".
    public void StartGame()
    {
        // Загружаем сцену с игрой (название "game").
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