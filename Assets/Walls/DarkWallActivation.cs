using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkWallActivation : RemoteActivation
{
    [SerializeField] private bool startEnabled = true;

    private Collider collider;
    private MeshRenderer renderer;
    private LightMovement soul;
    private int firstFrames;
    private bool caughtSoul;

    public override void Activate()
    {
        collider.enabled = true;
        renderer.enabled = true;
        caughtSoul = false;
        collider.isTrigger = true;
        firstFrames = 0;
    }

    public override void Deactivate()
    {
        collider.enabled = false;
        renderer.enabled = false;
        soul.EnableMovement();
    }

    public override void Respawn()
    {
        Deactivate();
    }

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
        renderer = GetComponent<MeshRenderer>();
        soul = FindObjectOfType<LightMovement>();
        if (!startEnabled) Deactivate();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (firstFrames < 2)
        {
            firstFrames++;
            if (firstFrames == 2)
            {
                if (!caughtSoul) collider.isTrigger = false;
                caughtSoul = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        LightMovement freedSoul = other.gameObject.GetComponent<LightMovement>();
        if (freedSoul != null && freedSoul.gameObject.layer == freedSoul.GetLayer(true))
        {
            caughtSoul = true;
            soul.DisableMovement();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        LightMovement freedSoul = other.gameObject.GetComponent<LightMovement>();
        if (!caughtSoul && freedSoul != null && freedSoul.gameObject.layer == freedSoul.GetLayer(false))
        {
            freedSoul.EnableMovement();
            collider.isTrigger = false;
        }
    }
}
