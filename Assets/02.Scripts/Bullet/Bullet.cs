using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("이동 속도")]
    public float StartSpeed = 1f;
    public float EndSpeed = 7f;
    private float _Speed;
    public float Duration = 1.2f;   // 마지막 속도에 도달하는 시간(초)

    private void Start()
    {
        _Speed = StartSpeed;
    }
    private void Update()
    {
        // 목표: Duration 안에 EndSpeed에 도달하고 싶다.

        float Acceleration = (EndSpeed - StartSpeed) / Duration;  // 가속도

        _Speed += Time.deltaTime * Acceleration;   // 초당 + 1 * 가속도
        

        _Speed = Mathf.Min(_Speed, EndSpeed);  // 아래 주석과 같은 뜻이다.
        // if (_Speed > EndSpeed)
        // {
        //     _Speed = EndSpeed;
        // }


        // 방향을 구한다.
        Vector2 direction = Vector2.up;
        // Vector2 direction = new Vector2(0, 1); 같은 말


        // 방향에 따라 이동한다.
        // 새로운 위치 = 현재 위치 + 방향 * 속력 * 시간
        Vector2 position = transform.position;
        Vector2 newPosition = position + direction * _Speed * Time.deltaTime;
        transform.position = newPosition;
    }
}
