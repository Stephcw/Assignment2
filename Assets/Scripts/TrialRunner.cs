using UnityEngine;
using System.IO;

public class TrialRunner : MonoBehaviour
{
    public TrialLoader loader;
    public GameObject targetPrefab;
    public Transform headTransform;   // assign Main Camera here

    [Header("Direction offset in meters")]
    public float lateralOffset = 0.3f;
    public float upwardOffset = 0.3f;

    private int index = 0;
    private GameObject currentTarget;

    private string filePath;
    private float startTime;

    void Start()
    {
        filePath = Path.Combine(Application.dataPath, "results.csv");

        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "Trial,Method,Distance,Size,Direction,Repetition,Time\n");
        }

        Debug.Log("CSV path: " + filePath);
        SpawnNext();
    }

    public void SpawnNext()
    {
        if (loader == null || loader.trials.Count == 0)
        {
            Debug.LogError("No trials loaded.");
            return;
        }

        if (index >= loader.trials.Count)
        {
            Debug.Log("Experiment complete");
            return;
        }

        TrialData t = loader.trials[index];

        Vector3 pos = GetPosition(t);

        currentTarget = Instantiate(targetPrefab, pos, Quaternion.identity);

        // Size in meters: 0.05 = 5 cm, 0.15 = 15 cm
        currentTarget.transform.localScale = Vector3.one * t.size;

        TrialTarget tt = currentTarget.GetComponent<TrialTarget>();
        if (tt != null)
        {
            tt.Setup(this, t);
        }

        startTime = Time.time;

        Debug.Log($"Spawned Trial {t.trial} | Method={t.method} | Distance={t.distance}m | Size={t.size}m | Direction={t.direction}");
    }

    Vector3 GetPosition(TrialData t)
    {
        // flatten forward/right so target appears relative to user on a comfortable plane
        Vector3 forward = headTransform.forward;
        forward.y = 0f;
        forward.Normalize();

        Vector3 right = headTransform.right;
        right.y = 0f;
        right.Normalize();

        // base position at requested distance in front of the headset
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
        Debug.Log("Wrote to CSV " + filePath + ": " + line);

        if (currentTarget != null)
        {
            Destroy(currentTarget);
            currentTarget = null;
        }

        index++;
        SpawnNext();
    }
}