using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    // 무결성: 속성(데이터)의 정확성, 일관성, 유효성
    private int _health;
    private int _maxHealth = 100;
    public int Health => _health;  // get 프로퍼티

    // 체력이 바뀌는 경우: 맞았을 때, 힐
    public void Heal(int amount)
    {
        _health += amount;
    }
    public void Hit(int amount)
    {
        _health -= amount;
    }

    public void Revive()
    {
        _health = _maxHealth;
    }
}
