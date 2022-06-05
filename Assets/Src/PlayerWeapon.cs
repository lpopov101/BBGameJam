using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField]
    private GameObject _projectile;
    [SerializeField]
    private Transform _firePoint;
    [SerializeField]
    private PlayerEnergy _playerEnergy;
    [SerializeField]
    private float _energyDrainPerShot;

    private void OnFire()
    {
        Instantiate(_projectile, _firePoint.position, _firePoint.rotation);
        _playerEnergy.depleteEnergy(_energyDrainPerShot);
    }
}
