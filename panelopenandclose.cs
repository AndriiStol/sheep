using UnityEngine;
using UnityEngine.UI;

public class panelopenandclose : MonoBehaviour
{
    public GameObject panel;



    public void OpenPanel()
    {
        // Открыть панель
        if (panel != null)
        {
            panel.SetActive(true);
        }
    }

    public void ClosePanel()
    {
        // Закрыть панель
        if (panel != null)
        {
            panel.SetActive(false);
        }
    }

}
