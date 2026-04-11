// ExperimentHUD.cs
using UnityEngine;
using TMPro;
using System.Collections;

public class ExperimentHUD : MonoBehaviour
{
    private static ExperimentHUD _instance;
    public static ExperimentHUD instance {
        get {
            if (_instance == null)
                _instance = FindObjectOfType<ExperimentHUD>();
            return _instance;
        }
    }

    private TextMeshProUGUI trialLabel;
    private TextMeshProUGUI methodLabel;
    private TextMeshProUGUI resultLabel;
    private TextMeshProUGUI finalLabel;

    void Awake() {
        _instance = this;
        CreateHUD();
    }

    void Start() {
        UpdateHUD();
    }

    private void CreateHUD() {
        GameObject canvasGO = new GameObject("ExperimentHUDCanvas");
        canvasGO.transform.SetParent(Camera.main.transform);

        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;

        canvasGO.transform.localPosition = new Vector3(0f, -0.25f, 1.5f);
        canvasGO.transform.localRotation = Quaternion.identity;
        canvasGO.transform.localScale    = new Vector3(0.001f, 0.001f, 0.001f);

        RectTransform canvasRect = canvasGO.GetComponent<RectTransform>();
        canvasRect.sizeDelta = new Vector2(600, 400);

        GameObject panelGO = new GameObject("Panel");
        panelGO.transform.SetParent(canvasGO.transform, false);
        UnityEngine.UI.Image panelImage = panelGO.AddComponent<UnityEngine.UI.Image>();
        panelImage.color = new Color(0, 0, 0, 0.75f);
        RectTransform panelRect = panelGO.GetComponent<RectTransform>();
        panelRect.anchorMin = Vector2.zero;
        panelRect.anchorMax = Vector2.one;
        panelRect.sizeDelta = Vector2.zero;

        trialLabel  = CreateLabel(canvasGO, "TrialLabel",  new Vector2(0,  120), 28);
        methodLabel = CreateLabel(canvasGO, "MethodLabel", new Vector2(0,   60), 24);
        resultLabel = CreateLabel(canvasGO, "ResultLabel", new Vector2(0,    0), 48);
        finalLabel  = CreateLabel(canvasGO, "FinalLabel",  new Vector2(0,  -60), 26);
    }

    private TextMeshProUGUI CreateLabel(GameObject parent, string name,
                                        Vector2 anchoredPosition, float fontSize) {
        GameObject labelGO = new GameObject(name);
        labelGO.transform.SetParent(parent.transform, false);

        TextMeshProUGUI tmp = labelGO.AddComponent<TextMeshProUGUI>();
        tmp.fontSize  = fontSize;
        tmp.color     = Color.white;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.text      = "";

        RectTransform rect = labelGO.GetComponent<RectTransform>();
        rect.sizeDelta        = new Vector2(580, 80);
        rect.anchoredPosition = anchoredPosition;

        return tmp;
    }

    public void ShowResult(bool isHit) {
        resultLabel.text  = isHit ? "HIT" : "MISS";
        resultLabel.color = isHit ? new Color(0.18f, 0.80f, 0.44f)
                                  : new Color(0.90f, 0.22f, 0.22f);
        StartCoroutine(ClearResultAfterDelay());
    }

    public void ShowFinalMessage(string message) {
        resultLabel.text  = "";
        finalLabel.text   = message;
        finalLabel.color  = new Color(1.0f, 0.85f, 0.0f);
    }

    public void UpdateHUD() {
        trialLabel.text  = $"Trial: {MyManager.currentTrial} / 6  Hits: {MyManager.totalHits}";
        methodLabel.text = $"Method: {MyManager.instance.GetInteractionMethod()}";
    }

    private IEnumerator ClearResultAfterDelay() {
        yield return new WaitForSeconds(2.0f);
        resultLabel.text = "";
    }
}