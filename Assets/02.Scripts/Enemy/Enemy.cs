using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("능력치")]
    public float Speed;
    private float _Health = 100f;
    public float Damage = 1f;


    [Header("시작위치")]
    private Vector3 _originPosition;


    void Update()
    {
        Vector2 direction = Vector2.down;

        transform.Translate(direction * (Speed * Time.deltaTime));
    }


    public void Hit(float damage)
    {
        _Health -= damage;

        if (_Health <= 0f)
        {
            Destroy(this.gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)  // 충돌이 시작될 때
    {
        // 적은 플레이어만 죽여야 한다. 총알은 안 죽인다.    ㄱ
        if (!other.gameObject.CompareTag("Player")) return;  // 플레이어'만' 충돌 처리

        Player Player = other.gameObject.GetComponent<Player>();

        Player.Hit(Damage);

        Destroy(gameObject);
    }

    // private void OnTriggerStay2D(Collider2D other)  // 충돌이 진행되는 동안
    // private void OnTriggerExit2D(Collider2D other)  // 충돌이 끝날 때

}
