using System;
using UnityEngine;

public class InteractableLoreElement : MonoBehaviour
{

    public Canvas indicatorCanvas;
    public Canvas loreCanvas;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("MovableRobot"))
            return;
        
        indicatorCanvas.gameObject.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("MovableRobot"))
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!loreCanvas.gameObject.activeSelf)
            {
                indicatorCanvas.gameObject.SetActive(false);
                loreCanvas.gameObject.SetActive(true);
            }
            else
            {
                indicatorCanvas.gameObject.SetActive(true);
                loreCanvas.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("MovableRobot"))
            return;
        
        indicatorCanvas.gameObject.SetActive(false);
        loreCanvas.gameObject.SetActive(false);
    }
}
