using UnityEngine;
using System.IO;

public class TrialRunner : MonoBehaviour
{
    public TrialLoader loader;
    public GameObject targetPrefab;
    public Transform headTransform;

    [Header("UI")]
    public UIManager ui;

    [Header("Direction offset in meters")]
    public float lateralOffset = 0.3f;
    public float upwardOffset = 0.3f;

    private int index = 0;
    private GameObject currentTarget;

    private string filePath;
    private float startTime;

    private int missesThisTrial = 0;
    private int totalMisses = 0;
    private bool trialResolved = false;

    private int selectionsThisTrial = 0;

    void Start()
    {
        filePath = Path.Combine(Application.dataPath, "ChickMate_OutputFile.csv");

        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "Trial,Method,Distance,Size,Direction,Repetition,Time,ErrorRate\n");
        }

        Debug.Log("CSV path: " + filePath);

        SpawnNext();
    }

    public void RegisterHit()
    {
        if (trialResolved)
            return;

        trialResolved = true;
        selectionsThisTrial++;

        TrialData t = loader.trials[index];
        float movementTime = Time.time - startTime;

        float errorRate = ((float)missesThisTrial / selectionsThisTrial) * 100f;
        string line = $"{t.trial},{t.method},{t.distance},{t.size},{t.direction},{t.repetition},{movementTime:F3},{errorRate:F2}\n";
        File.AppendAllText(filePath, line);

        Debug.Log($"HIT on trial {t.trial} | MT={movementTime:F3}s | Misses={missesThisTrial}");

        if (ui != null)
            ui.ShowResult(true);

        if (currentTarget != null)
        {
            Destroy(currentTarget);
            currentTarget = null;
        }

        index++;

        Invoke(nameof(SpawnNext), 1.0f);
    }

    public void RegisterMiss()
    {
        if (trialResolved)
            return;

        selectionsThisTrial++;
        missesThisTrial++;
        totalMisses++;

        Debug.Log($"MISS | Misses={missesThisTrial} | Selections={selectionsThisTrial}");

        ui.ShowResult(false);
    }

    public void SpawnNext()
    {
        trialResolved = false;
        missesThisTrial = 0;
        selectionsThisTrial = 0;

        if (loader == null || loader.trials.Count == 0)
        {
            Debug.LogError("No trials loaded.");
            return;
        }

        if (index >= loader.trials.Count)
        {
            Debug.Log("Experiment complete");

            // ✅ FINAL UI MESSAGE
            if (ui != null)
            {
                ui.ShowFinal(filePath);
            }

            return;
        }

        TrialData t = loader.trials[index];

        Vector3 pos = GetPosition(t);

        currentTarget = Instantiate(targetPrefab, pos, Quaternion.identity);
        currentTarget.transform.localScale = Vector3.one * t.size;

        TrialTarget tt = currentTarget.GetComponent<TrialTarget>();
        if (tt != null)
        {
            tt.Setup(this, t);
        }

        if (ui != null)
        {
            ui.UpdateTrial(t.trial);
            ui.UpdateMethod(t.method);
            ui.ClearResult();
        }

        startTime = Time.time;

        Debug.Log($"Spawned Trial {t.trial} | Method={t.method} | Distance={t.distance}m | Size={t.size}m | Direction={t.direction}");
    }

    Vector3 GetPosition(TrialData t)
    {
        Vector3 forward = headTransform.forward;
        forward.y = 0f;
        forward.Normalize();

        Vector3 right = headTransform.right;
        right.y = 0f;
        right.Normalize();

        Vector3 basePos = headTransform.position + forward * t.distance;

        Vector3 offset = Vector3.zero;

        switch (t.direction)
        {
            case "Left":
                offset = -right * lateralOffset;
                break;
            case "Right":
                offset = right * lateralOffset;
                break;
            case "Up":
                offset = Vector3.up * upwardOffset;
                break;
            case "Center":
            default:
                offset = Vector3.zero;
                break;
        }

        return basePos + offset;
    }

    public void CompleteCurrentTrial()
    {
        if (index >= loader.trials.Count)
            return;

        TrialData t = loader.trials[index];
        float movementTime = Time.time - startTime;

        string line = $"{t.trial},{t.method},{t.distance},{t.size},{t.direction},{t.repetition},{movementTime:F3}\n";
        File.AppendAllText(filePath, line);

        Debug.Log($"Completed Trial {t.trial} | MT={movementTime:F3}s");

        // ✅ SHOW HIT RESULT
        if (ui != null)
        {
            ui.ShowResult(true);
        }

        if (currentTarget != null)
        {
            Destroy(currentTarget);
            currentTarget = null;
        }

        index++;

        // small delay so user sees HIT text
        Invoke(nameof(SpawnNext), 1.0f);
    }
}
