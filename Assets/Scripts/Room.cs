using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    public string Name;
    public string Description;
    public Image Image;
}

public static class ExtensionMethods
{
    public static GameObject FindObject(this GameObject parent, string name)
    {
        Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs)
        {
            if (t.name == name)
            {
                return t.gameObject;
            }
        }
        return null;
    }
}
