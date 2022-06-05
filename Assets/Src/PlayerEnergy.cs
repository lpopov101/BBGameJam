using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergy : MonoBehaviour
{
    private float _energy;

    private void Start()
    {
        _energy = 100F;
    }
    
    public void depleteEnergy(float amount)
    {
        _energy -= amount;
    }

    public void regainEnergy(float amount)
    {
        _energy += amount;
    }

    public float getEnergy()
    {
        return _energy;
    }
}
