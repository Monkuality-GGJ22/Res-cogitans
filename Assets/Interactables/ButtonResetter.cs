using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonResetter : Resettable
{
    public override void Respawn()
    {
        GetComponent<Button>().Start();
    }
}
