using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class CsvFile : MonoBehaviour{
    
private List<string> csvRows= new List<string>();

public void LogTrial(int trialNumber, 
                                float A,
                                float W, 
                                double MT, 
                                double ID,
                                double TP)
                                {
    string row = $"{trialNumber},{A},{W},{MT},{ID},{TP}";
    csvRows.Add(row);
                                }

public void saveCSV(){
    string header = "Trial,A,W,MT,ID,TP";
    string path = Application.persistentDataPath + "/CsvPrint/Decomposers_Outputfile.csv";

    using (StreamWriter writer = new StreamWriter(path)){
        writer.WriteLine(header);
        foreach (string row in csvRows){
            writer.WriteLine(row);
        }
    }

    Debug.Log("CSV saved to:" + path);
}
}
