using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkWallActivation : RemoteActivation
{
    [SerializeField] private bool startEnabled = true;
    [SerializeField] private bool singleUse;

    private Collider collider;
    private MeshRenderer renderer;
    private LightMovement soul;
    private int firstFrames;
    private bool caughtSoul;
    private GameObject lightTrap;
    private bool alreadyActivated;
    public override void Activate()
    {
        if (!alreadyActivated || !singleUse)
        {
            collider.enabled = true;
            renderer.enabled = true;
            caughtSoul = false;
            collider.isTrigger = true;
            lightTrap.SetActive(false);
            firstFrames = 0;
            alreadyActivated = true;
        }
    }

    public override void Deactivate()
    {
        collider.enabled = false;
        renderer.enabled = false;
        lightTrap.SetActive(false);
        soul.EnableMovement();
    }

    public override void Respawn()
    {
        if (!startEnabled)
        {
            Deactivate();
            alreadyActivated = false;
        }
        else
        {
            alreadyActivated = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
        renderer = GetComponent<MeshRenderer>();
        soul = FindObjectOfType<LightMovement>();
        lightTrap = transform.GetChild(0).gameObject;
        lightTrap.SetActive(false);
        if (!startEnabled)
        {
            Deactivate();
            alreadyActivated = false;
        }
        else
        {
            alreadyActivated = true;
        }
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
            renderer.enabled = false;
            caughtSoul = true;
            lightTrap.SetActive(true);
            soul.DisableMovement(collider.bounds);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        LightMovement freedSoul = other.gameObject.GetComponent<LightMovement>();
        if (!caughtSoul && freedSoul != null && freedSoul.gameObject.layer == freedSoul.GetLayer(false))
        {
            freedSoul.EnableMovement();
            collider.isTrigger = false;
            lightTrap.SetActive(false);
            renderer.enabled = true;
        }
    }
}
