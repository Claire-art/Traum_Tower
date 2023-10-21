using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Check1 : MonoBehaviour
{
    public static int key1 = 0;
    public GameObject pass;
    public GameObject triggerObject;

    public void check(InputField f)
    {
        if (f.text == "물병자리")
        {
            key1 = 1;
            print("정답");
            Destroy(pass);
            Destroy(triggerObject);
        }

        else
        {
            print("틀린 답");
            pass.SetActive(true); 
        }
    }
}
