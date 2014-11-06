using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ItemGeneratorSpeedTest : MonoBehaviour
{

    public string webserviceOverride = "192.168.0.197/webservice/cloudgoods/cloudgoodsservice.svc/";
    public List<GeneratorStats> testRuns = new List<GeneratorStats>();
    int currentStep = 0;
    List<TimeKeepers> currentRun = new List<TimeKeepers>();
    bool isGenerating = true;


    IEnumerator ServiceCallGetListItemDatas(WWW www, Action<List<ItemData>, ItemGenerationSetup, TimeKeepers> callback, ItemGenerationSetup setup, TimeKeepers timeKeeper)
    {
        yield return www;

        if (www.error == null)
        {         
            try
            {
                Debug.Log("Recived items :" + www.text.ToRichColor(Color.white));
                callback(CloudGoods.serviceConverter.ConvertToItemDataList(www.text), setup, timeKeeper);
            }
            catch (Exception e)
            {
                Debug.LogWarning(e.Message);
                NextStep(setup);
            }
        }
        else
        {
            Debug.LogError(www.error.ToRichColor(Color.red) + " At:" + www.url.ToRichColor(Color.green));
            NextStep(setup);
        }
    }

    void Awake()
    {
        if (!string.IsNullOrEmpty(webserviceOverride))
        {
            CloudGoodsSettings.instance.url = webserviceOverride;
        }
        CloudGoods.OnUserAuthorized += SP_OnUserAuthorized;
    }

    void OnApplicationQuit()
    {
        RevertSettings();
    }

    public void RevertSettings()
    {
        CloudGoodsSettings.instance.url = "http://webservice.socialplay.com/cloudgoods/cloudgoodsservice.svc/";
    }

    void SP_OnUserAuthorized(CloudGoodsUser player)
    {
        isGenerating = false;
    }

    public void DoRun(ItemGenerationSetup setup)
    {

        if (isGenerating) return;
        isGenerating = true;
        currentStep = 0;
        currentRun.Clear();
        GenerateItems(setup, testRuns[currentStep]);
    }


    void GenerateItems(ItemGenerationSetup setup, GeneratorStats currentStats)
    {
        TimeKeepers currentKeeper = new TimeKeepers();
        string andTagsString = "";
        if (currentStats.AndTags.Count > 0)
        {
            foreach (string tag in currentStats.AndTags)
            {
                andTagsString += tag + ",";
            }
            andTagsString = andTagsString.Remove(andTagsString.Length - 1);
        }

        string url = string.Format("{0}GenerateItemsAtLocation?OwnerID={1}&OwnerType={2}&Location={3}&AppID={4}&MinimumEnergyOfItem={5}&TotalEnergyToGenerate={6}&ANDTags={7}&ORTags={8}", setup.Url, CloudGoods.user.sessionID, "Session", currentStats.Location, CloudGoods.AppID, currentStats.MinimumEnergyOfItem, currentStats.TotalEnergyToGenerate, andTagsString, "");
       Debug.Log("Sending URL :" + url.ToRichColor(Color.white));
        WWW www = new WWW(url);
        currentRun.Add(currentKeeper);
        StartCoroutine(ServiceCallGetListItemDatas(www, OnReceivedGeneratedItems, setup, currentKeeper));
    }

    public void OnReceivedGeneratedItems(List<ItemData> generatedItems, ItemGenerationSetup setup, TimeKeepers keeper)
    {
        keeper.CloseTimmer();     
        setup.putter.GetGameItem(generatedItems);
        NextStep(setup);
    }

    bool NextStep(ItemGenerationSetup setup)
    {
        currentStep++;
        if (currentStep >= testRuns.Count)
        {
            float totalTime = 0;
            foreach (TimeKeepers k in currentRun)
            {
                totalTime += k.runningTime;
            }
            currentRun.Clear();
            currentStep = 0;
            isGenerating = false;
            return false;
        }
        else
        {
            GenerateItems(setup, testRuns[currentStep]);
            return true;
        }

    }
}

[System.Serializable]
public class GeneratorStats
{
    public int Location = 0;
    public int MinimumEnergyOfItem = 0;
    public int TotalEnergyToGenerate = 1000;
    public List<string> AndTags;
    public List<string> ORTags;
}

public class TimeKeepers
{
    float startTime;
    float endTime = -1;
    public float runningTime
    {
        get
        {
            if (endTime != -1)
            {
                return endTime - startTime;
            }
            else
            {
                return Time.time - startTime;
            }
        }
    }

    public TimeKeepers()
    {
        startTime = Time.time;
    }


    public void CloseTimmer()
    {
        endTime = Time.time;
    }
}