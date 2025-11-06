using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 역할: 충돌하는 모든 오브젝트를 파괴한다.
        Destroy(other.gameObject);
    }
}
