using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    public string Code;
    public void GoTo(string id)
    {
        if (id.ToLower() == "back")
        {
            id = GameManager.inst.CurrentRoom;
        }
        var newPanel = this.transform.parent.gameObject.FindObject(id); 
        newPanel.SetActive(true);

        this.gameObject.SetActive(false);
        if (id.StartsWith("Description") == true)
        {
            GameManager.inst.TurnFastTravelOff();
            GameManager.inst.TurnOnBackButton(id);
        }
        else //Riktigt rum
        {
            GameManager.inst.CurrentRoom = id;
            GameManager.inst.CurrentDescription = null;
        }
        VisitedAreas.inst.AddToVisitedAreasList(id);
    }

    public void PlayMusic()
    {
        var audio = GetComponent<AudioSource>();
        if (audio.isPlaying)
        {
            return;
        }
        GetComponent<AudioSource>().Play();
    }
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
