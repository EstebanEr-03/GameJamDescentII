using UnityEngine;
using Utilities;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float duration = 2f;

    private CountdownTimer timer;

    private void Start()
    {
        timer = new CountdownTimer(duration);
        timer.Start();
        timer.OnTimeStop += () => Destroy(gameObject);
    }

    private void Update()
    {
        timer.Tick(Time.deltaTime);
        transform.Translate(speed * Time.deltaTime * Vector3.forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
