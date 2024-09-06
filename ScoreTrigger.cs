using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;




public class ScoreTrigger : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText; // ������ �� ����� ��� ����������� �������.
    private int score = 0;
    private SheepSpawner sheepSpawner;
    private int highScore; // ���������� ��� �������� �������.

    public GameObject messagePanel; // ����������� ������ ������ ������ ������.
    public float messageDuration = 2f; // ������������ ����������� ��������� � ��������.

    public delegate void ScoreChangedHandler(int newScore);
    public event ScoreChangedHandler OnScoreChanged;
    private bool gameIsOver = false; // �������� ���� ��� ������������ ���������� ����.

    public int language;
    MusicManager musicManager;

    public Text gameOverText; // �������� ����� ��� ����������� "Game Over" ��� ���������� 100 �����.
    public GameObject gameOverPanel;

    private void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = "Game Over\nScore: " + score.ToString();
        Time.timeScale = 0; // ������������� ����.
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
            Debug.LogError("�� ������ ������ SheepSpawner.");
        }

        // ��������� ������ �� PlayerPrefs � ���������� ���.
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
                scoreText.text = "���: " + score.ToString();
            }

            // ���� ������� ���� ������ �������, ��������� ������ � ��������� ��� � PlayerPrefs.
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

            // ���� ������� ���� ������ ��� ����� 100, ���������� ������ "Game Over".
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

        // �������� ������ ������.
        messagePanel.SetActive(true);
        CanvasGroup canvasGroup = messagePanel.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f; // ��������� �������� �����-����������.

        // ������� ���������.
        canvasGroup.DOFade(1f, 0.5f);

        musicManager.PlaySFX(musicManager.speedUp);
        // ���� ��������� ���������� ������.
        yield return new WaitForSeconds(messageDuration);

        // ������� ������������.
        canvasGroup.DOFade(0f, 0.5f).OnComplete(() => messagePanel.SetActive(false));


    }
}
