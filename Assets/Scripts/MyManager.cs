using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class MyManager : MonoBehaviour
{   
    public static int currentTrial = 0;
    public static double time = 0;
    public static Vector3 prevPosition = new Vector3(0, 0, 0);
    public static double a = 0.2;
    public static double b = 7.0532;
                                //dis w  dir   
    static float[] trial1 = new [] {0.35f, 10.0f, -1.0f};
    static float[] trial2 = new [] {0.35f, 20.0f, 1.0f};
    public static float[][] trials = new[] {trial1, trial2};
    private List<string> csvRows= new List<string>();

    public float[] getTrial() {
        return trials[currentTrial % trials.Length];
    }
    public void incrementTrial() {        
        if (currentTrial >= 6)
            return;
        

        currentTrial += 1;

        if (currentTrial >= 6)
            saveCSV();
    }
    public void logData(Vector3 position, float W){
        double newTime = Time.timeAsDouble;
        double trueMT = newTime - time;
        time = newTime;
        float A = Mathf.Abs(Vector3.Distance(prevPosition, position));
        double ID = Mathf.Log((A/W) + 1, 2);
        double TP = ID / trueMT;

        prevPosition = position;
        double shannonPredictedMT = a + b * ID;
        double origPredictedMT = System.Math.Abs(a + b * Mathf.Log((2 * A)/W, 2));
        LogTrial(currentTrial + 1 , A, W, trueMT, ID, TP, origPredictedMT, shannonPredictedMT);

        //Debug.Log(string.Format("A = {0}, W = {1}, ID = {2}", A, W, Mathf.Log((2 * A)/W, 2)));
        //b = (MT - a)/ID;
        
        Debug.Log(string.Format("Original Predicted MT = {0}, Shannon Predicted MT = {1}, Actual = {2}!", A, shannonPredictedMT, trueMT));
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

    

    
public void LogTrial(int trialNumber, 
                                float A,
                                float W, 
                                double trueMT, 
                                double ID,
                                double TP,
                                double origPredictedMT,
                                double shannonPredictedMT)
                                {
    string row = $"{trialNumber},{A},{W},{trueMT},{ID},{TP},{origPredictedMT},{shannonPredictedMT}";
    
    csvRows.Add(row);
                                }

public void saveCSV(){
    string header = "Trial,A,W,MT,ID,TP,OriginalPredictedMT,ShannonPredictedMT";
    string path = Application.dataPath + "/CsvPrint/Decomposers_Outputfile.csv";

    using (StreamWriter writer = new StreamWriter(path)){
        writer.WriteLine(header);
        foreach (string row in csvRows){
            writer.WriteLine(row);
        }
    }

    Debug.Log("CSV saved to:" + path);
}

public string GetInteractionMethod()
    {
        if (currentTrial < 3) 
            return "Controller Ray";
        else
            return "Hand Pinch";
    }
} 
