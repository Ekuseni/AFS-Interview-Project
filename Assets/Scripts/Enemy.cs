namespace AFSInterview
{
    using System;
    using UnityEngine;
    using Random = UnityEngine.Random;

    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float maxSpeed;
        [SerializeField] private float speedVariance;

        public event Action<Enemy> OnEnemyDied;

        private Vector2 boundsMin;
        private Vector2 boundsMax;

        private float speed;
        private Vector3 target;


        public Vector3 Velocity
        {
            get
            {
                return (target - transform.position).normalized * speed;
            }
        }

        public void Initialize(Vector2 boundsMin, Vector2 boundsMax)
        {
            this.boundsMin = boundsMin;
            this.boundsMax = boundsMax;

            speed = maxSpeed + Random.Range(-speedVariance, speedVariance) * maxSpeed;

            SetTarget();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, Velocity * 1000f);
        }

        private void OnDestroy()
        {
            OnEnemyDied?.Invoke(this);
        }

        private void Update()
        {
            var direction = (target - transform.position).normalized;

            transform.position += direction * speed * Time.deltaTime;
            if ((transform.position - target).magnitude <= 0.1f)
                SetTarget();
        }

        private void SetTarget()
        {
            target = new Vector3(
                Random.Range(transform.position.x - 5, transform.position.x + 5),
                transform.position.y,
                Random.Range(transform.position.z - 5, transform.position.z + 5)
            );

            target.x = Mathf.Clamp(target.x, boundsMin.x, boundsMax.x);
            target.z = Mathf.Clamp(target.z, boundsMin.y, boundsMax.y);
        }

        private void OnTriggerEnter(Collider other)
        {
            Destroy(gameObject);
        }
    }
}