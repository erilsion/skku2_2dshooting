using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("능력치")]
    private float _health = 3f;


    public void Hit(float Damage)
    {
        _health -= Damage;

        if (_health <= 0f)
        {
            Debug.Log("죽었다!");
            Destroy(this.gameObject);
        }
    }
    public void HealthUp(float value)
    {
        _health += value;
    }
}
