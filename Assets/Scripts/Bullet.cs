namespace AFSInterview
{
    using UnityEngine;

    public abstract class Bullet : MonoBehaviour
    {
        [SerializeField] protected float speed;
        protected GameObject targetObject;

        public abstract void Initialize(GameObject target);
    }
}