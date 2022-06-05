using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour, IDamageable
{
    [SerializeField]
    private GameObject _asteroidFragment;
    [SerializeField]
    private GameObject _fractureEffect;
    [SerializeField]
    private float _fractureRadius;
    [SerializeField]
    private int _numFragments;
    [SerializeField]
    private Vector2 _initDriftForce;
    [SerializeField]
    private float _initDriftTorque;
    [SerializeField]
    private float _initDriftVariation;
    [SerializeField]
    private float _fragmentDriftForce;
    
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        var driftForce = new Vector2(
            _initDriftForce.x + Random.Range(-_initDriftVariation, 
                                             _initDriftVariation),
            _initDriftForce.y + Random.Range(-_initDriftVariation, 
                                             _initDriftVariation)
        );
        _rb.AddForce(driftForce, ForceMode2D.Impulse);
        _rb.AddTorque(
            _initDriftTorque + Random.Range(-_initDriftVariation,
                                            _initDriftVariation),
            ForceMode2D.Impulse
        );   
    }

    private void fracture()
    {
        var angleNoise = Random.Range(0F, 360F);
        for(float angle = 0F; angle < 360F; 
            angle += 360F/(float)_numFragments)
        {
            var radAngle = (angle + angleNoise) * Mathf.Deg2Rad;
            var fragmentDirection = new Vector2(
                Mathf.Cos(radAngle),
                Mathf.Sin(radAngle)
            );
            var asteroidFragment = Instantiate(
                _asteroidFragment,
                (Vector2)transform.position + fragmentDirection*_fractureRadius,
                transform.rotation
            );
            var fragmentRb = asteroidFragment.GetComponent<Rigidbody2D>();
            fragmentRb.angularVelocity = _rb.angularVelocity;
            fragmentRb.AddForce(
                fragmentDirection*_fragmentDriftForce,
                ForceMode2D.Impulse
            );
        }
        Instantiate(
            _fractureEffect,
            transform.position,
            transform.rotation
        );
        Destroy(gameObject);
    }

    public void takeDamage(float damage, string tag = "")
    {
        if(tag == "Projectile")
        { 
            fracture();
        }
    }
}
