using AFSInterview;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlurryTower : Tower
{
    
    [SerializeField] private float firingRate;   
    [SerializeField] private int flurrySize;
    [SerializeField] private float rateOfFire;

    private float fireTimer;
    private bool isFiring = false;

    private WaitForSeconds waitFor;

    public override void Initialize(IReadOnlyList<Enemy> enemies)
    {
        base.Initialize(enemies);
        fireTimer = firingRate;
        waitFor = new WaitForSeconds(rateOfFire);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0f && !isFiring)
        {
            
            StartCoroutine(FireFlurry());
        }
    }

    IEnumerator FireFlurry()
    {
        isFiring = true;
        for (int i = 0; i < flurrySize; i++)
        {
            if (targetEnemy != null)
            {
                var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity).GetComponent<Bullet>();
                bullet.Initialize(targetEnemy);
            }
            else
            {
                break;
            }

            yield return waitFor;
        }

        isFiring = false;
        fireTimer = firingRate;

        yield return null;
    }
}
