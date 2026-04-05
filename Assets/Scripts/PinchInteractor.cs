using UnityEngine;

public class PinchInteractor : MonoBehaviour
{
    public float radius = 0.2f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // simulate pinch
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, radius);

            foreach (Collider c in hits)
            {
                Target t = c.GetComponent<Target>();

                if (t != null)
                {
                    FindObjectOfType<ExperimentManager>().TargetSelected();
                }
            }
        }
    }
}
