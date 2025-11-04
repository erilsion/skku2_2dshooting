using UnityEngine;

public class MiniBullet1 : MonoBehaviour
{
    [Header("이동 속도")]
    public float StartSpeed = 3f;
    public float EndSpeed = 10f;
    private float _Speed;
    public float Duration = 0.8f;

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
}
