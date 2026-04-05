using UnityEngine;

public class RayInteractor : MonoBehaviour
{
    public float maxDistance = 10f;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red);

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            Target t = hit.collider.GetComponent<Target>();

            if (t != null)
            {
                t.Hover();

                if (Input.GetMouseButtonDown(0)) // simulate click
                {
                    FindObjectOfType<ExperimentManager>().TargetSelected();
                }
            }
        }
    }
}
