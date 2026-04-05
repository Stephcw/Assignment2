using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TrialLoader : MonoBehaviour
{
    public List<TrialData> trials = new List<TrialData>();

    void Awake()
    {
        LoadCSV();
    }

    void LoadCSV()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "trials.csv");

        string[] lines = File.ReadAllLines(path);

        for (int i = 1; i < lines.Length; i++) // skip header
        {
            string[] values = lines[i].Split(',');

            TrialData t = new TrialData();
            t.trial = int.Parse(values[0]);
            t.method = values[1];
            t.distance = float.Parse(values[2]);
            t.size = float.Parse(values[3]);
            t.direction = values[4];
            t.repetition = int.Parse(values[5]);

            trials.Add(t);
        }

        Debug.Log("Loaded " + trials.Count + " trials");
    }
}

[System.Serializable]
public class TrialData
{
    public int trial;
    public string method;
    public float distance;
    public float size;
    public string direction;
    public int repetition;
}
