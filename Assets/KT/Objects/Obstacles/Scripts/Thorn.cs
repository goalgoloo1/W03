using UnityEngine;

public class Thorn : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            GameManager.Instance.StageManager.OnPlayerDeathEvent?.Invoke();
        }
    }
}
