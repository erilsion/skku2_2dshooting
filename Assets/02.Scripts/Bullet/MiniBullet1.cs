using UnityEngine;

public class MiniBullet1 : MonoBehaviour
{
    [Header("적 프리팹")]
    public GameObject EnemyPrefab;
    public GameObject EnemyChasingPrefab;

    [Header("이동 속도")]
    public float StartSpeed = 3f;
    public float EndSpeed = 10f;
    private float _Speed;
    public float Duration = 0.8f;


    [Header("공격력")]
    public float Damage = 40f;

    private void Start()
    {
        _Speed = StartSpeed;
    }
    private void Update()
    {
        float Acceleration = (EndSpeed - StartSpeed) / Duration;

        _Speed += Time.deltaTime * Acceleration;


        _Speed = Mathf.Min(_Speed, EndSpeed);


        Vector2 direction = Vector2.up;


        Vector2 position = transform.position;
        Vector2 newPosition = position + direction * _Speed * Time.deltaTime;
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
