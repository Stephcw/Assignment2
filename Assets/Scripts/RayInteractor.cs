using UnityEngine;

public class RayInteractor : MonoBehaviour
{
    public float rayDistance = 10f;
    public TrialManager manager;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            Target t = hit.collider.GetComponent<Target>();
            if (t != null)
            {
                t.Hover();

                if (Input.GetButtonDown("Fire1"))
                {
                    manager.RegisterHit(true);
                    Destroy(t.gameObject);
                }
            }
        }
    }
}
