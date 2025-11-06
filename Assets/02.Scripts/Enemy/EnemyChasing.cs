using Unity.VisualScripting;
using UnityEngine;

public class EnemyChasing : MonoBehaviour
{
    // 목표: 플레이어를 쫓아가는 적을 만들고 싶다.
    [Header("능력치")]
    public float Speed = 2f;
    private float _health = 100f;
    public float Damage = 1f;


    [Header("시작위치")]
    private Vector3 _originPosition;


    private void Update()
    {
        // 1, 플레이어의 위치를 구한다.
        GameObject PlayerObject = GameObject.FindWithTag ("Player");

        // 2. 위치에 따라 방향을 구한다.
        Vector2 direction = _originPosition + PlayerObject.transform.position;
        direction = direction.normalized;

        // 3. 방향에 맞게 이동한다.
        transform.Translate(direction * (Speed * Time.deltaTime));
    }
    public void Hit(float damage)
    {
        _health -= damage;

        if (_health <= 0f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        Player Player = other.gameObject.GetComponent<Player>();

        Player.Hit(Damage);

        Destroy(gameObject);
    }
}
