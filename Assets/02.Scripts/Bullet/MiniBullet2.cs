using UnityEngine;

public class MiniBullet2 : MonoBehaviour
{
    [Header("이동 속도")]
    public float StartSpeed = 0.2f;
    public float EndSpeed = 4f;
    private float _Speed;
    public float Duration = 2f;

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

        GameObject enemyGameObject = other.gameObject;        // 적 오브젝트 지정
        Enemy enemy = enemyGameObject.GetComponent<Enemy>();  // Enemy 스크립트 컴포넌트 가져오기
        // Enemy enemy = other.gameObject.GetComponent<Enemy>(); 로 줄여 쓸 수 있다.

        enemy.Health -= 40f;

        if (enemy.Health > 0f) return;
        Destroy(this.gameObject);
        Destroy(other.gameObject);

    }
}
