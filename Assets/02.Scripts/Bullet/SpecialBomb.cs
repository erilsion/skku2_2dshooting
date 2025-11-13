using UnityEngine;

public class SpecialBomb : MonoBehaviour
{
    [Header("총알 프리팹")]
    public GameObject SpecialBombPrefab;

    [Header("쿨타임")]
    private float _bombTimer = 0f;
    private float _finishTimer = 3f;
    private bool _finished = false;

    [Header("파티클 프리팹")]
    public GameObject PlayerEffectPrefab;
    public GameObject ParticlePrefab;

    private void Start()
    {
        _bombTimer = 0f;
        _finishTimer = 3f;
    }

    public void Update()
    {
        _bombTimer += Time.deltaTime;

        if (_bombTimer >= _finishTimer && !_finished)
        {
            Explode();
        }
    }

    private void Explode()
    {
        _finished = true;

        if (SpecialBombPrefab != null)
        {
            MakeParticleEffect();
        }
        FinishSpecial();
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        Destroy(other.gameObject);
    }
    private void MakeParticleEffect()
    {
        Instantiate(PlayerEffectPrefab, transform.position, Quaternion.identity);
        Instantiate(ParticlePrefab, transform.position, Quaternion.identity);
    }

    private void FinishSpecial()
    {
        if (PlayerEffectPrefab != null) Destroy(PlayerEffectPrefab);
        if (ParticlePrefab != null) Destroy(ParticlePrefab);
        Destroy(gameObject);
    }
}
