using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static IEnumerator AfterDelayDoThat(float delay, Action that)
    {
        yield return new WaitForSeconds(delay);
        that();
    }
}
