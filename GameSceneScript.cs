using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneScript : MonoBehaviour
{
    public Sprite[] backgrounds; // Array of background sprites.
    public Image backgroundDisplay; // Reference to the Image component where the background is displayed.

    private int selectedBackground = 1; // Default to the first background.

    private void Start()
    {
        // Load the selected background from PlayerPrefs.
        selectedBackground = PlayerPrefs.GetInt("SelectedBackground", 1);

        // Set the background image based on the selected background index.
        if (selectedBackground >= 1 && selectedBackground <= backgrounds.Length)
        {
            backgroundDisplay.sprite = backgrounds[selectedBackground - 1];
        }
    }
}
