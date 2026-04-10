using UnityEngine;


public class SphereInteractable : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public void Grab()
    {
        Debug.Log("MOVING");
        //adjusts position
        GameObject.Find("Manager").GetComponent<MyManager>().logData(transform.position, 5);
        GameObject.Find("Manager").GetComponent<MyManager>().incrementTrial();
        Instantiate(gameObject, transform.position, transform.rotation, gameObject.transform.parent);
        Destroy(gameObject);
    }
    void Start() {
        var trial = GameObject.Find("Manager").GetComponent<MyManager>().getTrial();
        var myPosition = transform.position;
        myPosition.x = trial[0] * trial[2];
        transform.position = myPosition;
        Debug.Log(transform.position);
        //transform.localScale = new Vector3(trial[1], trial[1], trial[1]);
    }
    public void EndGrab()
    {
        Debug.Log("WOAH");
    }

}
