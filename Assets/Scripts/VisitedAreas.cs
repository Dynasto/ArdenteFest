using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisitedAreas : MonoBehaviour
{
    public List<string> VisitedAreasList = new List<string>();
    public static VisitedAreas inst;
    public Transform VisitedAreasPanel;
    public Transform ButtonPrefab;
    public Transform ScrollPanel;

    void Start()
    {
        if (!inst)
        {
            inst = this;
        }
        AddToVisitedAreasList("Grand Foyer");
    }

    public void AddToVisitedAreasList(string area)
    {
        if (VisitedAreasList.Contains(area) || area.StartsWith("Description"))
        {
            return;
        }
        VisitedAreasList.Add(area);
        var button = Instantiate(ButtonPrefab);
        button.SetParent(ScrollPanel);
        button.GetComponentInChildren<Text>().text = area;
    }

    public void ToggleAreas()
    {
        VisitedAreasPanel.gameObject.SetActive(!VisitedAreasPanel.gameObject.activeSelf);
    }
}
