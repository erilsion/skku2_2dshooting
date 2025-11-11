using Unity.VisualScripting;
using UnityEngine;

public enum EBullettype
{
    MainBullet,
    Bins,
    Bomb
}

public class Bullet : MonoBehaviour
{
    [Header("총알 프리팹")]
    public EBullettype Type;


    [Header("이동 속도")]
    public float StartSpeed;
    public float EndSpeed;
    private float _speed;
    public float Duration;
    private float _acceleration;


    [Header("공격력")]
    public float Damage;


    private void Start()
    {
        _speed = StartSpeed;
        _acceleration = (EndSpeed - StartSpeed) / Duration;
    }

    private void Update()
    {
        if (Type == EBullettype.MainBullet)
        {
            MainBullet();
        }
        if (Type == EBullettype.Bins)
        {
            Bins();
        }
        if (Type == EBullettype.Bomb)
        {
            Bomb();
        }

        BulletMove();
    }

    private void MainBullet()
    {
        StartSpeed = 1f;
        EndSpeed = 7f;
        Duration = 1.2f;
        Damage = 40f;
    }

    private void Bins()
    {
        StartSpeed = 3f;
        EndSpeed = 10f;
        Duration = 0.8f;
        Damage = 10f;
    }

    private void Bomb()
    {
        StartSpeed = 0.6f;
        EndSpeed = 4f;
        Duration = 2f;
        Damage = 30f;
    }

    private void BulletMove()
    {
        _speed += Time.deltaTime * _acceleration;

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

        enemy.Hit(Damage);

        Destroy(gameObject);
    }
}
