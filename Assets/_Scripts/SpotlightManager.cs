using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightManager : MonoBehaviour
{

    [SerializeField]
    private float[] spotlightSwitchInterval;
    [SerializeField]
    private Sprite[] spotLights;
    [SerializeField]
    private SpriteRenderer[] spotlightRenderers;

    private float[] currentIntervalTime;
    private int[] currentUsedSpotLightPerPosition;
    private List<int>[] openSpotLightColors;

    // Start is called before the first frame update
    void Start()
    {
        currentUsedSpotLightPerPosition = new int[spotlightRenderers.Length];
        openSpotLightColors = new List<int>[spotlightRenderers.Length];
        for(int i = 0; i < openSpotLightColors.Length; i++)
        {
            openSpotLightColors[i] = new List<int>();
        }
        currentIntervalTime = new float[spotlightRenderers.Length];
        for(int i = 1; i < spotLights.Length; i++)
        {
            
            foreach(var list in openSpotLightColors)
            {
                list.Add(i);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < currentIntervalTime.Length; i++)
        {
            currentIntervalTime[i] += Time.deltaTime;
            if(currentIntervalTime[i] > spotlightSwitchInterval[i])
            {
                currentIntervalTime[i] = 0;
                int nextSpotlightIndex = Random.Range(0, openSpotLightColors[i].Count);
                int nextSpotLight = openSpotLightColors[i][nextSpotlightIndex];
                openSpotLightColors[i].RemoveAt(nextSpotlightIndex);
                spotlightRenderers[i].sprite = spotLights[nextSpotLight];
                openSpotLightColors[i].Add(currentUsedSpotLightPerPosition[i]);
                currentUsedSpotLightPerPosition[i] = nextSpotLight;
            }
        }
    }

    public void SetSpotlightIntervalForPosition(int position, float newInterval)
    {
        spotlightSwitchInterval[position] = newInterval;
    }
}
