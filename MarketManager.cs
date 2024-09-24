using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MarketManager : MonoBehaviour
{

    public Text totalScoreText; 
    public int cost = 10; 
    public Image messageImage; 
    public int language;

    private void Start()
    {

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

        // Ïðîâåðÿåì, áûëà ëè óæå ñîâåðøåíà ïîêóïêà, è åñëè äà, ñêðûâàåì êíîïêó.
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
            
            totalScore -= cost;
            PlayerPrefs.SetInt("TotalScore", totalScore);
            totalScoreText.text = "Total Score: " + totalScore.ToString();


            gameObject.SetActive(false);

           
            PlayerPrefs.SetInt("PurchaseMade", 1);
        }
        else
        {
            
            messageImage.gameObject.SetActive(true);
            Invoke("HideMessageImage", 1f);
        }
    }

    private void HideMessageImage()
    {
        messageImage.gameObject.SetActive(false);
    }

}
