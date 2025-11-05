using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("능력치")]
    public float Speed;
    public float Health = 100f;


    [Header("시작위치")]
    private Vector3 _originPosition;


    void Update()
    {
        Vector2 direction = Vector2.down;

        transform.Translate(direction * (Speed * Time.deltaTime));
    }


    private void OnTriggerEnter2D(Collider2D other)  // 충돌이 시작될 때
    {
        Debug.Log("충돌이 시작되었습니다.");

        // 적은 플레이어만 죽여야 한다. 총알은 안 죽인다. ㄱ
        if (!other.gameObject.CompareTag("Player")) return;  // 플레이어'만' 충돌 처리

        Destroy(this.gameObject);  // 스크립트 부여된 오브젝트 삭제(사망)  => 적
        Destroy(other.gameObject); // 부딪힌 다른 오브젝트 삭제(사망)  => 플레이어, 총알
    }

    private void OnTriggerStay2D(Collider2D other)  // 충돌이 진행되는 동안
    {
        Debug.Log("충돌중...");
    }

    private void OnTriggerExit2D(Collider2D other)  // 충돌이 끝날 때
    {
        Debug.Log("충돌이 끝났습니다.");
    }
}
