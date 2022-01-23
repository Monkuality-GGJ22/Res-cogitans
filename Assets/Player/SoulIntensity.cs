using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulIntensity : MonoBehaviour
{
    [SerializeField] private float minLightIntensity;
    [SerializeField] private float maxLightIntensity;
    [SerializeField] private float intensityReductionFactor;

    public float MaxLightIntensity{
        get{ return maxLightIntensity; }
    }

    public float MinLightIntensity
    {
        get { return maxLightIntensity; }
    }

    private Light mLight;
    private LightMovement lightMovement;
    private float movingFactor;
    private float timer= 0f;
    public float MovingFactor {
        get { return movingFactor; }
        set { movingFactor=value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        mLight = GetComponent<Light>();
        lightMovement = GetComponent<LightMovement>();
        mLight.intensity = maxLightIntensity;
    }

    // Update is called once per frame
    void Update()
    { timer += Time.deltaTime;
        if (timer >= 2f && mLight.intensity >= minLightIntensity)
        {
            float totalintensityReductionFactor = intensityReductionFactor+movingFactor;
            mLight.intensity -= totalintensityReductionFactor*Time.deltaTime;
            mLight.intensity = mLight.intensity < minLightIntensity ? minLightIntensity : mLight.intensity;
            float lightPercent = (mLight.intensity - minLightIntensity) / (maxLightIntensity - minLightIntensity);
            float lightSpeed = lightMovement.MaxLightSpeed * lightPercent;
            lightMovement.LightSpeed = lightSpeed >= lightMovement.MinLightSpeed ? lightSpeed : lightMovement.MinLightSpeed;
        }
    }
}