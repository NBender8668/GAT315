using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelSelect : MonoBehaviour
{
    [System.Serializable]
    public struct panelInfo
    {
        public GameObject panel;
        public Button button;
        public KeyCode keyCode;
    }
    public KeyCode toggleKey;
    public GameObject masterPanel;
    public panelInfo[] panelInfos;

     void Start()
     {
        foreach (panelInfo panelInfo in panelInfos)
        {
            panelInfo.button.onClick.AddListener(delegate { ButtonEvent(panelInfo); });
        }
     }

     void Update()
    {
        if(Input.GetKeyDown(toggleKey))
        {
            masterPanel.SetActive(!masterPanel.activeSelf);
        }
        foreach (panelInfo panelInfo in panelInfos)
        {
            if(Input.GetKeyDown(panelInfo.keyCode))
            {
                setPanelActive(panelInfo);
            }
        }   
    }
    void setPanelActive( panelInfo panelInfo)
    {
        for (int i = 0; i < panelInfos.Length; i++)
        {
            bool active = panelInfos[i].Equals(panelInfo);
            panelInfos[i].panel.SetActive(active);
        }
    }

    void ButtonEvent(panelInfo panelInfo)
    {
        setPanelActive(panelInfo);
    }
}
