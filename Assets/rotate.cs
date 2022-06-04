using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{

    public float torque;
    protected Rigidbody2D r; 

    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        // r.AddTorque(torque);
        Debug.Log(torque.ToString());
    }
}
