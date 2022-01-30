using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulIntensity : MonoBehaviour
{
    [SerializeField] private float minLightIntensity;
    [SerializeField] private float maxLightIntensity;
    [SerializeField] private float intensityReductionFactor;
    [SerializeField] private float intensityReductionFactorInMovement;

    public float MaxLightIntensity{
        get{ return maxLightIntensity; }
    }

    public float MinLightIntensity
    {
        get { return minLightIntensity; }
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
    void FixedUpdate()
    {
        if (timer >= 2f && mLight.intensity >= minLightIntensity)
        {

            float totalintensityReductionFactor = intensityReductionFactor+movingFactor *intensityReductionFactorInMovement ;
            mLight.intensity -= totalintensityReductionFactor * Time.fixedDeltaTime;
            mLight.intensity = mLight.intensity < minLightIntensity ? minLightIntensity : mLight.intensity;

            float lightPercent = 1 - (mLight.intensity - minLightIntensity) / (maxLightIntensity - minLightIntensity);
            float lightSpeed = lightMovement.MaxLightSpeed * lightPercent;
            lightMovement.LightSpeed = lightSpeed;
            if (lightSpeed < lightMovement.MinLightSpeed) {
                lightMovement.LightSpeed = lightMovement.MinLightSpeed;
            }else if(lightSpeed > lightMovement.MaxLightSpeed)
            {
                lightMovement.LightSpeed = lightMovement.MaxLightSpeed;
            }
        }
        else timer += Time.fixedDeltaTime;
    }

    public void RefillLight(bool fromDeath)
    {
        StartCoroutine(RefillCoroutine(fromDeath));
    }
    IEnumerator RefillCoroutine(bool fromDeath)
    {
        mLight.intensity = maxLightIntensity;
        if (fromDeath) lightMovement.ForceSetPos();
        yield return new WaitForSeconds(lightMovement.startupTime);
        mLight.intensity = maxLightIntensity;
        if (fromDeath) lightMovement.ForceSetPos();
    }


    public float GetSoulIntensity() {
        return mLight.intensity;
    }

}
