using UnityEngine;

namespace DescentII
{
    public class Dropeable : Entity
    {
        [SerializeField] float speed = 1f;

        public Transform Player { get; protected set; }

        protected void Awake()
        {
            Player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        protected void Update()
        {
            Vector3 direction = (Player.position - transform.position).normalized;
            transform.Translate(speed * Time.deltaTime * direction);
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Execute();
            }
        }

        protected virtual void Execute() => Destroy(gameObject);
    }
}
