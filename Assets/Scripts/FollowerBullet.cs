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
        if (!targetObject.isActiveAndEnabled)
        {
            ObjectPool.ReturnToPool(this);
            return;
        }

        var direction = (targetObject.transform.position - transform.position).normalized;

        transform.position += direction * speed * Time.deltaTime;

        if ((transform.position - targetObject.transform.position).magnitude <= 0.2f)
        {
            targetObject.Kill();
            ObjectPool.ReturnToPool(this);
        }
    }

    private void OnDrawGizmos()
    {
        if (targetObject != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(targetObject.transform.position, 1f);
        }
    }

    public override void OnDisable()
    {
        targetObject = null;
    }
}