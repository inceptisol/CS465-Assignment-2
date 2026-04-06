using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Disappear : UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable {

    [SerializeField] private float speed = 100.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {
            if (isSelected)
            {
                Debug.Log("Hey there!");
                Debug.Log("I'm losing my mind!");
            }
        }
    }
}
