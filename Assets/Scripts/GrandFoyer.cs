using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandFoyer : Room {

    public void GoTo(string id)
    {
        var newPanel = this.transform.parent.gameObject.FindObject(id);
        newPanel.SetActive(true);

        this.gameObject.SetActive(false);
    }
}
