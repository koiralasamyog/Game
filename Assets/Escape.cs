using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escape : MonoBehaviour
{
    public GameObject button;

    void Update()
    {
        HandleEscapeInput();
    }

    public void HandleEscapeInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Toggle the active state of the button
            button.SetActive(!button.activeSelf);
        }
    }
}
