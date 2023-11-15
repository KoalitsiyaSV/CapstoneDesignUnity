using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TorchLight : MonoBehaviour
{
    private Light2D targetLight;
    private float settedIntensity;
    private float settedRadius;

    void Start()
    {
        targetLight = GetComponent<Light2D>();
        settedIntensity = targetLight.intensity;
        settedRadius = targetLight.pointLightOuterRadius;

        InvokeRepeating("ChangeIntensity", 0f, 0.083f);
        InvokeRepeating("ChangeRadius", 0f, 0.083f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ChangeIntensity()
    {
        float randomIntensityChange = Random.Range(-0.5f, 0.5f);
        float changedIntensity = settedIntensity + randomIntensityChange;
        
        targetLight.intensity = changedIntensity;
    }

    private void ChangeRadius()
    {
        float randomRadiusChange = Random.Range(-0.5f, 0.5f);
        float changedRadius = settedRadius + randomRadiusChange;

        targetLight.pointLightOuterRadius = changedRadius;
    }
}
