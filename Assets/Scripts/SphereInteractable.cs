using UnityEngine;


public class SphereInteractable : MonoBehaviour
{
    Material material;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        material = Resources.Load(Application.dataPath + "/VRTemplateAssets/Materials/Anchor Materials/Blue Anchor Arrow.mat", typeof(Material)) as Material;
    }
    public void Grab()
    {
        if (MyManager.currentTrial >= 6) 
            return;

        var manager = GameObject.Find("Manager").GetComponent<MyManager>();
        var trial = manager.getTrial();
        manager.logData(transform.position, trial[1]);
        manager.incrementTrial();

        ExperimentHUD.instance.ShowResult(true);
        ExperimentHUD.instance.UpdateHUD();

        var newPosition = transform.position;
        newPosition.x = trial[0] * trial[2];
        var newSphere = Instantiate(gameObject, newPosition, transform.rotation, GameObject.Find("Main Camera").transform);
        newSphere.transform.localScale = new Vector3(trial[1], trial[1], trial[1]);
        newSphere.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        var material = Resources.Load("Blue Anchor Arrow", typeof(Material)) as Material;
        newSphere.transform.GetChild(1).gameObject.GetComponent<Renderer>().material = material;
        Destroy(gameObject);
    }
    public void EndGrab()
    {
        Debug.Log("Grab ended");
    }

}
