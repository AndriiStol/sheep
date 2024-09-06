using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectSkin : MonoBehaviour
{
    public GameObject Skin1, Skin2, Skin3;
    int selectSkin1, selectSkin2, selectSkin3;
    public Button selectSkin1Button; // Reference to the button for skin 1.
    public Button selectSkin2Button; // Reference to the button for skin 2.
    public Button selectSkin3Button; // Reference to the button for skin 3.
    MusicManager musicManager;



    public void GoToMainMenu()
    {
        // Переходим на сцену с именем "main".
        SceneManager.LoadScene("main");
    }

    void Start()
    {
        musicManager = GameObject.FindGameObjectWithTag("audio").GetComponent<MusicManager>();


        selectSkin1 = PlayerPrefs.GetInt("selectSkin1", 1);
        selectSkin2 = PlayerPrefs.GetInt("selectSkin2", 1);
        selectSkin3 = PlayerPrefs.GetInt("selectSkin3", 1);

        // Set up button click listeners.
        selectSkin1Button.onClick.AddListener(() => SelectOneSkin());
        selectSkin2Button.onClick.AddListener(() => SelectTwoSkin());
        selectSkin3Button.onClick.AddListener(() => SelectTreeSkin());

        // Set the interactable state of the buttons based on the saved selection.
        selectSkin1Button.interactable = selectSkin1 != 1;
        selectSkin2Button.interactable = selectSkin2 != 2;
        selectSkin3Button.interactable = selectSkin3 != 3;
    }

    private void Update()
    {
        if (selectSkin1 == 1)
        {
            Skin1.SetActive(true);
            Skin2.SetActive(false);
            Skin3.SetActive(false);
        }
        else
        {
            Skin1.SetActive(false);
        }

        if (selectSkin2 == 2)
        {
            Skin1.SetActive(false);
            Skin2.SetActive(true);
            Skin3.SetActive(false);
        }
        else
        {
            Skin2.SetActive(false);
        }

        if (selectSkin3 == 3)
        {
            Skin1.SetActive(false);
            Skin2.SetActive(false);
            Skin3.SetActive(true);
        }
        else
        {
            Skin3.SetActive(false);
        }

    }

    public void SelectOneSkin()
    {
        selectSkin1 = 1;
        PlayerPrefs.SetInt("selectSkin1", selectSkin1);
        selectSkin2 = 1;
        PlayerPrefs.SetInt("selectSkin2", selectSkin2);
        selectSkin3 = 1;
        PlayerPrefs.SetInt("selectSkin3", selectSkin3);

        // Set the interactable state of the buttons.
        selectSkin1Button.interactable = false;
        selectSkin2Button.interactable = true;
        selectSkin3Button.interactable = true;
    }

    public void SelectTwoSkin()
    {
        selectSkin1 = 2;
        PlayerPrefs.SetInt("selectSkin1", selectSkin1);
        selectSkin2 = 2;
        PlayerPrefs.SetInt("selectSkin2", selectSkin2);
        selectSkin3 = 2;
        PlayerPrefs.SetInt("selectSkin3", selectSkin3);

        selectSkin1Button.interactable = true;
        selectSkin2Button.interactable = false;
        selectSkin3Button.interactable = true;
    }

    public void SelectTreeSkin()
    {
        selectSkin1 = 3;
        PlayerPrefs.SetInt("selectSkin1", selectSkin1);
        selectSkin2 = 3;
        PlayerPrefs.SetInt("selectSkin2", selectSkin2);
        selectSkin3 = 3;
        PlayerPrefs.SetInt("selectSkin3", selectSkin3);

        selectSkin1Button.interactable = true;
        selectSkin2Button.interactable = true;
        selectSkin3Button.interactable = false;
    }


    public void clickButton()
    {
        musicManager.PlaySFX(musicManager.click);
    }
}