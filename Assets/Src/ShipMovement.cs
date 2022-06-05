using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipMovement : MonoBehaviour
{
    [SerializeField]
    private float _thrustForce;
    [SerializeField]
    private float _brakeForce;
    [SerializeField]
    private float _turnSpeed;
    [SerializeField]
    private float _autoBrakeForce;
    [SerializeField]
    private float _collisionRepulsionForce;
    [SerializeField]
    private PlayerEnergy _playerEnergy;
    [SerializeField]
    private float _energyDrainRate;

    private Rigidbody2D _rb;
    private float _thrustInput = 0F;
    private float _turnInput = 0F;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnMove(InputValue input)
    {
        var inputVec = input.Get<Vector2>();
        _thrustInput = inputVec.y;
        _turnInput = -inputVec.x;
    }

    private void FixedUpdate()
    {
        applyThrust();
        applyTurning();
        applyAutoBrake();
        
        if(_thrustInput > 0F)
        {
            _playerEnergy.depleteEnergy(_energyDrainRate * Time.fixedDeltaTime);
        }
    }

    private void applyThrust()
    {
        var moveForce = new Vector2(
            0,
            _thrustInput >= 0F ? _thrustInput * _thrustForce : 0F);
        _rb.AddRelativeForce(moveForce);
    }

    private void applyTurning()
    {
        var turnForce = _turnInput * _turnSpeed;
        _rb.angularVelocity = turnForce;
    }

    private void applyAutoBrake()
    {
        var totalBrakeForce = _autoBrakeForce;
        if(_thrustInput <= 0F)
        {   
            totalBrakeForce += _brakeForce * -_thrustInput;
        }
        _rb.AddForce(-_rb.velocity * totalBrakeForce);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        var averageContactPoint = Vector2.zero;
        foreach(var contact in col.contacts)
        {
            averageContactPoint += contact.point;
        }
        averageContactPoint /= col.contacts.Length;
        var repulsionForceDirection = (
            (Vector2)transform.position - averageContactPoint
        ).normalized;
        repulsionForceDirection = (
            (repulsionForceDirection - _rb.velocity.normalized)/2F
        ).normalized;
        _rb.AddForce(
            repulsionForceDirection * _collisionRepulsionForce  
                                    * col.relativeVelocity.magnitude,
            ForceMode2D.Impulse
        );
    }

}
