using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public EResourceType ResourceType = EResourceType.Magnetite;

    [SerializeField] GameObject[] Resources;

    float[] Probs;

#if UNITY_EDITOR
    private void Update()
    {
        UpdateResourceType();
    }
#endif

    private void OnEnable()
    {
        Probs = new float[Resources.Length];

        Tables.Load();

        for (int i = 0; i < Probs.Length; i++)
        {
            Probs[i] = Tables.MapGimmickResource.Get(i + 1)._Probability;
        }

        SetResourceType(ChooseType());
    }

    int ChooseType()
    {
        float total = 0;

        foreach (float elem in Probs)
        {
            total += elem;
        }

        float randomPoint = Random.Range(0, total);

        for (int i = 0; i < Probs.Length; i++)
        {
            if (randomPoint < Probs[i])
            {
                return i;
            }
            else
            {
                randomPoint -= Probs[i];
            }
        }
        return Probs.Length - 1;
    }

    public void SetResourceType(int Sender)
    {
        ResourceType = (EResourceType)(Sender + 1);

        UpdateResourceType();
    }

    public void UpdateResourceType()
    {
        for(int i = 0; i < Resources.Length; i++)
        {
            if(Resources[i].name.CompareTo(ResourceType.ToString()) == 0)
            {
                Resources[i].SetActive(true);
            }
            else
            {
                Resources[i].SetActive(false);
            }
        }
    }
}
