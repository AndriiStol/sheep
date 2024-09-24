using UnityEngine;
using UnityEngine.UI;

public class panelopenandclose : MonoBehaviour
{
    public GameObject panel;



    public void OpenPanel()
    {
        if (panel != null)
        {
            panel.SetActive(true);
        }
    }

    public void ClosePanel()
    {
        if (panel != null)
        {
            panel.SetActive(false);
        }
    }

}
