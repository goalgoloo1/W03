using UnityEngine;

public class SetParent : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.parent == null)
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.parent == transform)
        {
            collision.transform.SetParent(null);
        }
    }
}
