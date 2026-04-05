using UnityEngine;
using System.IO;

public class ExperimentManager : MonoBehaviour
{
    public GameObject spherePrefab;

    private float startTime;
    private int trialIndex = 0;

    private string filePath;

    void Start()
    {
        filePath = Application.dataPath + "/results.csv";
        File.WriteAllText(filePath, "Trial,Time\n");

        SpawnTarget();
    }

    void SpawnTarget()
    {
        Vector3 pos = Camera.main.transform.position + Camera.main.transform.forward * 2f;

        GameObject sphere = Instantiate(spherePrefab, pos, Quaternion.identity);

        startTime = Time.time;
    }

    public void TargetSelected()
    {
        float mt = Time.time - startTime;

        Debug.Log("MT: " + mt);

        File.AppendAllText(filePath, trialIndex + "," + mt + "\n");

        trialIndex++;

        SpawnTarget();
    }
}
