using UnityEngine;

public class TrialRunner : MonoBehaviour
{
    public TrialLoader loader;
    public GameObject targetPrefab;

    private int index = 0;

    void Start()
    {
        SpawnNext();
    }

    void SpawnNext()
    {
        if (index >= loader.trials.Count)
        {
            Debug.Log("Experiment complete");
            return;
        }

        TrialData t = loader.trials[index];

        Vector3 pos = GetPosition(t);

        GameObject obj = Instantiate(targetPrefab, pos, Quaternion.identity);
        obj.transform.localScale = Vector3.one * t.size;

        Debug.Log("Trial " + t.trial + " | Method: " + t.method);

        index++;
    }

    Vector3 GetPosition(TrialData t)
    {
        float x = 0;
        float y = 1.2f;
        float z = t.distance;

        if (t.direction == "Left") x = -0.3f;
        if (t.direction == "Right") x = 0.3f;
        if (t.direction == "Up") y = 1.5f;

        return new Vector3(x, y, z);
    }
}
