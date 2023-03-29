using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using Palmmedia.ReportGenerator.Core.Common;
using Unity.VisualScripting;
using UnityEngine.UI;

public class OpenWeather : MonoBehaviour
{
    //C = K - 273.15.

    [SerializeField] private string API_key = "bc5d58f5d7bbfd1874ad0d119173c277";

    //EAC
    [SerializeField] Text EAC_temp;
    [SerializeField] Text EAC_desc;
    //FRS
    [SerializeField] Text FRS_temp;
    [SerializeField] Text FRS_desc;
    //GAU
    [SerializeField] Text GAU_temp;
    [SerializeField] Text GAU_desc;
    //KZN
    [SerializeField] Text KZN_temp;
    [SerializeField] Text KZN_desc;
    //LIM
    [SerializeField] Text LIM_temp;
    [SerializeField] Text LIM_desc;
    //MPU
    [SerializeField] Text MPU_temp;
    [SerializeField] Text MPU_desc;
    //NOW
    [SerializeField] Text NOW_temp;
    [SerializeField] Text NOW_desc;
    //NOC
    [SerializeField] Text NOC_temp;
    [SerializeField] Text NOC_desc;
    //WEC
    [SerializeField] Text WEC_temp;
    [SerializeField] Text WEC_desc;

    float temp;
    string description;

    void Start()
    {
        StartCoroutine(getWeatherData("Bhisho", EAC_temp, EAC_desc));
        StartCoroutine(getWeatherData("Bloemfontein", FRS_temp, FRS_desc));
        StartCoroutine(getWeatherData("Johannesburg", GAU_temp, GAU_desc));
        StartCoroutine(getWeatherData("Pietermaritzburg", KZN_temp, KZN_desc));
        StartCoroutine(getWeatherData("Polokwane", LIM_temp, LIM_desc));
        StartCoroutine(getWeatherData("Mbombela", MPU_temp, MPU_desc));
        StartCoroutine(getWeatherData("Mahikeng", NOW_temp, NOW_desc));
        StartCoroutine(getWeatherData("Kimberley", NOC_temp, NOC_desc));
        StartCoroutine(getWeatherData("Cape Town", WEC_temp, WEC_desc));
    }

    //Download JSON data
    private IEnumerator getWeatherData(string city, Text tempText, Text descriptionText)
    {

        UnityWebRequest data = new UnityWebRequest("https://api.openweathermap.org/data/2.5/weather?q=" + city + "&appid=" + API_key)
        {
            downloadHandler = new DownloadHandlerBuffer()
        };

        yield return data.SendWebRequest();

        if (data.result == UnityWebRequest.Result.ConnectionError|| data.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Error: " + data.error);
            yield break;
        }

        Debug.Log(data.downloadHandler.text);

        setWeather(data.downloadHandler.text);

        temp -= 273.15f; //convert to celsius

        //update UI
        tempText.text = "Temp: " + temp.ToString("F2") + "°C";
        descriptionText.text = "Desc: " + description;
    }

    //set the weather attributes from the JSON file
    private void setWeather(string JSONstring)
    {
        var weatherJson = JSON.Parse(JSONstring);
        temp = weatherJson["main"]["temp"].AsFloat;
        description = weatherJson["weather"][0]["description"].Value;
    }
}
