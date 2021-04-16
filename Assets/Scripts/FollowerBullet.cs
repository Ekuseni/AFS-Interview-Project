using AFSInterview;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerBullet : Bullet
{
    public override void Initialize(Enemy target)
    {
        targetObject = target;
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    private void Update()
    {
        if (targetObject == null)
        {
            ObjectPool.ReturnToPool(this);
            return;
        }

        var direction = (targetObject.transform.position - transform.position).normalized;

        transform.position += direction * speed * Time.deltaTime;

        if ((transform.position - targetObject.transform.position).magnitude <= 0.2f)
        {
            ObjectPool.ReturnToPool(this);
            targetObject.Kill();
        }
    }
}

