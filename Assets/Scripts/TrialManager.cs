using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class TrialManager : MonoBehaviour
{
    public GameObject targetPrefab;

    public TextMeshProUGUI trialText;
    public TextMeshProUGUI methodText;
    public TextMeshProUGUI resultText;

    private int trialIndex = 0;

    private float startTime;

    private List<TrialData> trials = new List<TrialData>();

    void Start()
    {
        GenerateTrials();
        SpawnNextTarget();
    }

    void GenerateTrials()
    {
        string[] methods = { "Ray", "Pinch" };
        float[] distances = { 0.5f, 1.0f };
        float[] sizes = { 0.05f, 0.15f };

        foreach (string method in methods)
        {
            foreach (float d in distances)
            {
                foreach (float s in sizes)
                {
                    trials.Add(new TrialData(method, d, s));
                }
            }
        }
    }

    void SpawnNextTarget()
    {
        if (trialIndex >= trials.Count)
        {
            SaveCSV();
            return;
        }

        TrialData t = trials[trialIndex];

        Vector3 pos = new Vector3(Random.Range(-0.5f, 0.5f), 1.2f, t.distance);

        GameObject target = Instantiate(targetPrefab, pos, Quaternion.identity);
        target.transform.localScale = Vector3.one * t.size;

        trialText.text = "Trial: " + (trialIndex + 1);
        methodText.text = "Method: " + t.method;

        startTime = Time.time;
    }

    public void RegisterHit(bool hit)
    {
        float mt = Time.time - startTime;

        trials[trialIndex].MT = mt;
        trials[trialIndex].hit = hit;

        resultText.text = hit ? "Hit!" : "Miss!";

        trialIndex++;
        Invoke(nameof(SpawnNextTarget), 1f);
    }

    void SaveCSV()
    {
        string path = Application.dataPath + "/Output.csv";
        StreamWriter writer = new StreamWriter(path);

        writer.WriteLine("Trial,Method,A,W,MT,Hit");

        for (int i = 0; i < trials.Count; i++)
        {
            var t = trials[i];
            writer.WriteLine($"{i},{t.method},{t.distance},{t.size},{t.MT},{t.hit}");
        }

        writer.Close();

        Debug.Log("Saved to: " + path);
    }
}

[System.Serializable]
public class TrialData
{
    public string method;
    public float distance;
    public float size;
    public float MT;
    public bool hit;

    public TrialData(string m, float d, float s)
    {
        method = m;
        distance = d;
        size = s;
    }
}
