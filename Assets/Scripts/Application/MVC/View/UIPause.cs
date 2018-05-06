using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPause : View
{
    public Text txtCoins;
    public Text txtDistance;
    public Text txtScore;

    public MeshRenderer ballMesh;

    public SkinnedMeshRenderer SkinMeshRender;

    public GameModel gm;

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
        ballMesh.material = Game.Instance.staticData.GetFootballInfo(gm.TakeonFootball).FootballMaterial;
        SkinMeshRender.material = Game.Instance.staticData.GetPlayerSkinInfo(gm.TakeonSkin.SkinID, gm.TakeonSkin.ClothID).FootballMaterial;
    }

    public void OnResumeButtonClick()
    {
        Game.Instance.sound.PlayEffect("Se_UI_Button");
        Hide();
        SendEvent(Const.E_ResumeGame);
    }

    public void OnHomeButtonClick()
    {
        Game.Instance.sound.PlayEffect("Se_UI_Button");
        Game.Instance.LoadLevel(1);
    }

    public override void HandleEvent(string eventName, object data)
    {

    }
    private void Awake()
    {
        gm = GetModel<GameModel>();
    }
}
