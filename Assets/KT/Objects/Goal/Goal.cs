using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            //RankType rank = GameManager.StageManager.Rank;
            //float timer = GameManager.StageManager.Timer;
            //int coinCount = GameManager.StageManager.CoinCounter;

            //GameManager.StageManager.OnStageClearEvent?.Invoke();
        }
    }
}
