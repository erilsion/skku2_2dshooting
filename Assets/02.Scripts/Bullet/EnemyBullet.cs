using UnityEngine;
public enum EEnemyBulletType
{
    EnemyMainBullet,
    EnemyBigBullet
}

public class EnemyBullet : MonoBehaviour
{
    [Header("총알 프리팹")]
    public EEnemyBulletType Type;

    [Header("이동 속도")]
    private float _startSpeed;
    private float _endSpeed;
    private float _speed;
    private float _duration;
    private float _acceleration;

    [Header("공격력")]
    private float _damage;

    [Header("플레이어 위치")]
    private GameObject _playerObject;


    private void Start()
    {
        _playerObject = GameObject.FindWithTag("Player");
        _speed = _startSpeed;
        _acceleration = (_endSpeed - _startSpeed) / _duration;
    }

    void Update()
    {
        if (Type == EEnemyBulletType.EnemyMainBullet)
        {
            EnemyMainBullet();
        }
        if (Type == EEnemyBulletType.EnemyBigBullet)
        {
            EnemyBigBullet();
        }

        EnemyBulletMove();
    }

    private void EnemyMainBullet()
    {
        _startSpeed = 2f;
        _endSpeed = 2f;
        _duration = 1f;
        _damage = 1f;
    }

    private void EnemyBigBullet()
    {
        _startSpeed = 1f;
        _endSpeed = 2f;
        _duration = 1.8f;
        _damage = 2f;
    }

    private void EnemyBulletMove()
    {
        _speed += Time.deltaTime * _acceleration;

        _speed = Mathf.Min(_speed, _endSpeed);

        Vector2 direction = transform.position;

        Vector2 position = transform.position;
        Vector2 newPosition = position + direction * _speed * Time.deltaTime;
        transform.position = newPosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Player Player = other.gameObject.GetComponent<Player>();

        Player.Hit(_damage);
        Debug.Log($"{_damage} 대미지를 받았다!");

        Destroy(gameObject);
    }
}
