using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public GameObject panel;

    public void OpenCredit()
    {
        panel.SetActive(true);
    }

    public void CloseCredit()
    {
        panel.SetActive(false);
    }
}
