using UnityEngine;

// 플레이어 이동
public class PlayerMove : MonoBehaviour
{
    // 필요 속성
    [Header("능력치")]
    public float Speed = 3f;


    [Header("이동 제한 범위")]
    public float MinX = -2.5f;
    public float MaxX = 2.5f;
    public float MinY = -5f;
    public float MaxY = -2f;


    [Header("시작위치")]
    private Vector3 _originPosition;

    private void Start()
    {
        _originPosition = transform.position;
    }


    [Header("스피드 증감")]
    public float speedIncrease = 1f;
    public float speedDecrease = -1f;
    public KeyCode speedUpKey = KeyCode.Q;
    public KeyCode speedDownKey = KeyCode.E;
    public KeyCode GoBack = KeyCode.R;
    public float maxSpeed = 10f;
    public float minSpeed = 1f;



    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Speed = 6f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Speed = 3f;
        }

        if (Input.GetKey(GoBack))
        {
            TranslateToOrigin();
            return;
        }


        if (Input.GetKeyDown(speedUpKey))
        {
            Speed += speedIncrease;
            if (Speed > maxSpeed)
            {
                Speed = maxSpeed;
            }
        }
        if (Input.GetKeyDown(speedDownKey))
        {
            Speed += speedDecrease;
            if (Speed < minSpeed)
            {
                Speed = minSpeed;
            }
        }

       
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");


        Vector2 direction = new Vector2(h, v);

        direction.Normalize();



        Vector2 position = transform.position;
        Vector2 newPosition = position + direction * Speed * Time.deltaTime;


        if (newPosition.x > MaxX)
        {
            newPosition.x = MinX;
        }
        else if (newPosition.x < MinX)
        {
            newPosition.x = MaxX;
        }
        if (newPosition.y > MaxY)
        {
            newPosition.y = MaxY;
        }
        else if (newPosition.y < MinY)
        {
            newPosition.y = MinY;
        }

        transform.position = newPosition;

    }

    private void TranslateToOrigin()
    {
        Vector2 direction = _originPosition - transform.position;
        transform.Translate(translation: direction * Speed * Time.deltaTime);
    }
}

