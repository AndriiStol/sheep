using UnityEngine;
using UnityEngine.UI;

public class DontDestroyOnLoadScript : MonoBehaviour
{
    public Button[] buttons; // Array of buttons to hide.
    public int[] highScoreThresholds; // Array of high score thresholds for each button.

    private void Start()
    {
        // Load the high score from PlayerPrefs.
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        // Iterate through the buttons and hide them if the high score is greater than or equal to the threshold.
        for (int i = 0; i < buttons.Length; i++)
        {
            if (highScore >= highScoreThresholds[i])
            {
                buttons[i].gameObject.SetActive(false);
            }
        }
    }
}