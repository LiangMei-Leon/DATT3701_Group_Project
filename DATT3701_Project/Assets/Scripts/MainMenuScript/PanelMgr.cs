using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelMgr : MonoBehaviour {

    public GameObject StartPanel;
    public GameObject MainPanel;
    public GameObject BagPanel;
    public GameObject ChooseLevelPanel;
    public GameObject TipPanel;
    public Slider slider;
    public AudioSource BGsound;
    public void volum()
    {
        BGsound.volume = slider.value;
    }

    public void ClosStartPanel()
    {
        StartPanel.SetActive(false);
        MainPanel.SetActive(true);
    }

    public void CloseAll()
    {
        MainPanel.SetActive(false);
        BagPanel.SetActive(false);
        ChooseLevelPanel.SetActive(false);
    }
    public void OpenMainPanel()
    {
        CloseAll();

        MainPanel.SetActive(true);
    }
    public void CloseMainPanel()
    {
        CloseAll();
        MainPanel.SetActive(false);
    }
    public void OpenBagPanel()
    {
        CloseAll();
        BagPanel.SetActive(true);
    }
    public void CloseBagPanel()
    {
        CloseAll();
        BagPanel.SetActive(false);
    }
    public void OpenChooseLevelPanel()
    {
        CloseAll();
        ChooseLevelPanel.SetActive(true);
    }
    public void ClosChooseLevelPanel()
    {
        CloseAll();
        ChooseLevelPanel.SetActive(false);
    }
    public void OpenTipPanel()
    {
        TipPanel.SetActive(true);
    }
    public void ClosTipPanel()
    {
        TipPanel.SetActive(false);
    }
}
