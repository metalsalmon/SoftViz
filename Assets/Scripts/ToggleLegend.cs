using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLegend : MonoBehaviour
{
    public GameObject panel;
    public void OpenPanel()
    {
        if (panel != null)
        {
            panel.SetActive(!panel.activeSelf);
        }
    }
}
