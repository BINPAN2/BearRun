using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenu : View
{
    GameModel gm;
    public MeshRenderer ballMesh;
    public SkinnedMeshRenderer SkinMeshRender;


    public override string Name
    {
        get
        {
           return Const.V_MainMenu;
        }
    }

    public override void HandleEvent(string eventName, object data)
    {

    }

    public void OnPlayClick()
    {
        Game.Instance.sound.PlayEffect("Se_UI_Button");
        Game.Instance.LoadLevel(3);
    }

    public void OnShopClick()
    {
        Game.Instance.sound.PlayEffect("Se_UI_Button");
        Game.Instance.LoadLevel(2);
    }

    private void Awake()
    {
        gm = GetModel<GameModel>();
        SkinMeshRender.material = Game.Instance.staticData.GetPlayerSkinInfo(gm.TakeonSkin.SkinID, gm.TakeonSkin.ClothID).FootballMaterial;
        ballMesh.material = Game.Instance.staticData.GetFootballInfo(gm.TakeonFootball).FootballMaterial;
    }
}
