using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Disappear : UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable {

    [SerializeField] private float speed = 100.0f;
    Vector3 initialPosition;
    bool moved = false;
    bool initiallyMoved = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);
        if (!initiallyMoved)
        {
            initialPosition = transform.position;
            initiallyMoved = true;
            Debug.Log("initially moved!");
        }
        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {
            if (isSelected && !moved)
            {
                Debug.Log(Vector3.Distance(initialPosition, transform.position));
                if (Vector3.Distance(initialPosition, transform.position) > 1)
                {
                    Debug.Log("ITS BEEN MOVED");
                    moved = true;
                }
            }
        }
    }
}
