using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float _damage;
    [SerializeField]
    private GameObject _destructionParticle;
    [SerializeField]
    private float _destructionParticleLifetime;

    private void OnCollisionEnter2D(Collision2D col)
    {
        foreach(var damageable in col.gameObject.GetComponents<IDamageable>())
        {
            damageable.takeDamage(_damage);
        }
        var destructionParticle = Instantiate(
            _destructionParticle,
            col.contacts[0].point,
            transform.rotation);
        Destroy(destructionParticle, _destructionParticleLifetime);
        Destroy(gameObject);
    }
}
