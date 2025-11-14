using UnityEngine;

public class SpecialBomb : MonoBehaviour
{
    [Header("총알 프리팹")]
    public GameObject SpecialBombPrefab;

    [Header("쿨타임")]
    private float _bombTimer = 0f;
    private float _finishTime = 3f;
    private bool _finished = false;
    private float _effectTimer = 0f;
    private float _effectFinishTime = 4f;

    [Header("파티클 프리팹")]
    public GameObject PlayerEffectPrefab;
    public GameObject ParticlePrefab1;
    public GameObject ParticlePrefab2;
    public GameObject DestroyEffectPrefab;

    [Header("플레이어 위치")]
    private GameObject _playerObject;


    private void Start()
    {
        _playerObject = GameObject.FindWithTag("Player");
        _bombTimer = 0f;
        _finishTime = 3f;
        _effectTimer = 0f;
        _effectFinishTime = 4f;
        MakeParticleEffect();
    }

    public void Update()
    {
        _bombTimer += Time.deltaTime;

        if (_bombTimer >= _finishTime && !_finished)
        {
            _effectTimer += Time.deltaTime;
            Explode();
        }
    }

    private void Explode()
    {
        _finished = true;
        FinishSpecial();
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        Destroy(other.gameObject);
    }

    private void MakeParticleEffect()
    {
        PlayerEffectPrefab = Instantiate(PlayerEffectPrefab, _playerObject.transform.position, Quaternion.identity);
        ParticlePrefab1 = Instantiate(ParticlePrefab1, transform.position, Quaternion.identity);
        ParticlePrefab2 = Instantiate(ParticlePrefab2, transform.position, Quaternion.identity);
    }

    private void FinishSpecial()
    {
        if (PlayerEffectPrefab != null) Destroy(PlayerEffectPrefab);
        if (ParticlePrefab1 != null) Destroy(ParticlePrefab1);
        if (ParticlePrefab2 != null) Destroy(ParticlePrefab2);
        DestroyEffectPrefab = Instantiate(DestroyEffectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
        if (_effectTimer >= _effectFinishTime)
        {
            Destroy(DestroyEffectPrefab);
        }
    }
}
