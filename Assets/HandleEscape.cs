using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleEscape : MonoBehaviour
{
    public GameObject button;
    public void HandleEscapeInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            button.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                button.SetActive(true);
            }
        }
            
    }
}
