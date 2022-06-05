using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float _damage;
    [SerializeField]
    private GameObject _destructionParticle;

    private void OnCollisionEnter2D(Collision2D col)
    {
        foreach(var damageable in col.gameObject.GetComponents<IDamageable>())
        {
            damageable.takeDamage(_damage, "Projectile");
        }
        var destructionParticle = Instantiate(
            _destructionParticle,
            col.contacts[0].point,
            transform.rotation);
        Destroy(gameObject);
    }
}
