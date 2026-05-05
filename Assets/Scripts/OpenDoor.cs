using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public int coin_req = 30;

    public GameObject Door;
    public GameObject target;
    public float speed = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && CoinManager.Instance.coinCount >= coin_req)
        {
            Debug.Log("Door opening");
            Door.SetActive(false);
        }
    }

}
