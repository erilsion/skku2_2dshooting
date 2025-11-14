using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    [Header("적 타입")]
    public EEnemytype Type;

    [Header("총구")]
    public Transform FirePosition;

    [Header("쿨타임")]
    private float _fireTimer = 0f;
    private float Cooltime = 0.4f;

    [Header("회전 공격")]
    private float _turnSpeed = 1f;

    [Header("보스 공격 대기")]
    private float _bossTime = 0f;
    private float _bossCoolTime = 2f;


    private void Update()
    {
        _bossTime += Time.deltaTime;
        if (Type == EEnemytype.Boss)
        {
            BossAttack();
        }
    }

    public void BossAttack()
    {
        if (_bossTime < _bossCoolTime) return;
        SpinFire();
    }

    public void SpinFire()
    {
        transform.Rotate(Vector3.forward * (_turnSpeed * 100 * Time.deltaTime));
        _fireTimer += Time.deltaTime;
        if (_fireTimer < Cooltime) return;
        _fireTimer = 0f;
        MakeBullets();
    }

    private void MakeBullets()
    {
        EnemyBulletFactory.Instance.MakeEnemyBullet(FirePosition.position);
    }
}
