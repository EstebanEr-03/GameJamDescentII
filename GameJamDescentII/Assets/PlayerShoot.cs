using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab; // El prefab de la bala
    public Transform bulletSpawn; // El punto desde donde se disparará la bala
    public float bulletSpeed = 20f; // La velocidad de la bala

    void Update()
    {
        // Verifica si se presiona el botón izquierdo del mouse
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Instancia la bala en la posición y rotación del punto de disparo
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

        // Obtiene el componente Rigidbody de la bala y le aplica una fuerza para dispararla
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = bulletSpawn.forward * bulletSpeed;
        }
    }
}
