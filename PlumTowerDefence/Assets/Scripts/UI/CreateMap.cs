using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Map.Instance.ChooseRandomMapPattern();
    }
}
