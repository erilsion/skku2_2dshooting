using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("적 타입")]
    public int Type;


    [Header("능력치")]
    public float Speed = 3;
    private float _health = 100f;
    public float Damage = 1f;


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
        if(Type == 1)
        {
            MoveDirectional();
        }
        if (Type == 2)
        {
            MoveTrace();
        }
    }


    private void MoveDirectional()
    {
        Vector2 direction = Vector2.down;
        transform.Translate(direction * (Speed * Time.deltaTime));
    }


    private void MoveTrace()
    {
        if (_playerObject == null) return;

        Vector2 playerPosition = _playerObject.transform.position;

        Vector2 direction = playerPosition - (Vector2)transform.position;
        direction = direction.normalized;

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
