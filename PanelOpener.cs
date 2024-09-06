using UnityEngine;
using UnityEngine.UI;



public class PanelOpener : MonoBehaviour
{
    public Button pauseButton; // Ссылка на кнопку паузы.
    public GameObject panel; // Ссылка на вашу панель.
    public GameObject panel1;
    private bool isPanelOpen = false; // Флаг, который определяет, открыта ли панель.
    private bool isPanel1Open = false;



    // Метод, вызываемый при нажатии на кнопку.
    public void TogglePanel()
    {
        // Проверяем состояние панели и меняем его.
        isPanelOpen = !isPanelOpen;

        // Включаем или выключаем панель в зависимости от состояния.
        panel.SetActive(isPanelOpen);
    }


    public void TogglePanel1()
    {
        // Проверяем состояние панели и меняем его.
        isPanel1Open = !isPanel1Open;


        // Включаем или выключаем панель в зависимости от состояния.
        panel1.SetActive(isPanel1Open);

        pauseButton.gameObject.SetActive(!isPanel1Open);
    }


}
