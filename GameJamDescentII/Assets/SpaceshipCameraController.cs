using UnityEngine;

public class SpaceshipCameraController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float lookSpeed = 3f;
    public float rotationSpeed = 1f;

    private float yaw = 0f;
    private float pitch = 0f;

    void Update()
    {
        // Movimiento con WASD
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float upDown = 0f;

        if (Input.GetKey(KeyCode.Q))
        {
            upDown = 1f;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            upDown = -1f;
        }

        Vector3 direction = new Vector3(horizontal, upDown, vertical);
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        // Rotación con el mouse
        yaw += lookSpeed * Input.GetAxis("Mouse X");
        pitch -= lookSpeed * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        // Rotación con A y D (rotación lateral)
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.back * rotationSpeed);
        }
    }
}
