using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    public Button background1Button;
    public Button background2Button;
    public Button background3Button;

    private int selectedBackground = 1; // Default to the first background.

    private void Start()
    {
        // Load the selected background from PlayerPrefs.
        selectedBackground = PlayerPrefs.GetInt("SelectedBackground", 1);

        // Add click event listeners to your background selection buttons.
        background1Button.onClick.AddListener(() => SelectBackground(1));
        background2Button.onClick.AddListener(() => SelectBackground(2));
        background3Button.onClick.AddListener(() => SelectBackground(3));

        // Set the selected background button's interactable state based on the saved selection.
        background1Button.interactable = selectedBackground != 1;
        background2Button.interactable = selectedBackground != 2;
        background3Button.interactable = selectedBackground != 3;
    }

    private void SelectBackground(int backgroundIndex)
    {
        // Save the selected background to PlayerPrefs.
        selectedBackground = backgroundIndex;
        PlayerPrefs.SetInt("SelectedBackground", selectedBackground);

        // Set the interactable state of the buttons.
        background1Button.interactable = selectedBackground != 1;
        background2Button.interactable = selectedBackground != 2;
        background3Button.interactable = selectedBackground != 3;
    }
}
