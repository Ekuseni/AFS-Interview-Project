using AFSInterview;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerBullet : Bullet
{
    public override void Initialize(GameObject target)
    {
        targetObject = target;
    }

    // Update is called once per frame
    private void Update()
    {
        if (targetObject == null)
        {
            Destroy(gameObject);
            return;
        }

        var direction = (targetObject.transform.position - transform.position).normalized;

        transform.position += direction * speed * Time.deltaTime;

        if ((transform.position - targetObject.transform.position).magnitude <= 0.2f)
        {
            Destroy(gameObject);
            Destroy(targetObject);
        }
    }
}

