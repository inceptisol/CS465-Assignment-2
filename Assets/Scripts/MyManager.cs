// MyManager.cs
using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class MyManager : MonoBehaviour
{
    private static MyManager _instance;
    public static MyManager instance {
        get {
            if (_instance == null)
                _instance = FindObjectOfType<MyManager>();
            return _instance;
        }
    }

    public static int currentTrial = 0;
    public static int totalHits = 0;
    public static double time = 0;
    public static Vector3 prevPosition = new Vector3(0, 0, 0);
    public static double a = 0.2;
    public static double b = 7.0532;

    static float[] trial1 = new[] { 0.35f, 10.0f, -1.0f };
    static float[] trial2 = new[] { 0.35f, 20.0f,  1.0f };
    public static float[][] trials = new[] { trial1, trial2 };

    private List<string> csvRows = new List<string>();

    public float[] getTrial() {
        return trials[currentTrial % trials.Length];
    }

    public string GetInteractionMethod() {
        if (currentTrial < 3)
            return "Controller Ray";
        else
            return "Hand Pinch";
    }

    public void RegisterHit() {
        totalHits += 1;
        incrementTrial();
    }

    public void RegisterMiss() {
        incrementTrial();
    }

    public void incrementTrial() {
        currentTrial += 1;
    }

    public void logData(Vector3 position, float W) {
        double newTime = Time.timeAsDouble;
        double trueMT = newTime - time;
        time = newTime;
        float A = Mathf.Abs(Vector3.Distance(prevPosition, position));
        double ID = Mathf.Log((A / W) + 1, 2);
        double TP = ID / trueMT;
        prevPosition = position;
        double shannonPredictedMT = a + b * ID;
        double origPredictedMT = System.Math.Abs(a + b * Mathf.Log((2 * A) / W, 2));

        string row = $"{currentTrial},{A},{W},{trueMT},{ID},{TP},{origPredictedMT},{shannonPredictedMT}";
        csvRows.Add(row);
    }

    public void saveCSV() {
        string path = Application.dataPath + "/CsvPrint/Decomposers_Outputfile.csv";
        using (StreamWriter writer = new StreamWriter(path)) {
            writer.WriteLine("Trial,A,W,MT,ID,TP,OriginalPredictedMT,ShannonPredictedMT");
            foreach (string row in csvRows)
                writer.WriteLine(row);
        }
        Debug.Log("CSV saved to: " + path);
    }
}