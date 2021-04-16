namespace AFSInterview
{
    using System.Collections.Generic;
    using UnityEngine;

    public class SimpleTower : Tower
    {
        [SerializeField] private float firingRate;

        private float fireTimer;

        public override void Initialize(IReadOnlyList<Enemy> enemies)
        {
            base.Initialize(enemies);
            fireTimer = firingRate;
        }

        protected override void Update()
        {
            base.Update();

            fireTimer -= Time.deltaTime;
            if (fireTimer <= 0f)
            {
                if (targetEnemy != null)
                {
                    var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity).GetComponent<Bullet>();
                    bullet.Initialize(targetEnemy.gameObject);
                }

                fireTimer = firingRate;
            }
        }


    }
}
