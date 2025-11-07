using UnityEngine;
public enum EItemtype
{
    SpeedItem,
    HealthItem,
    AttackSpeedItem
}


public class Item : MonoBehaviour
{
    [Header("아이템 타입")]
    public EItemtype Type;

    [Header("아이템 효과")]
    private float SpeedUpAmount = 2f;
    private float HealthUpAmount = 1f;
    private float AttackSpeedUpAmount = 0.5f;

    [Header("플레이어 위치")]
    private GameObject _playerObject;

    [Header("총알 오브젝트")]
    private GameObject _bulletObject;

    [Header("아이템 이동관련 옵션")]
    private float _speed = 3f;
    private float _timer = 0f;


    void Start()
    {
        _playerObject = GameObject.FindWithTag("Player");
    }


    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer < 2f) return;
        ItemMove();
    }
    private void ItemMove()
    {
        if (_playerObject == null) return;

        Vector2 playerPosition = _playerObject.transform.position;

        Vector2 direction = playerPosition - (Vector2)transform.position;
        direction = direction.normalized;

        transform.Translate(direction * (_speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") == false) return;

        if (Type == EItemtype.SpeedItem)
        {
            PlayerMove _playerMove = other.gameObject.GetComponent<PlayerMove>();
            _playerMove.SpeedUp(SpeedUpAmount);
        }
        else if (Type == EItemtype.HealthItem)
        {
            Player _playerHealth = other.gameObject.GetComponent<Player>();
            _playerHealth.HealthUp(HealthUpAmount);
        }
        else if (Type == EItemtype.AttackSpeedItem)
        {
            _bulletObject = GameObject.FindWithTag("PlayerBullet");
            Bullet _playerAttackSpeed = GetComponent<Bullet>();
            _playerAttackSpeed.AttackSpeedUp(AttackSpeedUpAmount);
        }

        Destroy(gameObject);
    }
}
