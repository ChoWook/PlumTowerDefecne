using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAniController : MonoBehaviour
{
    private GameObject run1;
    private GameObject dead1;
    private GameObject deadEffect1;


    private void OnEnable()
    {
        run1 = transform.GetChild(0).gameObject;
        dead1 = transform.GetChild(1).gameObject;
        deadEffect1 = transform.GetChild(2).gameObject;
        dead1.SetActive(false);
        deadEffect1.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0)){
            DeadAnimation();
        }
    }

    public void DeadAnimation()
    {
        run1.SetActive(false);
        dead1.SetActive(true);
        deadEffect1.SetActive(true);
    }

}
