using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectable
{
    void collect(GameObject collector, string tag = "");
}
