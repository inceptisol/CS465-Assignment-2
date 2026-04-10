using UnityEngine;
using System.Collections.Generic;

public class MyManager : MonoBehaviour
{   
    public static int currentTrial = 0;
    public static double time = 0;
    public static Vector3 prevPosition = new Vector3(0, 0, 0);
    public static double a = 0.2;
    public static double b = 7.0532;
                                //dis w  dir   
    static float[] trial1 = new [] {0.1f, 1.0f, -1.0f};
    static float[] trial2 = new [] {0.1f, 1.0f, 1.0f};
    public static float[][] trials = new[] {trial1, trial2};
    public float[] getTrial() {
        return trials[currentTrial];
    }
    public void incrementTrial() {
        currentTrial += 1;
    }
    public void logData(Vector3 position, float W){
        double newTime = Time.timeAsDouble;
        double trueMT = newTime - time;
        time = newTime;
        float A = Mathf.Abs(Vector3.Distance(prevPosition, position));
        double ID = Mathf.Log((A/W) + 1, 2);
        //Debug.Log(string.Format("A = {0}, W = {1}, ID = {2}", A, W, Mathf.Log((2 * A)/W, 2)));
        //b = (MT - a)/ID;
        double shannonPredictedMT = a + b * ID;
        double origPredictedMT = a + b * Mathf.Log((2 * A)/W, 2);
        Debug.Log(string.Format("Original Predicted MT = {0}, Shannon Predicted MT = {1}, Actual = {2}!", origPredictedMT, shannonPredictedMT, trueMT));
    }
    
    private static MyManager _instance;
    public static MyManager instance {
        get {
            if(_instance == null)
            {
                _instance = FindObjectOfType<MyManager>(); //Only ever ran once, no prefromance issue
            }
            return _instance;
        }
    }
} 
