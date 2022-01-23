using UnityEngine;

public abstract class RemoteActivation : Resettable
{
    public abstract void Activate();
    public abstract void Deactivate();
}