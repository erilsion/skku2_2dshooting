using UnityEngine;

public class SpecialBomb : MonoBehaviour
{
    [Header("총알 프리팹")]
    public GameObject SpecialBombPrefab;

    [Header("쿨타임")]
    private float _bombTimer = 0f;
    private float _finishTimer = 3f;

    [Header("파티클 프리팹")]
    public GameObject PlayerEffectPrefab;
    public GameObject ParticlePrefab;


    public void Update()
    {
        _bombTimer += Time.deltaTime;
        MakeParticleEffect();
    }

    public void SpecialAttack()
    {
        if (_bombTimer > _finishTimer) Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        Destroy(other.gameObject);
    }
    private void MakeParticleEffect()
    {
        Instantiate(PlayerEffectPrefab, transform.position, Quaternion.identity);
        Instantiate(ParticlePrefab, SpecialBombPrefab.transform.position, Quaternion.identity);
    }
}
