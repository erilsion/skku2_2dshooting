using UnityEditor;
using UnityEngine;


public class PlayerMove : MonoBehaviour
{
    [Header("조이스틱")]
    public Joystick Joystick;

    [Header("애니메이터")]
    private Animator _animator;

    [Header("능력치")]
    private static float _speed = 3f;
    public static float Speed => _speed;

    [Header("이동 제한 범위")]
    public float MinX = -2.5f;
    public float MaxX = 2.5f;
    public float MinY = -5f;
    public float MaxY = -2f;

    [Header("시작위치")]
    private Vector3 _originPosition;

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
        _animator = gameObject.GetComponent<Animator>();
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
        // float h = Input.GetAxis("Horizontal");
        // float v = Input.GetAxis("Vertical");
        float h = Joystick.Horizontal;
        float v = Joystick.Vertical;


        Vector2 direction = new Vector2(h, v);
        direction.Normalize();

        _animator.SetInteger("x", (int)direction.x);


        Vector2 position = transform.position;
        Vector2 newPosition = position + direction * _speed * Time.deltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, MinX, MaxX);
        newPosition.y = Mathf.Clamp(newPosition.y, MinY, MaxY);

        transform.position = newPosition;
    }

    public void AutoMoveOn()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies == null || enemies.Length == 0)
        {
            _animator.Play("Idle");
            return;
        }

        // 가까운 적 찾는 로직
        GameObject closestEnemy = enemies[0];
        float closestDistance = Vector2.Distance(transform.position, enemies[0].transform.position);
        for (int i = 1; i < enemies.Length; i++)
        {
            float distance = Vector2.Distance(transform.position, enemies[i].transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemies[i];
            }
        }

        Vector2 enemyPosition = closestEnemy.transform.position;
        Vector2 direction = Vector2.zero;

        if (enemyPosition.x < transform.position.x)
        {
            direction.x = -1;
            _animator.Play("Left");
        }
        else
        {
            direction.x = 1;
            _animator.Play("Right");
        }

        transform.Translate(direction * _speed * Time.deltaTime);
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
