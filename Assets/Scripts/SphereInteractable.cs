using UnityEngine;

public class SphereInteractable : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Grab()
    {
        Debug.Log("HELLOOO");
    }
    public void EndGrab()
    {
        Debug.Log("MOVING");
        //adjusts position
        var myPosition = transform.position;
        myPosition.y += 0.10f;
        myPosition.x += 0.10f;
        transform.position = myPosition;
    }

}
