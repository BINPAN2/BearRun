using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIResume : View {

    public Image image;
    public Sprite[] sprite;

    public override string Name
    {
        get
        {
            return Const.V_Resume;
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

    public  void StartCount()
    {
        Show();
        StartCoroutine(StartCountCor());
    }

    IEnumerator StartCountCor()
    {
        int i = 3;
        while (i > 0)
        {
            image.sprite = sprite[i - 1];
            i--;
            yield return new WaitForSeconds(1);
            if (i<=0)
            {
                break;
            }
        }
        Hide();
        //发事件
        SendEvent(Const.E_ContinueGame);//更改游戏状态 ispause isplay
    }

    public override void HandleEvent(string eventName, object data)
    {

    }
}
