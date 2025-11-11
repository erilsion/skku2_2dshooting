using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    // 목표: 배경 스크롤이 되도록 하고 싶다.

    // 필요 속성: 머터리얼, 스크롤 속도
    public Material BackgroundMaterial;
    public float ScrollSpeed = 0.1f;

    void Update()
    {
        // 방향을 구한다.
        Vector2 direction = Vector2.up;

        // 스크롤한다.(움직인다.)
        BackgroundMaterial.mainTextureOffset += direction * ScrollSpeed * Time.deltaTime;
    }
}
