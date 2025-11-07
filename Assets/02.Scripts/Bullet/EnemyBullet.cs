using UnityEngine;
public enum EEnemyBullettype
{
    EnemyBullet,
    EnemyBigBullet
}

public class EnemyBullet : MonoBehaviour
{
    [Header("총알 프리팹")]
    public EBullettype Type;


    [Header("이동 속도")]
    public float StartSpeed;
    public float EndSpeed;
    private float _speed;
    public float Duration;


    [Header("공격력")]
    public float Damage;


    private void Start()
    {
        _speed = StartSpeed;
    }

    void Update()
    {
        
    }

    private void EnemyMainBullet()
    {
        _speed = 3f;
        Damage = 20f;

    }

    private void EnemyBigBullet()
    {
        _speed = 2.5f;
        Damage = 60f;

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Player Player = other.gameObject.GetComponent<Player>();

        Player.Hit(Damage);
        Debug.Log($"{Damage} 대미지를 받았다!");

        Destroy(gameObject);
    }
}
