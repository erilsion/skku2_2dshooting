using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("능력치")]
    public float Speed = 3;
    private float _health = 100f;
    public float Damage = 1f;


    [Header("적 프리팹")]
    public GameObject EnemyPrefab;
    public GameObject EnemyTracePrefab;


    [Header("시작 위치")]
    private Vector3 _originPosition;


    [Header("플레이어 위치")]
    private GameObject _playerObject;

    private void Start()
    {
        // 캐싱: 자주 쓰는 데이터를 미리 가까운 곳에 저장해두고 참조하는 것
        _playerObject = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        MoveDirectional();
    }

    private void MoveDirectional()
    {
        Vector2 direction = Vector2.down;
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
