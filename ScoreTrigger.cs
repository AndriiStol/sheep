using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;




public class ScoreTrigger : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText; // Ссылка на текст для отображения рекорда.
    private int score = 0;
    private SheepSpawner sheepSpawner;
    private int highScore; // Переменная для хранения рекорда.

    public GameObject messagePanel; // Используйте объект панели вместо текста.
    public float messageDuration = 2f; // Длительность отображения сообщения в секундах.

    public delegate void ScoreChangedHandler(int newScore);
    public event ScoreChangedHandler OnScoreChanged;
    private bool gameIsOver = false; // Добавлен флаг для отслеживания завершения игры.

    public int language;
    MusicManager musicManager;

    public Text gameOverText; // Добавлен текст для отображения "Game Over" при достижении 100 очков.
    public GameObject gameOverPanel;

    private void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = "Game Over\nScore: " + score.ToString();
        Time.timeScale = 0; // Останавливаем игру.
    }

    private void Start()
    {
        musicManager = GameObject.FindGameObjectWithTag("audio").GetComponent<MusicManager>();

        language = PlayerPrefs.GetInt("language", language);
        DOTween.Init();
        messagePanel.SetActive(false);

        sheepSpawner = FindObjectOfType<SheepSpawner>();
        if (sheepSpawner == null)
        {
            Debug.LogError("Не найден скрипт SheepSpawner.");
        }

        // Загружаем рекорд из PlayerPrefs и отображаем его.
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + highScore.ToString();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            musicManager.PlaySFX(musicManager.score);
            score++;
            
            PlayerPrefs.SetInt("TotalScore", PlayerPrefs.GetInt("TotalScore", 0) + 1);
            if (language == 0)
            {
                scoreText.text = "Score: " + score.ToString();
            }
            else if (language == 1)
            {
                scoreText.text = "Очкі: " + score.ToString();
            }

            // Если текущий счет больше рекорда, обновляем рекорд и сохраняем его в PlayerPrefs.
            if (score > highScore)
            {
                highScore = score;
                PlayerPrefs.SetInt("HighScore", highScore);
                highScoreText.text = "High Score: " + highScore.ToString();
            }

            if (OnScoreChanged != null)
            {
                OnScoreChanged(score);
            }

            // Если текущий счет больше или равен 100, показываем панель "Game Over".
            if (score >= 100 && !gameIsOver)
            {
                gameIsOver = true;
                ShowGameOverPanel();
            }

            if (score % 10 == 0 && sheepSpawner != null)
            {
                sheepSpawner.UpdateSpawnInterval();
                StartCoroutine(DisplayMessageCoroutine());
            }
        }
    }


    private IEnumerator DisplayMessageCoroutine()
    {

        // Включаем панель плавно.
        messagePanel.SetActive(true);
        CanvasGroup canvasGroup = messagePanel.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f; // Начальное значение альфа-компонента.

        // Плавное появление.
        canvasGroup.DOFade(1f, 0.5f);

        musicManager.PlaySFX(musicManager.speedUp);
        // Ждем указанное количество секунд.
        yield return new WaitForSeconds(messageDuration);

        // Плавное исчезновение.
        canvasGroup.DOFade(0f, 0.5f).OnComplete(() => messagePanel.SetActive(false));


    }
}
