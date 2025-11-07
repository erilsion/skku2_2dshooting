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


    void Start()
    {
        
    }


    void Update()
    {

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("PlayerBullet") == false) return;


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
            Bullet _playerAttackSpeed = other.gameObject.GetComponent<Bullet>();
            _playerAttackSpeed.AttackSpeedUp(AttackSpeedUpAmount);
        }

        Destroy(gameObject);
    }
}
