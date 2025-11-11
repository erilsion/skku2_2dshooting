using UnityEngine;
public class GeminiTest : MonoBehaviour
{
    private int Speed = 3;

    void Update()
    {
        transform.position += movingPosition();
        Debug.Log("dfdaf" + Speed);
    }

    private Vector3 movingPosition()
    {
        return Vector3.up * Time.deltaTime * Speed;
    }
}
