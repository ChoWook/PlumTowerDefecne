using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerAniController : BaseAniContoller
{
    private GameObject run1;
    private GameObject dead1;
    private GameObject deadEffect1;
    private GameObject run2;
    private GameObject dead2;
    private GameObject run3;
    private GameObject dead3;
    private GameObject run4;
    private GameObject dead4;
    int currentElement;

    private void OnEnable()
    {
        run1 = transform.GetChild(0).gameObject;
        dead1 = transform.GetChild(1).gameObject;
        deadEffect1 = transform.GetChild(2).gameObject;
        run2 = transform.GetChild(3).gameObject;
        dead2 = transform.GetChild(4).gameObject;
        run3 = transform.GetChild(5).gameObject;
        dead3 = transform.GetChild(6).gameObject;
        run4 = transform.GetChild(7).gameObject;
        dead4 = transform.GetChild(8).gameObject;

        currentElement = GetComponent<Enemy>().CurrentElement;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            DeadAnimation();
        }
    }

    public override void DeadAnimation()
    {
        switch (currentElement)
        {
            case 0:
                run1.SetActive(false);
                dead1.SetActive(true);
                break;
            case 1:
                run2.SetActive(false);
                dead2.SetActive(true);
                break;
            case 2:
                run3.SetActive(false);
                dead3.SetActive(true);
                break;
            case 3:
                run4.SetActive(false);
                dead4.SetActive(true);
                break;
        }
        deadEffect1.SetActive(true);
    }

    public override void InitAnimation()
    {
        switch (currentElement)
        {
            case 0:
                run1.SetActive(true);
                dead1.SetActive(false);
                break;
            case 1:
                run2.SetActive(true);
                dead2.SetActive(false);
                break;
            case 2:
                run3.SetActive(true);
                dead3.SetActive(false);
                break;
            case 3:
                run4.SetActive(true);
                dead4.SetActive(false);
                break;
        }
        deadEffect1.SetActive(false);
    }
}
