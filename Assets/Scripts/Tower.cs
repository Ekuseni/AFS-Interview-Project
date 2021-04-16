using AFSInterview;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] protected ObjectPool bulletPool;
    [SerializeField] protected Transform bulletSpawnPoint;
    [SerializeField] protected float firingRange;
    private IReadOnlyList<Enemy> enemies;
    protected Enemy targetEnemy;

    private void Awake()
    {
        bulletPool.CreatePool();
    }

    public virtual void Initialize(IReadOnlyList<Enemy> enemies)
    {
        gameObject.SetActive(true);
        this.enemies = enemies;
    }

    protected Enemy FindClosestEnemy(float firingRange)
    {
        Enemy closestEnemy = null;
        var closestDistance = float.MaxValue;

        foreach (var enemy in enemies)
        {
            var distance = (enemy.transform.position - transform.position).magnitude;
            if (distance <= firingRange && distance <= closestDistance)
            {
                closestEnemy = enemy;
                closestDistance = distance;
            }
        }

        return closestEnemy;
    }

    protected virtual void Update()
    {
        targetEnemy = FindClosestEnemy(firingRange);
        if (targetEnemy != null)
        {
            LookAt(targetEnemy.transform);
        }
    }

    protected void LookAt(Transform target)
    {
        var lookRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, lookRotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

}
