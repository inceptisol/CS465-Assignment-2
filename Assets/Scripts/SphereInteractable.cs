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
        GameObject.Find("Manager").GetComponent<MyManager>().logData(transform.position, 5);
        //MyManager.instance.logData(transform.position, 5);
        myPosition.x = Random.Range(-0.2f, 0.2f);
        //myPosition.y = Random.Range(-0.005f, 0.005f);
        myPosition.z = Random.Range(-0.2f, 0.2f);
        Instantiate(gameObject, myPosition, transform.rotation, gameObject.transform.parent);
        Destroy(gameObject);
    }

}
