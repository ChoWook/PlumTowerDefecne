using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleDownDestroy : MonoBehaviour
{
    public float scaleDownSpeed = 10;

    void Update()
    {
        ScaleDownTerminate(scaleDownSpeed);
    }

    public void ScaleDownTerminate(float scaleDownSpeed)
    {
        transform.localScale -= Vector3.one * scaleDownSpeed * 10 * Time.deltaTime;
        if (transform.localScale.x <= 0)
        {
            if (transform.GetChild(0).GetComponent<ParticleSystem>())
            {
                var firstChild = transform.GetChild(0).GetComponent<ParticleSystem>();
                firstChild.gameObject.AddComponent<DestroyTimer>().timer = 1;

                var emission = firstChild.emission;
                emission.rateOverTime = 0;
                firstChild.transform.parent = null;
                firstChild.transform.localScale = Vector3.one;
            }

            Destroy(gameObject);
        }
    }
}
