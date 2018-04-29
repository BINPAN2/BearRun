using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPause : View
{
    public Text txtCoins;
    public Text txtDistance;
    public Text txtScore;

    public override string Name
    {
        get
        {
            return Const.V_Pause;
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void OnResumeButtonClick()
    {
        Hide();
        SendEvent(Const.E_ResumeGame);
    }

    public override void HandleEvent(string eventName, object data)
    {

    }
}
