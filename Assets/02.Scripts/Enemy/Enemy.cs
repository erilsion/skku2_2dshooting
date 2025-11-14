using DG.Tweening;
using UnityEngine;

public enum EEnemytype
{
    Directional,
    Trace,
    Boomerang,
    Boss
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

    [Header("플레이어 위치")]
    private GameObject _playerObject;

    [Header("부메랑 위치")]
    private Vector2 BoomerangLeft = new Vector2(1f, 2f);
    private Vector2 BoomerangRight = new Vector2(-1f, 2f);

    [Header("보스 관련")]
    private Vector2 _bossStop = new Vector2(0f, 3.5f);
    private Vector2 _bossSize = new Vector2(1.2f, 1.2f);
    private float _bossRiseTime = 1f;

    [Header("폭발 프리팹")]
    public GameObject ExplosionPrefab;

    [Header("적 능력치 초기값")]
    private float _directionalSpeed = 3f;
    private float _directionalHealth = 300f;
    private float _traceSpeed = 2f;
    private float _traceHealth = 240f;
    private float _boomerangSpeed = 4f;
    private float _boomerangHealth = 60f;
    private float _bossSpeed = 5f;
    private float _bossHealth = 1000f;

    [Header("아이템 드랍")]
    public GameObject[] ItemPrefabs;
    public int[] ItemWeights;
    private float _maxRate = 2f;
    private float _minRate = 0f;

    [Header("애니메이션 관련")]
    private Animator _animator;

    [Header("점수 관련")]
    private int _killScore = 100;

    [Header("사운드")]
    [SerializeField]
    private AudioClip _enemyDeathSound;
    [Range(0f, 1f)]
    public float _enemyDeathSoundVolume = 1f;


    private void Start()
    {
        _playerObject = GameObject.FindWithTag("Player");
        _animator = gameObject.GetComponent<Animator>();


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
            case EEnemytype.Boss:
                Speed = _bossSpeed;
                _health = _bossHealth;
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
        if (Type == EEnemytype.Boss)
        {
            gameObject.transform.DOScale(_bossSize, _bossRiseTime);
            MoveBoss();
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
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") == false) return;

        Player Player = other.gameObject.GetComponent<Player>();

        Player.Hit(Damage);

        Destroy(gameObject);
    }
    public void Hit(float damage)
    {
        _health -= damage;
        if (_animator != null)
        {
            _animator.SetInteger("x", 1);
            _animator.SetInteger("x", 0);
        }

        Death();
    }

    private void Death()
    {
        if (_health <= 0f)
        {
            DropItem();
            MakeExplosionEffect();

            EnemyKilledSound();

            EnemyKillScore();
            gameObject.SetActive(false);
        }
    }

    private void MakeExplosionEffect()
    {
        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
    }

    private void DropItem()
    {
        if (Random.Range(_minRate, _maxRate) == 0) return;  // 50%의 확률로 드랍

        // 가중치의 합
        // ItemWeights [70, 20, 10]
        int weightSum = 0;
        for(int i = 0; i < ItemWeights.Length; i++)
        {
            weightSum += ItemWeights[i];  // 100
        }

        // 0 ~ 100 가중치의 합 사이 랜덤 값
        int randomValue = UnityEngine.Random.Range(0, weightSum);

        // 가중치 값을 더해가며 구간을 비교한다.
        // 70보다 작다면 0번째 아이템 생성, 90(70+20)보다 작다면 1번째 아이템 생성, 100(90+10)보다 작다면 2번째 아이템 생성 
        int sum = 0; // 누적해갈 값

        for (int i = 0; i < ItemWeights.Length; i++)
        {
            sum += ItemWeights[i];
            if (randomValue < sum)
            {
                Instantiate(ItemPrefabs[i], transform.position, Quaternion.identity);
            }
        }
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

    private void MoveBoss()
    {
        Vector2 BossStop = _bossStop;
        transform.position = Vector2.MoveTowards(transform.position, BossStop, Speed * Time.deltaTime);
    }


    private void EnemyKillScore()
    {
        // 점수 관리자는 인스턴스가 단 하나다. 혹은 단 하나임을 보장해야 한다.
        // 아무데서나 빠르게 접근하고 싶다.
        // 싱글톤 패턴

        ScoreManager.Instance.AddScore(_killScore);

        // 관리자(Manager) -> 관리자의 인스턴스(객체)는 보통 하나
    }

    private void EnemyKilledSound()
    {
        if (_enemyDeathSound != null)
        {
            AudioSource.PlayClipAtPoint(_enemyDeathSound, transform.position, _enemyDeathSoundVolume);
        }
    }
}
