using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MarketManager : MonoBehaviour
{

    public Text totalScoreText; // Ссылка на текст для отображения общего счета.
    public int cost = 10; // Стоимость покупки.
    public Image messageImage; // Ссылка на объект изображения для отображения сообщения.
    public int language;

    private void Start()
    {

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

        // Проверяем, была ли уже совершена покупка, и если да, скрываем кнопку.
        if (PlayerPrefs.GetInt("PurchaseMade", 0) == 1)
        {
            gameObject.SetActive(false);
        }
    }


    public void OnPurchaseButtonClick()
    {
        int totalScore = PlayerPrefs.GetInt("TotalScore", 0);

        if (totalScore >= cost)
        {
            // У вас достаточно очков для покупки.
            totalScore -= cost;
            PlayerPrefs.SetInt("TotalScore", totalScore);
            totalScoreText.text = "Total Score: " + totalScore.ToString();

            // Выполните здесь действия по покупке, например, активируйте какой-то объект или выполните другую логику.
            // Скрыть кнопку покупки.
            gameObject.SetActive(false);

            // Дополнительно: сохранить информацию, что покупка совершена, чтобы кнопка не появлялась при последующих запусках игры.
            PlayerPrefs.SetInt("PurchaseMade", 1);
        }
        else
        {
            // У вас недостаточно очков для покупки. Показываем изображение на короткое время.
            messageImage.gameObject.SetActive(true);
            Invoke("HideMessageImage", 1f);
        }
    }

    private void HideMessageImage()
    {
        messageImage.gameObject.SetActive(false);
    }

}
