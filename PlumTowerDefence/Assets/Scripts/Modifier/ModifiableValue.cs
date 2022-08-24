using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ModifiableValue
{
    [SerializeField]
    private float baseValue;
    public float BaseValue { get { return baseValue; } set { baseValue = value; UpdateModifiedValue(); } }

    [SerializeField]
    private float modifiedValue;
    public float ModifiedValue { get { return modifiedValue; } private set { modifiedValue = value; } }

    public List<IModifier> modifiers = new List<IModifier>();

    public void UpdateModifiedValue()
    {
        float valueToAdd = 0;

        for (int i = 0; i < modifiers.Count; i++)
        {
            modifiers[i].AddValue(ref valueToAdd);
        }
        ModifiedValue = baseValue + valueToAdd;
    }

    public void AddModifier(IModifier _modifier)
    {
        modifiers.Add(_modifier);
        UpdateModifiedValue();
    }
    public void RemoveModifier(IModifier _modifier)
    {
        modifiers.Remove(_modifier);
        UpdateModifiedValue();
    }

}

[System.Serializable]
public class BuffExample : IModifier
{
    public int value;

    public void AddValue(ref float baseValue)
    {
        baseValue += value;
    }
}
