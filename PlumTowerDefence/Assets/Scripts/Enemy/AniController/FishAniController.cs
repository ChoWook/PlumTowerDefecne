using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAniController : BaseAniContoller
{
    Animator animator;

    private GameObject anim1;
    private GameObject deadEffect1;

    private void Awake()
    {
        anim1 = transform.GetChild(0).gameObject;
        deadEffect1 = transform.GetChild(1).gameObject;
        deadEffect1.SetActive(false);
    }

    public override void DeadAnimation()
    {
        TriggerAnimation();
    }

    public override void InitAnimation()
    {
        anim1.SetActive(true);
        deadEffect1.SetActive(false);

    }


    public void TriggerAnimation()
    {
        StartCoroutine(IE_TriggerAnimation());
    }

    public IEnumerator IE_TriggerAnimation()
    {
        animator = anim1.GetComponent<Animator>();
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
