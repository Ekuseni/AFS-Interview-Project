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
                    var bullet = bulletPool.GetFromPool<Bullet>();
                    bullet.transform.position = bulletSpawnPoint.transform.position;
                    bullet.Initialize(targetEnemy);
                }

                fireTimer = firingRate;
            }
        }


    }
}
