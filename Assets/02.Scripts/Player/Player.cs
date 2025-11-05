using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("능력치")]
    private float _Health = 3f;

    public void Hit(float Damage)
    {
        _Health -= Damage;

        if (_Health <= 0f)
        {
            Destroy(this.gameObject);
        }
    }
}
