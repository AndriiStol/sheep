using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;
public class zabor : MonoBehaviour
{
    public GameObject pausepanel;
    public LayerMask playerLayer; 
    public Button pauseButton; 
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
        
        if (!collisionHandled && playerLayer == (playerLayer | (1 << collision.gameObject.layer)))
        {
            collisionHandled = true; 

            StartCoroutine(DelayedGameOver());

            
        }
    }

    public IEnumerator DelayedGameOver()
    {
        musicManager.PlaySFX(musicManager.die);

        yield return new WaitForSeconds(0.2f);
        PausePanelIntro();

        Time.timeScale = 0;




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
