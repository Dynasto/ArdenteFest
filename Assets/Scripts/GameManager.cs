using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;
    public string CurrentRoom;
    public string CurrentDescription;
    public Transform BackButton;
    public Transform FastTravelButton;
    public Transform FastTravelScrollView;
    private Transform Canvas;
    public Transform InputCode;
    public Transform SafeOpen;
    public Transform ErrorText;

    void Start()
    {
        inst = this;
        inst.CurrentRoom = "Grand Foyer";
        Canvas = GameObject.Find("Canvas").transform;
    }

    public void GoToGlobal(Transform button)
    {
        var newId = button.GetComponentInChildren<Text>().text;
        if (newId != inst.CurrentRoom)
        {
            var newPanel = inst.Canvas.gameObject.FindObject(newId);

            if (newPanel != null)
            {
                newPanel.SetActive(true);
                var currentRoom = GameObject.Find(inst.CurrentRoom);
                if (currentRoom)
                {
                    currentRoom.SetActive(false);
                }//nullchecka this och make better fast travel med descriptions
                if (string.IsNullOrEmpty(inst.CurrentDescription) == false)
                {
                    Debug.Log(inst.CurrentDescription);
                    GameObject.Find(inst.CurrentDescription).SetActive(false);
                    inst.CurrentDescription = null;
                }
                VisitedAreas.inst.AddToVisitedAreasList(newId);
                inst.CurrentRoom = newId;
            }
            else
            {
                Debug.LogError("Du är dum i huvudet, namnet finns inte: " + newId);
            }
        }
    }

    public void GoToGlobal(string newId)
    {
        Debug.Log(newId);
        var newPanel = inst.Canvas.gameObject.FindObject(newId);

        newPanel.SetActive(true);
    }

    public void Back()
    {
        var newId = inst.CurrentRoom;
        var newPanel = inst.Canvas.gameObject.FindObject(newId);

        if (newPanel != null)
        {
            newPanel.SetActive(true);
            GameObject.Find(inst.CurrentDescription).SetActive(false);
            VisitedAreas.inst.AddToVisitedAreasList(newId);
        }

        BackButton.gameObject.SetActive(false);
        inst.CurrentDescription = null;

        inst.TurnFastTravelOn();
    }

    public void WriteCode()
    {
        var codeWindow = inst.Canvas.gameObject.FindObject("Input Code");
        var safeWindow = inst.Canvas.gameObject.FindObject("Description Safe Closed");
        codeWindow.SetActive(true);
        safeWindow.SetActive(false);

    }

    public void ValidateCode(InputField inputField)
    {
        var code = inputField.text;

        if (code.ToLower() == "mellon" && inst.CurrentDescription != "Description Safe Closed")
        {
            inputField.text = "";
            ErrorText.gameObject.SetActive(false);
            InputCode.gameObject.SetActive(false);
            GoToGlobal("Grand Foyer");
        }
        else if (code.ToLower() == "egg" && inst.CurrentDescription == "Description Safe Closed")
        {
            inputField.text = "";
            ErrorText.gameObject.SetActive(false);
            InputCode.gameObject.SetActive(false);
            SafeOpen.gameObject.SetActive(true);
        }
        else
        {

            inst.ErrorText.gameObject.GetComponent<Animation>().Play("errorcodeanimation");
            inst.ErrorText.gameObject.GetComponent<AudioSource>().Play();
        }
    }

    public List<GameObject> FindInactiveGameObjects()
    {
        GameObject[] all = GameObject.FindObjectsOfType<GameObject>();//Get all of them in the scene
        //Debug.Log();
        List<GameObject> objs = new List<GameObject>();
        foreach (GameObject obj in all) //Create a list 
        {
            objs.Add(obj);
        }
        Predicate<GameObject> inactiveFinder = new Predicate<GameObject>((GameObject go) => !go.activeInHierarchy);//Create the Finder
        List<GameObject> results = objs.FindAll(inactiveFinder);//And find inactive ones
        return results;
    }

    public void TurnOnBackButton(string id)
    {
        inst.CurrentDescription = id;
        BackButton.gameObject.SetActive(true);
    }

    public void TurnFastTravelOff()
    {
        FastTravelButton.gameObject.SetActive(false);
        FastTravelScrollView.gameObject.SetActive(false);
    }
    public void TurnFastTravelOn()
    {
        FastTravelButton.gameObject.SetActive(true);
        //FastTravelScrollView.gameObject.SetActive(true);
    }
}

