using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    [SerializeField]
    private PlayerEnergy _playerEnergy;

    private Image _energyBar;

    private void Start()
    {
        _energyBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        _energyBar.fillAmount = _playerEnergy.getEnergy()/100F;
    }
}
