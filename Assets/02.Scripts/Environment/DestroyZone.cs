using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            other.gameObject.SetActive(false);
            return;
        }

        Destroy(other.gameObject);
    }
}
