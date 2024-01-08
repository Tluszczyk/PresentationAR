using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasList : MonoBehaviour
{
    [SerializeField]
    private GameObject startCanvas;

    [SerializeField]
    private GameObject appCanvas;

    void Start()
    {
        appCanvas.SetActive(false);
    }

    public void disableCanvasVisibility() {
        startCanvas.SetActive(false);
        appCanvas.SetActive(true);
    }
}
