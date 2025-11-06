using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("총알 프리팹")]
    public GameObject BulletPrefab;
    public GameObject MiniBullet1Prefab;
    public GameObject MiniBullet2Prefab;


    [Header("이동 속도")]
    public float StartSpeed = 1f;
    public float EndSpeed = 7f;
    private float _speed;
    public float Duration = 1.2f;   // 마지막 속도에 도달하는 시간(초)
    

    [Header("공격력")]
    public float Damage = 60f;


    private void Start()
    {
        _speed = StartSpeed;
    }

    private void Update()
    {
        float Acceleration = (EndSpeed - StartSpeed) / Duration;

        _speed += Time.deltaTime * Acceleration;
        

        _speed = Mathf.Min(_speed, EndSpeed);


        Vector2 direction = Vector2.up;


        Vector2 position = transform.position;
        Vector2 newPosition = position + direction * _speed * Time.deltaTime;
        transform.position = newPosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;


        Enemy enemy = other.GetComponent<Enemy>();
        EnemyTrace enemyChasing = other.GetComponent<EnemyTrace>();

        if (enemy != null)
        {
            enemy.Hit(Damage);
        }

        if (enemyChasing != null)
        {
            enemyChasing.Hit(Damage);
        }

        Destroy(gameObject);
    }
}
