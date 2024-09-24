using UnityEngine;
using UnityEngine.UI;



public class PanelOpener : MonoBehaviour
{
    public Button pauseButton; 
    public GameObject panel; 
    public GameObject panel1;
    private bool isPanelOpen = false; 
    private bool isPanel1Open = false;



    
    public void TogglePanel()
    {
       
        isPanelOpen = !isPanelOpen;

       
        panel.SetActive(isPanelOpen);
    }


    public void TogglePanel1()
    {
        
        isPanel1Open = !isPanel1Open;


        
        panel1.SetActive(isPanel1Open);

        pauseButton.gameObject.SetActive(!isPanel1Open);
    }


}
