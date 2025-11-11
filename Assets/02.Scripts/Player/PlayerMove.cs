using UnityEngine;

// 플레이어 이동
public class PlayerMove : MonoBehaviour
{
    // 필요 속성
    [Header("능력치")]
    private float _speed = 3f;

    [Header("이동 제한 범위")]
    public float MinX = -2.5f;
    public float MaxX = 2.5f;
    public float MinY = -5f;
    public float MaxY = -2f;

    [Header("시작위치")]
    private Vector3 _originPosition;

    [Header("적 위치")]
    private GameObject _enemyObject;

    [Header("스피드 증감")]
    public float speedIncrease = 1f;
    public float speedDecrease = -1f;
    public KeyCode GoBack = KeyCode.R;

    [Header("자동 / 수동 이동")]
    private KeyCode AutoMove = KeyCode.Keypad1;
    private KeyCode AutoMove2 = KeyCode.Alpha1;
    private KeyCode NotAutoMove = KeyCode.Keypad2;
    private KeyCode NotAutoMove2 = KeyCode.Alpha2;
    private bool isAutoMove = true;


    private void Start()
    {
        _originPosition = transform.position;
    }


    private void Update()
    {
        switch (Input.GetKeyDown(AutoMove) || Input.GetKeyDown(AutoMove2))
        {
            case true:
                isAutoMove = true;
                break;
            case false when Input.GetKeyDown(NotAutoMove) || Input.GetKeyDown(NotAutoMove2):
                isAutoMove = false;
                break;
            case false:
                break;
        }

        if (isAutoMove == true)
        {
            AutoMoveOn();
        }
        else if (isAutoMove == false)
        {
            MoveOn();
        }


        if (Input.GetKey(KeyCode.LeftShift))
        {
            _speed = 6f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _speed = 3f;
        }

        if (Input.GetKey(GoBack))
        {
            TranslateToOrigin();
            return;
        }
    }

    public void MoveOn()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");


        Vector2 direction = new Vector2(h, v);

        direction.Normalize();



        Vector2 position = transform.position;
        Vector2 newPosition = position + direction * _speed * Time.deltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, MinX, MaxX);
        newPosition.y = Mathf.Clamp(newPosition.y, MinY, MaxY);

        transform.position = newPosition;
    }

    public void AutoMoveOn()
    {
        _enemyObject = GameObject.FindWithTag("Enemy");
        if (_enemyObject == null) return;

        Vector2 enemyPosition = _enemyObject.transform.position;

        Vector2 direction = (enemyPosition - (Vector2)transform.position).normalized;

        transform.Translate(direction * (_speed * Time.deltaTime));

        Vector2 newPosition = transform.position;

        newPosition.x = Mathf.Clamp(newPosition.x, MinX, MaxX);
        newPosition.y = Mathf.Clamp(newPosition.y, MinY, MaxY);

        transform.position = newPosition;
    }

    private void TranslateToOrigin()
    {
        Vector2 direction = _originPosition - transform.position;
        transform.Translate(translation: direction * _speed * Time.deltaTime);
    }

    public void SpeedUp(float value)
    {
        _speed += value;
    }
}
