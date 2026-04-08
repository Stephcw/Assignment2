using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI trialText;
    public TextMeshProUGUI methodText;
    public TextMeshProUGUI resultText;

    public void UpdateTrial(int trialNum)
    {
        trialText.text = "Trial: " + trialNum;
    }

    public void UpdateMethod(string method)
    {
        methodText.text = "Method: " + method;
    }

    public void ShowResult(bool hit)
    {
        if (hit)
        {
            resultText.text = "HIT";
            resultText.color = Color.green;
        }
        else
        {
            resultText.text = "MISS";
            resultText.color = Color.red;
        }
    }

    public void ShowFinal(string path)
    {
        trialText.text = "Experiment Complete";
        methodText.text = "";
        resultText.text = "Saved to:\n" + path;
        resultText.color = Color.white;
    }

    public void ClearResult()
    {
    resultText.text = "Result: -";
    resultText.color = Color.white;
    }
}
