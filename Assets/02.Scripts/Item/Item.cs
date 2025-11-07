using Unity.VisualScripting;
using UnityEngine;
public enum EItemType
{
    SpeedItem,
    HealthItem,
    AttackSpeedItem
}


public class Item : MonoBehaviour
{
    [Header("아이템 타입")]
    public EItemType Type;
    public float value;

    [Header("플레이어 위치")]
    private GameObject _playerObject;

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

        Apply(other);

        Destroy(gameObject);
    }
    private void Apply(Collider2D other)
    {
        switch (Type)
        {
            case EItemType.SpeedItem:
                {
                    PlayerMove _playerMove = other.gameObject.GetComponent<PlayerMove>();
                    _playerMove.SpeedUp(value);
                    break;
                }
            case EItemType.HealthItem:
                {
                    Player _playerHealth = other.gameObject.GetComponent<Player>();
                    _playerHealth.HealthUp(value);
                    break;
                }
            case EItemType.AttackSpeedItem:
                {
                    PlayerFire _playerAttackSpeed = other.GetComponent<PlayerFire>();
                    _playerAttackSpeed.AttackSpeedUp(value);
                    break;
                }
        }
    }
}
