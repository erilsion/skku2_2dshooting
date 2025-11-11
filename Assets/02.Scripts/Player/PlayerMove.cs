using UnityEditor;
using UnityEngine;

// 플레이어 이동
public class PlayerMove : MonoBehaviour
{
    private Animator _animator;
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
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");


        Vector2 direction = new Vector2(h, v);
        direction.Normalize();

        // 움직이기 1. Play 메서드를 이용한 강제 적용
        // if (direction.x < 0) _animator.Play("Left");
        // if (direction.x == 0) _animator.Play("Idle");
        // if (direction.x > 0) _animator.Play("Right");
        // 장점은 빠르게 쓰기 편하다.
        // 단점으로 Fade, Timing, State가 무시되고, 남용하기 쉬워서 어디서 어떤 애니메이션을 수정하는지 알 수 없게 된다.


        // 움직이기 2.
        _animator.SetInteger("x", (int)direction.x);
        // 애니메이터에서 조정하기

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

        // 왼쪽이면 왼쪽으로
        if (enemyPosition.x < transform.position.x)
        {
            direction.x = -1;
            _animator.Play("Left");
        }
        // 오른쪽이면 오른쪽으로
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
