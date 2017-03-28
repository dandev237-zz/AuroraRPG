using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField] private float mProjectileSpeed = 10.0f;
    public float projectileSpeed
    {
        get
        {
            return mProjectileSpeed;
        }
        set
        {
            mProjectileSpeed = value;
        }
    }

    [SerializeField] private float mDamageOnHit = 10.0f;
    public float damageOnHit
    {
        get
        {
            return mDamageOnHit;
        }
        set
        {
            mDamageOnHit = value;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Component damageableComponent = other.gameObject.GetComponent(typeof(IDamageable));
        if (damageableComponent)
        {
            (damageableComponent as IDamageable).TakeDamage(damageOnHit);
        }

        Destroy(gameObject);
    }
}
