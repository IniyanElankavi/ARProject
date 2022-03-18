using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    public InteractionController interactionController;

    // Start is called before the first frame update
    void Start()
    {
        interactionController = GameObject.FindObjectOfType<InteractionController>();
    }

    void OnMouseDown()
    {
        // Destroy the gameObject after clicking on it
        interactionController.OnTouch();
    }
}
