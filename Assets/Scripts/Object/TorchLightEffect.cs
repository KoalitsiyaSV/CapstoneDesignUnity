using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TorchLightEffect : MonoBehaviour
{
    //Light 2D Ŭ���� ����
    private Light2D targetLight;

    //�ش� ��ũ��Ʈ�� ������ ������Ʈ�� Light 2D ������Ʈ���� ���� �� �� ����
    //private float settedIntensity;
    //private float settedOuterRadius;
    private float innerRadius;
    //private float settedInnerAngle;   
    private float falloffStrength;


    void Start()
    {
        targetLight = GetComponent<Light2D>();
        //settedIntensity = targetLight.intensity;
        //settedOuterRadius = targetLight.pointLightOuterRadius;
        innerRadius = targetLight.pointLightInnerRadius;
        //settedInnerAngle = targetLight.pointLightInnerAngle;
        falloffStrength = targetLight.falloffIntensity;

        InvokeRepeating("TorchEffect", 0f, 0.083f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TorchEffect()
    {
        //Intensity ����
        //float randomIntensityChange = Random.Range(-0.5f, 0.5f);
        //float changedIntensity = settedIntensity + randomIntensityChange;
       
        //targetLight.intensity = changedIntensity;

        //FalloffStrength ����
        float randomFalloffStrength = Random.Range(-0.1f, 0.1f);
        float randomFalloffStrengthValue = falloffStrength + randomFalloffStrength;
        
        targetLight.falloffIntensity = randomFalloffStrengthValue;

        //Outer Radius ����
        //float randomOuterRadiusChange = Random.Range(-0.5f, 0.5f);
        //float changedOuterRadius = settedOuterRadius + randomOuterRadiusChange;

        //targetLight.pointLightOuterRadius = changedOuterRadius;

        //Inner Radius ����
        float randomInnerRadiusChange = Random.Range(-0.5f, 0.5f);
        float randomInnerRadiusValue = innerRadius + randomInnerRadiusChange;

        targetLight.pointLightInnerRadius = randomInnerRadiusValue;

        //Inner Angle ����
        //float randomInnerAngleChange = Random.Range(5f, -5f);
        //float changeedInnerAngle = settedInnerAngle + randomInnerAngleChange;

        //targetLight.pointLightInnerAngle = changeedInnerAngle;
    }
}
