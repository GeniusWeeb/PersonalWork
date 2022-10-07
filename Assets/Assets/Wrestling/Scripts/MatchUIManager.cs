using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using WrestleSimualtor;

public class MatchUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    
    private static MatchUIManager _instance;

    public static MatchUIManager Instance => _instance;

    public GameObject uiPopup;
    private TextMeshProUGUI  statusText;

    private void Awake()
    {
        _instance = this;
        statusText=  uiPopup.GetComponentInChildren<TextMeshProUGUI>();

    }

    public void ShowPopUp(PowerMode mode)
    {
   
    StartCoroutine(ShowBlinkPopUp(mode));
        

    }


    IEnumerator ShowBlinkPopUp(PowerMode mode) {

        uiPopup.SetActive(true);
        switch (mode)
        {

            case PowerMode.Strike:
                statusText.text = "Strike Priority"; 
                break;
            case PowerMode.Clash:
                statusText.text = "Clash";
                break;
            case PowerMode.Power:
                statusText.text = "Power Priority";
                break;

        }

        yield return new WaitForSeconds(0.3f);
        uiPopup.SetActive(false); 
        yield return new WaitForSeconds(0.3f);
        uiPopup.SetActive(true); 
        yield return new WaitForSeconds(0.3f);
        uiPopup.SetActive(false);
       
    
    }



}
