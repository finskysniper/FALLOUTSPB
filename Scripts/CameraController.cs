using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 10f;

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Берём направления камеры
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        // Убираем наклон по Y
        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 move = forward * vertical + right * horizontal;
        transform.position += move * moveSpeed * Time.deltaTime;
    }
}
