using UnityEngine;

public class GrantDash : MonoBehaviour
{
    public PlayerLocomotion player;
    public int coin_req = 10;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && CoinManager.Instance.coinCount >= coin_req)
        {
            player.hasDash = true;
            Destroy(gameObject);
        }
    }
}
