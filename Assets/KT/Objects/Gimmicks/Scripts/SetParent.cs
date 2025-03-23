using UnityEngine;

public class SetParent : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.parent == null)
        {
            collider.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.transform.parent == transform)
        {
            collider.transform.SetParent(null);
        }
    }
}
