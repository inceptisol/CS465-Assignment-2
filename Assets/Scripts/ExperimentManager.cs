using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class ExperimentManager : MonoBehaviour
{
    [Header("HUD Text References")]
    public TextMeshProUGUI trialNumberText;
    public TextMeshProUGUI interactionMethodText;
    public TextMeshProUGUI trialResultText;
    public TextMeshProUGUI finalMessageText;

    [Header("Trial List")]
    public List<TrialConfig> trials;

    private int          _currentIndex = 0;
    private List<string> _csvRows      = new List<string>();
    private string       _csvPath;

    void Start()
    {
        string timestamp = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");
        _csvPath = Path.Combine(Application.persistentDataPath,
                                $"experiment_{timestamp}.csv");

        _csvRows.Add("TrialNumber,InteractionMethod,Result,Timestamp");

        finalMessageText.gameObject.SetActive(false);
        trialResultText.gameObject.SetActive(false);

        LoadTrial();
    }

    void LoadTrial()
    {
        if (_currentIndex >= trials.Count)
        {
            EndExperiment();
            return;
        }

        TrialConfig current = trials[_currentIndex];

        trialNumberText.text       = $"Trial {_currentIndex + 1} / {trials.Count}";
        interactionMethodText.text = $"Method: {current.interactionMethod}";
        trialResultText.gameObject.SetActive(false);
    }

    public void SubmitResult(bool hit)
    {
        TrialConfig current = trials[_currentIndex];

        trialResultText.gameObject.SetActive(true);
        trialResultText.text  = hit ? "HIT"  : "MISS";
        trialResultText.color = hit ? Color.green : Color.red;

        string timestamp = System.DateTime.Now.ToString("HH:mm:ss.fff");
        _csvRows.Add($"{_currentIndex + 1}," +
                     $"{current.interactionMethod}," +
                     $"{(hit ? "Hit" : "Miss")}," +
                     $"{timestamp}");

        _currentIndex++;
        StartCoroutine(AdvanceAfterDelay(1.5f));
    }

    IEnumerator AdvanceAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        LoadTrial();
    }

    void EndExperiment()
    {
        File.WriteAllLines(_csvPath, _csvRows);

        finalMessageText.gameObject.SetActive(true);
        finalMessageText.text = $"Experiment complete!\n\nCSV saved to:\n{_csvPath}";

        trialNumberText.gameObject.SetActive(false);
        interactionMethodText.gameObject.SetActive(false);
        trialResultText.gameObject.SetActive(false);

        Debug.Log($"[ExperimentManager] CSV saved to: {_csvPath}");
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) SubmitResult(true);
        if (Input.GetKeyDown(KeyCode.M)) SubmitResult(false);
    }
}