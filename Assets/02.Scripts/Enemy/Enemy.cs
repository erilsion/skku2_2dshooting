using TMPro;
using UnityEngine;

// Enum(열거형): 기억하기 어려운 상수들을 기억하기 쉬운 이름 하나로 묶어 그룹처럼 관리하는 표현 방식
// 추후 사용할 때 정의되지 않은 열거형(오타 등) 이름이 나오면 빨간색으로 뜬다.

public enum EEnemytype  // 열거형의 이름은 앞에 E를 하나 더 붙이는 게 관례 (다른 이름과 헷갈리지 않도록)
{
    Directional,
    Trace,
    Boomerang
}


public class Enemy : MonoBehaviour
{
    [Header("적 타입")]
    public EEnemytype Type;


    [Header("능력치")]
    public float Speed;
    private float _health;
    public float Damage = 1f;


    [Header("시작 위치")]
    private Vector3 _originPosition;
    private Vector2 BoomerangLeft = new Vector2 (1f, 2f);
    private Vector2 BoomerangRight = new Vector2(-1f, 2f);


    [Header("플레이어 위치")]
    private GameObject _playerObject;


    private void Start()
    {
        // 캐싱: 자주 쓰는 데이터를 미리 가까운 곳에 저장해두고 참조하는 것
        _playerObject = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if(Type == EEnemytype.Directional)
        {
            MoveDirectional();
        }
        if (Type == EEnemytype.Trace)
        {
            MoveTrace();
        }
        if (Type == EEnemytype.Boomerang)
        {
            MoveBoomerang();
        }

        // 0. 타입에 따라 동작이 다르다  ->  함수로 쪼갠다.
        // 1. 함수가 너무 많아지는 거 같다   ->  클래스로 쪼개는 게 좋다.
        // 2. 쪼갠 후에 보니까 똑같은 기능 / 속성이 있다  ->  상속
        // 3. 상속을 하자니 책임이 너무 크다  ->  조합(컴포넌트 패턴)
    }


    private void MoveDirectional()
    {
        Speed = 3f;
        _health = 100f;

        Vector2 direction = Vector2.down;
        transform.Translate(direction * (Speed * Time.deltaTime));
    }


    private void MoveTrace()
    {
        Speed = 2f;
        _health = 100f;

        if (_playerObject == null) return;

        Vector2 playerPosition = _playerObject.transform.position;

        Vector2 direction = playerPosition - (Vector2)transform.position;
        direction = direction.normalized;

        transform.Translate(direction * (Speed * Time.deltaTime));
    }

    private void MoveBoomerang()
    {
        Speed = 4f;
        _health = 60f;

        BoomerangEnter();
    }


    public void Hit(float damage)
    {
        _health -= damage;

        if (_health <= 0f)
        {
            Destroy(this.gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        Player Player = other.gameObject.GetComponent<Player>();

        Player.Hit(Damage);

        Destroy(gameObject);
    }

    private void BoomerangEnter()
    {
        Vector2 target;

        if (_originPosition.x < 0)
        {
            target = (Vector2)_originPosition + BoomerangLeft;
        }
        else
        {
            target = (Vector2)_originPosition + BoomerangRight;
        }

        transform.position = Vector2.MoveTowards(transform.position,target, Speed * Time.deltaTime);
    }
}
