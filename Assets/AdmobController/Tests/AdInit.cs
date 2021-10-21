using GoogleMobileAds.Api;
using GoogleMobileAdsMediationTestSuite.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdInit : MonoBehaviour
{
    private IEnumerator Start()
    {
        //Debug.Log("Initialize Applovin");
        //AppLovin.Initialize();        

        yield return new WaitForSeconds(0.5f);

        Debug.Log("Initialize Google Ad");        

        MobileAds.Initialize(initStatus =>
        {
            var statusMap = initStatus.getAdapterStatusMap();

            foreach(var st in statusMap)
            {
                Debug.Log($"{st.Key} State : {st.Value.InitializationState} Desc {st.Value.Description} | ToString {st.Value} Latency {st.Value.Latency}");
            }            
        });
    }

    public void ShowTestSuite()
    {
        MediationTestSuite.Show();
    }
}
