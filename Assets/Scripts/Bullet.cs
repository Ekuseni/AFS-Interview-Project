namespace AFSInterview
{
    using UnityEngine;

    public abstract class Bullet : MonoBehaviour
    {
        [SerializeField] protected float speed;
        protected Enemy targetObject;

        public abstract void Initialize(Enemy target);
        public abstract void OnDisable();
        
    }
}