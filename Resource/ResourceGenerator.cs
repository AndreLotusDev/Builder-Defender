using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private float timePassed = 0;
    private const float ONE_SECOND_RESETER = 1;

    void Update()
    {
        timePassed += Time.deltaTime;

        if(timePassed >= ONE_SECOND_RESETER)
        {

        }
    }

    private void AddResource()
    {

    }
}
