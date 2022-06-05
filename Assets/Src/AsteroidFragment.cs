using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidFragment : MonoBehaviour, IDamageable
{
    [SerializeField]
    private GameObject _energyPickup;
    [SerializeField]
    private GameObject _destroyEffect;
    [SerializeField]
    [Range(0,1)]
    private float _pickupSpawnProbability;

    private void destroyFragment()
    {
        if(_pickupSpawnProbability >= Random.Range(0F, 1F))
        {
            Instantiate(
                _energyPickup, 
                transform.position, 
                Quaternion.identity
            );
        }
        Instantiate(
            _destroyEffect,
            transform.position,
            transform.rotation
        );
        Destroy(gameObject);
    }

    public void takeDamage(float damage, string tag = "")
    {
        if(tag == "Projectile")
        {
            destroyFragment();
        }
    }
}
