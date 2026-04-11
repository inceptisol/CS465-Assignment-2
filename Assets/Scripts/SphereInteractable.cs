using UnityEngine;
using System.Collections;

public class SphereInteractable : MonoBehaviour
{
    private bool trialResolved = false;
    private float missTimeout = 5.0f;

    void Start() {
        StartCoroutine(MissTimer());
    }

    private IEnumerator MissTimer() {
        yield return new WaitForSeconds(missTimeout);
        if (!trialResolved)
            ResolveTrial(false);
    }

    public void Grab() {
        if (trialResolved) return;           
        if (MyManager.experimentOver) return;
        ResolveTrial(true);                 
    }

    private void ResolveTrial(bool wasHit) {
        trialResolved = true;

        var manager = MyManager.instance;
        manager.logData(transform.position, 5, wasHit);

        if (wasHit)
            manager.RegisterHit();
        else
            manager.RegisterMiss();

        ExperimentHUD.instance.ShowResult(wasHit);
        ExperimentHUD.instance.UpdateHUD();

        if (!MyManager.experimentOver)
            SpawnNext();

        Destroy(gameObject);
    }

    private void SpawnNext() {
        var trial = MyManager.instance.getTrial();
        var newPosition = transform.position;
        newPosition.x = trial[0] * trial[2];
        var newSphere = Instantiate(gameObject, newPosition, transform.rotation,
                            GameObject.Find("Main Camera").transform);
        newSphere.transform.localScale = new Vector3(trial[1], trial[1], trial[1]);
        newSphere.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        var mat = Resources.Load("Blue Anchor Arrow", typeof(Material)) as Material;
        newSphere.transform.GetChild(1).gameObject.GetComponent<Renderer>().material = mat;
    }

    public void EndGrab() {
        Debug.Log("Grab ended");
    }
}