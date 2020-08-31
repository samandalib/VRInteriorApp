﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script makes the Slider able to change the daylight direction, intensity and color
/// </summary>
public class TimeSetSlider : MonoBehaviour
{
    public float lightAngle;
    public GameObject lightSource;
    public GameObject sceneCamera;

    private Camera _camera;

    private Color _baseBgColor;
    private Color _backgroundColor;



    Vector3 lightSourceBaseAngles;
    float baseIntensity;
    float lightIntensity;

    void Start()
    {
        lightSourceBaseAngles = lightSource.transform.eulerAngles;
        baseIntensity = lightSource.GetComponent<Light>().intensity;
        _camera = sceneCamera.GetComponent<Camera>();
        _baseBgColor = _camera.backgroundColor;
    }

    void Update()
    {
        
    }
    public void onChange()
    {
        lightAngle = GetComponent<Slider>().value;
        if (lightAngle<90 || lightAngle > -90)
        {
            float radian = Mathf.PI / 180;
            lightIntensity = Mathf.Cos(Mathf.Abs(lightAngle*radian));
            lightSource.GetComponent<Light>().intensity = lightIntensity;
            lightSource.transform.eulerAngles = new Vector3(lightSourceBaseAngles.x, lightAngle, lightSourceBaseAngles.z);

            _camera.backgroundColor = new Vector4(lightIntensity, lightIntensity, lightIntensity);

        }
        else if (lightAngle==90 || lightAngle==-90)
        {
            lightIntensity = 0;
        }
        

    }
}
