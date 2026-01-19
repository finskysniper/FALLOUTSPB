using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 10f;

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(horizontal, 0f, vertical);
        transform.position += move * moveSpeed * Time.deltaTime;
    }
}
