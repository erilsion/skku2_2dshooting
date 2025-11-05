using UnityEngine;

public class MiniBullet2 : MonoBehaviour
{
    [Header("이동 속도")]
    public float StartSpeed = 0.2f;
    public float EndSpeed = 4f;
    private float _Speed;
    public float Duration = 2f;


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
        if (other.gameObject.CompareTag("Enemy") == false) return;

        Enemy enemy = other.gameObject.GetComponent<Enemy>();

        enemy.Hit(Damage);

        Destroy(gameObject);
    }
}
