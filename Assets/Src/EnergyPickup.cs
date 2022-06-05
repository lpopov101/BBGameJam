using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPickup : MonoBehaviour
{
    [SerializeField]
    private float _energyRestored;

    private void OnTriggerEnter2D(Collider2D col)
    {
        var obj = col.gameObject;
        var playerEnergy = obj.GetComponent<PlayerEnergy>();
        if(playerEnergy)
        {
            playerEnergy.regainEnergy(_energyRestored);
            Destroy(gameObject);
        }
    }
}
