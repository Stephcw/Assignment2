using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

public class TrialLoader : MonoBehaviour
{
    public List<TrialData> trials = new List<TrialData>();

    void Awake()
    {
        LoadTrials();
    }

    void LoadTrials()
    {
        TextAsset csvFile = Resources.Load<TextAsset>("trials");

        if (csvFile == null)
        {
            Debug.LogError("Could not find trials.csv in Assets/Resources/");
            return;
        }

        string[] lines = csvFile.text.Split('\n');

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i].Trim();

            if (string.IsNullOrEmpty(line))
                continue;

            string[] cols = line.Split(',');

            TrialData t = new TrialData();
            t.trial = int.Parse(cols[0]);
            t.method = cols[1];
            t.distance = float.Parse(cols[2], CultureInfo.InvariantCulture);
            t.size = float.Parse(cols[3], CultureInfo.InvariantCulture);
            t.direction = cols[4];
            t.repetition = int.Parse(cols[5]);

            trials.Add(t);
        }

        Debug.Log("Loaded " + trials.Count + " trials.");
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
