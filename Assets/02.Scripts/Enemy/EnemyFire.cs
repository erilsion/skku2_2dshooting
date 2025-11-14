using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    [Header("총구")]
    public Transform FirePosition;

    [Header("쿨타임")]
    private float _fireTimer = 0f;
    private float Cooltime = 0.4f;

    private void Update()
    {
        _fireTimer += Time.deltaTime;
    }

    public void AutoFireOn()
    {
        if (_fireTimer > Cooltime)
        {
            MakeBullets();

            _fireTimer = 0f;
        }
    }
    private void MakeBullets()
    {
        EnemyBulletFactory.Instance.MakeEnemyBullet(FirePosition.position);
    }
}
