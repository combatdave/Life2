using UnityEngine;
using UnityEditor;

public class YourClassAsset
{
    [MenuItem("Assets/Create/Species")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<SpeciesType>();
    }
}