using UnityEngine;

public class Thorn : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameManager.Instance.StageManager.OnPlayerDeathEvent?.Invoke();
    }
}
