using UnityEngine;
using System.Collections.Generic;

public class MyManager : MonoBehaviour
{   
    public static int currentTrial = 0;
    public static double time = 0;
    public static Vector3 prevPosition = new Vector3(0, 0, 0);
    static int[] trial1 = new [] {1, 2};
    static int[] trial2 = new [] {1, 3};
    public static int[][] trials = new[] {trial1, trial2};

    public void logData(Vector3 position, float W){
        double newTime = Time.timeAsDouble;
        double MT = newTime - time;
        time = newTime;
        float A = Vector3.Distance(prevPosition, position);
        double ID = Mathf.Log((A/W) + 1);
        Debug.Log(MT);
        Debug.Log(ID);
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
