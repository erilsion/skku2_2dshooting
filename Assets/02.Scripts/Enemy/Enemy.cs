using TMPro;
using UnityEngine;


public enum EEnemytype
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

    [Header("적 능력치 초기값")]
    private float _directionalSpeed = 3f;
    private float _directionalHealth = 100f;
    private float _traceSpeed = 2f;
    private float _traceHealth = 100f;
    private float _boomerangSpeed = 4f;
    private float _boomerangHealth = 60f;


    private void Start()
    {
        _playerObject = GameObject.FindWithTag("Player");

        switch (Type)
        {
            case EEnemytype.Directional:
                Speed = _directionalSpeed;
                _health = _directionalHealth;
                break;
            case EEnemytype.Trace:
                Speed = _traceSpeed;
                _health = _traceHealth;
                break;
            case EEnemytype.Boomerang:
                Speed = _boomerangSpeed;
                _health = _boomerangHealth;
                break;
        }
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
            BoomerangRoutine();
        }

    }

    private void MoveDirectional()
    {
        Vector2 direction = Vector2.down;
        transform.Translate(direction * (Speed * Time.deltaTime));
    }

    private void MoveTrace()
    {
        if (_playerObject == null) return;

        Vector2 playerPosition = _playerObject.transform.position;

        Vector2 direction = playerPosition - (Vector2)transform.position;
        direction = direction.normalized;

        transform.Translate(direction * (Speed * Time.deltaTime));
    }

    private void BoomerangRoutine()
    {
        BoomerangEnter();
       // BoomerangAttack();
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
        if (other.gameObject.CompareTag("Player") == false) return;

        Player Player = other.gameObject.GetComponent<Player>();

        Player.Hit(Damage);

        Destroy(gameObject);
    }

    private void BoomerangEnter()
    {
        Vector2 BoomerangStop;
        _originPosition = transform.position;

        if (_originPosition.x > 0)
        {
            BoomerangStop = BoomerangLeft;
        }
        else
        {
            BoomerangStop = BoomerangRight;
        }

        transform.position = Vector2.MoveTowards(transform.position, BoomerangStop, Speed * Time.deltaTime);
    }

    private void BoomerangAttack()
    {

    }
}
