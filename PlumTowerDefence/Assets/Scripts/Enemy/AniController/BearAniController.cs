using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearAniController : BaseAniContoller
{
    Animator animator;

    private GameObject anim1;
    private GameObject deadEffect1;
    private GameObject anim2;
    private GameObject anim3;
    private GameObject anim4;
    int currentElement;


    private void Awake()
    {
        anim1 = transform.GetChild(0).gameObject;
        deadEffect1 = transform.GetChild(1).gameObject;
        anim2 = transform.GetChild(2).gameObject;
        anim3 = transform.GetChild(3).gameObject;
        anim4 = transform.GetChild(4).gameObject;

        currentElement = GetComponent<Enemy>().CurrentElement;

    }

    public override void DeadAnimation()
    {
        TriggerAnimation();
    }

    public override void InitAnimation()
    {
        switch (currentElement)
        {
            case 1:
                anim1.SetActive(true);
                break;
            case 2:
                anim2.SetActive(true);
                break;
            case 3:
                anim3.SetActive(true);
                break;
            case 4:
                anim4.SetActive(true);
                break;
        }
        deadEffect1.SetActive(false);

    }


    public void TriggerAnimation()
    {
        StartCoroutine(IE_TriggerAnimation());
    }

    public IEnumerator IE_TriggerAnimation()
    {
        switch (currentElement)
        {
            case 1:
                animator = anim1.GetComponent<Animator>();
                break;
            case 2:
                animator = anim2.GetComponent<Animator>();
                break;
            case 3:
                animator = anim3.GetComponent<Animator>();
                break;
            case 4:
                animator = anim4.GetComponent<Animator>();
                break;
        }
        if (animator != null)
        {
            animator.SetTrigger("DeadTrigger");
            GetComponent<EnemyMovement>().MoveSpeed = 0;
            deadEffect1.SetActive(true);
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
            ObjectPools.Instance.ReleaseObjectToPool(gameObject);
        }
        else
            Debug.Log("Null");
    }
}
