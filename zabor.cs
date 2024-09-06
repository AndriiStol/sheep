using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;
public class zabor : MonoBehaviour
{
    public GameObject pausepanel;
    public LayerMask playerLayer; // Добавляем переменную для слоя игрока.
    public Button pauseButton; // Ссылка на кнопку паузы.
    [SerializeField] RectTransform pausePanelRect;
    [SerializeField] float topPosY, middlePosY;
    [SerializeField] float tweenDuration;
    [SerializeField] CanvasGroup canvas;

    private bool collisionHandled = false;

    MusicManager musicManager;

    private void Awake()
    {
        musicManager = GameObject.FindGameObjectWithTag("audio").GetComponent<MusicManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверяем, является ли объект игроком на основе слоя.
        if (!collisionHandled && playerLayer == (playerLayer | (1 << collision.gameObject.layer)))
        {
            collisionHandled = true; // Помечаем столкновение как обработанное.

            StartCoroutine(DelayedGameOver());

            
        }
    }

    public IEnumerator DelayedGameOver()
    {
        musicManager.PlaySFX(musicManager.die);

        // Ждем две секунды перед выполнением следующих действий.
        yield return new WaitForSeconds(0.2f);
        PausePanelIntro();

        // Остановка игры
        Time.timeScale = 0;




        // Скрываем кнопку паузы
        if (pauseButton != null)
        {
            pauseButton.gameObject.SetActive(false);
        }
    }


    void PausePanelIntro()
    {
        pausepanel.SetActive(true);
        canvas.DOFade(1, tweenDuration).SetUpdate(true);
        pausePanelRect.DOAnchorPosY(middlePosY, tweenDuration). SetUpdate(true);
    }
}
