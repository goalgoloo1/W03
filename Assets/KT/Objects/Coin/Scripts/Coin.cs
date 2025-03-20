using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            GameManager.Instance.StageManager.OnGetCoinEvent?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
