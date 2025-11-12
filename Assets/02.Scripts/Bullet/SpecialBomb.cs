using UnityEngine;

public class SpecialBomb : MonoBehaviour
{
    [Header("총알 프리팹")]
    public GameObject SpecialBombPrefab;

    [Header("쿨타임")]
    private float _bombTimer = 0f;
    private float _finishTimer = 3f;


    public void Update()
    {
        _bombTimer += Time.deltaTime;
    }

    public void SpecialAttack()
    {
        Instantiate(SpecialBombPrefab, Vector2.zero, Quaternion.identity);

        if (_bombTimer > _finishTimer) Destroy(gameObject);

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        Destroy(other.gameObject);
    }
}
