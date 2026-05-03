using UnityEngine;

public class CoinCollected : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CoinManager.Instance.AddCoin();
            Destroy(gameObject);
        }
    }
}
