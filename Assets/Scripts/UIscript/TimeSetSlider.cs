using System.Collections;
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
    public float globalIntensityFix;

    private Camera _camera;

    private Color _baseBgColor;
    private Color _backgroundColor;



    Vector3 lightSourceBaseAngles;
    float baseIntensity;
    float lightIntensity;

    void Start()
    {
        //Get the light source (Directional Light) Angles
        lightSourceBaseAngles = lightSource.transform.eulerAngles;
        //Get the Light Intensity of the directional light
        baseIntensity = lightSource.GetComponent<Light>().intensity;
        _camera = sceneCamera.GetComponent<Camera>();

        //Get the background color to change in time changing
        _baseBgColor = _camera.backgroundColor;
    }

    void Update()
    {
        
    }
    public void onChange()
    {
        lightAngle = GetComponent<Slider>().value;

        //If direction of the light source changes, it means change in intensity and color of light
        if (lightAngle<90 || lightAngle > -90)
        {
            //Change Intensity of the directional light
            float radian = Mathf.PI / 180;
            lightIntensity = Mathf.Cos(Mathf.Abs(lightAngle*radian));
            Debug.Log("Directional Light Intensity is::::::::" + lightIntensity);

            lightSource.GetComponent<Light>().intensity = lightIntensity;
            lightSource.transform.eulerAngles = new Vector3(lightSourceBaseAngles.x, lightAngle, lightSourceBaseAngles.z);

            //change the background color
            _camera.backgroundColor = new Vector4(lightIntensity, lightIntensity, lightIntensity);

            //change the ambient intensity
            RenderSettings.ambientIntensity = lightIntensity+globalIntensityFix ;
            Debug.Log("Ambient Intensity is :::::" + RenderSettings.ambientIntensity);

        }
        else if (lightAngle==90 || lightAngle==-90)
        {
            lightIntensity = 0;
        }
        

    }
}
