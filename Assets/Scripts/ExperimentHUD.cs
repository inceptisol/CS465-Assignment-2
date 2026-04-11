using UnityEngine;
using TMPro;
using System.Collections;

public class ExperimentHUD : MonoBehaviour
{
    // ── Singleton ──────────────────────────────────────────────────────────
    private static ExperimentHUD _instance;
    public static ExperimentHUD instance {
        get {
            if (_instance == null)
                _instance = FindObjectOfType<ExperimentHUD>();
            return _instance;
        }
    }

    // ── Private TMP references (created in code, not Inspector) ────────────
    private TextMeshProUGUI trialLabel;
    private TextMeshProUGUI methodLabel;
    private TextMeshProUGUI resultLabel;

    // ── Settings ───────────────────────────────────────────────────────────
    private float resultDisplayDuration = 2.0f;
    private float hudDistance           = 1.5f;
    private float hudVerticalOffset     = -0.25f;

    // ── Lifecycle ──────────────────────────────────────────────────────────
    void Awake() {
        _instance = this;
        CreateHUD();
    }

    void Start() {
        UpdateHUD();
    }

    // ── HUD Builder ────────────────────────────────────────────────────────
    private void CreateHUD() {
        // 1. Create a new GameObject to hold the Canvas,
        //    then parent it to the camera so it moves with the player's head
        GameObject canvasGO = new GameObject("ExperimentHUDCanvas");
        canvasGO.transform.SetParent(Camera.main.transform);

        // 2. Add a Canvas component and set it to World Space so it exists
        //    as a flat panel floating in 3D space rather than screen overlay
        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;

        // 3. Position the canvas in front of and slightly below the camera
        canvasGO.transform.localPosition = new Vector3(0, hudVerticalOffset, hudDistance);
        canvasGO.transform.localRotation = Quaternion.identity;
        canvasGO.transform.localScale    = new Vector3(0.001f, 0.001f, 0.001f);

        // 4. Set the canvas rectangle size (in Unity units, scaled down by 0.001 above)
        RectTransform canvasRect = canvasGO.GetComponent<RectTransform>();
        canvasRect.sizeDelta = new Vector2(600, 300);

        // 5. Create a dark semi-transparent background panel behind the text
        GameObject panelGO = new GameObject("Panel");
        panelGO.transform.SetParent(canvasGO.transform, false);
        UnityEngine.UI.Image panelImage = panelGO.AddComponent<UnityEngine.UI.Image>();
        panelImage.color = new Color(0, 0, 0, 0.75f);
        RectTransform panelRect = panelGO.GetComponent<RectTransform>();
        panelRect.anchorMin = Vector2.zero;
        panelRect.anchorMax = Vector2.one;
        panelRect.sizeDelta = Vector2.zero;

        // 6. Create the three TMP labels, stacked vertically inside the panel
        trialLabel  = CreateLabel(canvasGO, "TrialLabel",  new Vector2(0,  60), 28);
        methodLabel = CreateLabel(canvasGO, "MethodLabel", new Vector2(0,  10), 24);
        resultLabel = CreateLabel(canvasGO, "ResultLabel", new Vector2(0, -60), 48);
    }

    // ── Label Factory ──────────────────────────────────────────────────────
    // Creates a single TMP text object, positions it, and returns the component
    private TextMeshProUGUI CreateLabel(GameObject parent, string name,
                                        Vector2 anchoredPosition, float fontSize) {
        // Create the GameObject and parent it to the canvas
        GameObject labelGO = new GameObject(name);
        labelGO.transform.SetParent(parent.transform, false);

        // Add the TextMeshProUGUI component (this is what renders the text)
        TextMeshProUGUI tmp = labelGO.AddComponent<TextMeshProUGUI>();
        tmp.fontSize  = fontSize;
        tmp.color     = Color.white;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.text      = "";

        // Position it on the canvas using its RectTransform
        RectTransform rect = labelGO.GetComponent<RectTransform>();
        rect.sizeDelta       = new Vector2(580, 60);   // width x height of the text box
        rect.anchoredPosition = anchoredPosition;       // where it sits on the canvas

        return tmp;
    }

    // ── Public API ─────────────────────────────────────────────────────────
    public void ShowResult(bool isHit) {
        resultLabel.text  = isHit ? "HIT" : "MISS";
        resultLabel.color = isHit ? new Color(0.18f, 0.80f, 0.44f)  // green
                                  : new Color(0.90f, 0.22f, 0.22f); // red
        StartCoroutine(ClearResultAfterDelay());
    }

    public void UpdateHUD() {
        trialLabel.text  = $"Trial: {MyManager.currentTrial} / 6";
        methodLabel.text = $"Method: {MyManager.instance.GetInteractionMethod()}";
    }

    // ── Coroutine ──────────────────────────────────────────────────────────
    private IEnumerator ClearResultAfterDelay() {
        yield return new WaitForSeconds(resultDisplayDuration);
        resultLabel.text = "";
    }
}