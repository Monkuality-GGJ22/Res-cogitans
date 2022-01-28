using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateResetter : Resettable
{
    // Start is called before the first frame update
    public override void Respawn()
    {
        GetComponent<PressurePlateScript>().Start();
    }
}
