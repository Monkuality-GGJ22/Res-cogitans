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
    void Update()
    {
        if (timer >= 2f && mLight.intensity >= minLightIntensity)
        {
            float totalintensityReductionFactor = intensityReductionFactor+movingFactor *intensityReductionFactorInMovement ;
            mLight.intensity -= totalintensityReductionFactor * Time.deltaTime;
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
        else timer += Time.deltaTime;
    }

    public void RefillLight()
    {
        StartCoroutine(RefillCoroutine());
    }
    IEnumerator RefillCoroutine()
    {
        mLight.intensity = maxLightIntensity;
        yield return new WaitForSeconds(1);
        mLight.intensity = maxLightIntensity;
    }


    public float GetSoulIntensity() {
        return mLight.intensity;
    }

}
