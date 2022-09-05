using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAniController : BaseAniContoller
{
    private GameObject run1;
    private GameObject dead1;
    private GameObject deadEffect1;
    private GameObject run2;
    private GameObject dead2;
    private GameObject run3;
    private GameObject dead3;
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

        run1.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0)){
            DeadAnimation();
        }
    }

    public override void DeadAnimation()
    {
        currentElement = GetComponent<Enemy>().CurrentElement;
        run1.SetActive(false);
        run2.SetActive(false);
        run3.SetActive(false);

        switch (currentElement)
        {
            case 0:
                dead1.SetActive(true);
                break;
            case 2:
                dead2.SetActive(true);
                break;
            case 3:
                dead3.SetActive(true);
                break;
        }
        deadEffect1.SetActive(true);
    }

    public override void InitAnimation()
    {
        currentElement = GetComponent<Enemy>().CurrentElement;
        dead1.SetActive(false);
        dead2.SetActive(false);
        dead3.SetActive(false);

        switch (currentElement)
        {
            case 0:
                run1.SetActive(true);
                break;
            case 2:
                run2.SetActive(true);
                break;
            case 3:
                run3.SetActive(true);
                break;
        }
        deadEffect1.SetActive(false);
    }

}
