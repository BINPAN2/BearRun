using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : View
{
    #region 常量
    #endregion

    #region 事件
    #endregion

    #region 字段

    public int selectIndex = 0;

    public MeshRenderer ballMesh;

    public SkinnedMeshRenderer SkinMeshRender;

    public ItemState state = ItemState.UnBuy;

    //持有BtnBuy的Image
    public Image imgBuybtn;
    public Image imgSkinBuybtn;
    public Image imgClothBuybtn;

    //BtnBuy的两个sprite（购买/装备）
    public Sprite sprBuy;
    public Sprite sprEquip;

    //三个头像
    public List<Sprite> Head;


    //状态Gizmo
    public Sprite gizmoBuy;
    public Sprite gizmoUnbuy;
    public Sprite gizmoEquip;

    public List<Image> FootballStateGizmo;
    public List<Image> SkinStateGizmo;
    public List<Image> ClothStateGizmo;

    GameModel gm;

    //UI相关
    public Image HeadShow;
    public Text Coin;
    public Text PlayerName;
    public Text PlayerLevel;

    #endregion

    #region 属性
    public override string Name
    {
        get
        {
            return Const.V_Shop;
        }
    }
    #endregion

    #region 方法

    public void UpdateUI()
    {
        Coin.text = gm.Coin.ToString();
        switch (gm.TakeonSkin.SkinID)
        {
            case 0:
                HeadShow.overrideSprite = Head[0];
                PlayerName.text = "莫莫";
                break;
            case 1:
                HeadShow.overrideSprite = Head[1];
                PlayerName.text = "Sali";
                break;
            case 2:
                HeadShow.overrideSprite = Head[2];
                PlayerName.text = "Sugar";
                break;
            default:
                break;
        }
    } 

    #region 足球

    public void OnNormalBallSelected()
    {
        Game.Instance.sound.PlayEffect("Se_UI_Dress");
        selectIndex = 0;
        ballMesh.material = Game.Instance.staticData.GetFootballInfo(selectIndex).FootballMaterial;
        UpdateFootballBuyBtn(selectIndex);
    }

    public void OnFireBallSelected()
    {
        Game.Instance.sound.PlayEffect("Se_UI_Dress");
        selectIndex = 1;
        ballMesh.material = Game.Instance.staticData.GetFootballInfo(selectIndex).FootballMaterial;
        UpdateFootballBuyBtn(selectIndex);
    }

    public void OnColorBallSelected()
    {
        Game.Instance.sound.PlayEffect("Se_UI_Dress");
        selectIndex = 2;
        ballMesh.material = Game.Instance.staticData.GetFootballInfo(selectIndex).FootballMaterial;
        UpdateFootballBuyBtn(selectIndex);
    }

    //更新购买按钮图标(足球)
    public void UpdateFootballBuyBtn(int i)
    {
        state = gm.CheckItemState(i);
        switch (state)
        {
            case ItemState.Buy:
                imgBuybtn.transform.gameObject.SetActive(true);
                imgBuybtn.sprite = sprEquip;
                break;
            case ItemState.UnBuy:
                imgBuybtn.transform.gameObject.SetActive(true);
                imgBuybtn.sprite = sprBuy;
                break;
            case ItemState.Equiped:
                imgBuybtn.transform.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }

    public void OnBuyFootballBtnClick()
    {
        Game.Instance.sound.PlayEffect("Se_UI_Button");
        state = gm.CheckItemState(selectIndex);
        switch (state)
        {
            case ItemState.Buy:
                //装备物品
                BuyFootballArgs ee = new BuyFootballArgs
                {
                    Index = selectIndex
                };
                SendEvent(Const.E_EquipFootball, ee);
                break;
            case ItemState.UnBuy:
                //发起购买
                BuyFootballArgs e = new BuyFootballArgs
                {
                    Coin = Game.Instance.staticData.GetFootballInfo(selectIndex).coin,
                    Index = selectIndex
                };
                SendEvent(Const.E_BuyFootball, e);
                break;
            case ItemState.Equiped:
                break;
            default:
                break;
        }
    }

    //更新足球gizmo
    public void UpdateFootBallGizmo()
    {
        for (int i = 0; i < 3; i++)
        {
            state = gm.CheckItemState(i);
            switch (state)
            {
                case ItemState.Buy:
                    FootballStateGizmo[i].overrideSprite = gizmoBuy;
                    break;
                case ItemState.UnBuy:
                    FootballStateGizmo[i].overrideSprite = gizmoUnbuy;
                    break;
                case ItemState.Equiped:
                    FootballStateGizmo[i].overrideSprite = gizmoEquip;
                    break;
                default:
                    break;
            }
        }
    }

    #endregion

    #region 人物

    public void OnMoMoClick()
    {
        Game.Instance.sound.PlayEffect("Se_UI_Dress");
        selectIndex = 0;
        SkinMeshRender.material = Game.Instance.staticData.GetPlayerSkinInfo(selectIndex, 0).FootballMaterial;
        UpdateSkinBuyBtn(selectIndex);
    }

    public void OnSaLiClick()
    {
        Game.Instance.sound.PlayEffect("Se_UI_Dress");
        selectIndex = 1;
        SkinMeshRender.material = Game.Instance.staticData.GetPlayerSkinInfo(selectIndex, 0).FootballMaterial;
        UpdateSkinBuyBtn(selectIndex);
    }

    public void OnSugarClick()
    {
        Game.Instance.sound.PlayEffect("Se_UI_Dress");
        selectIndex = 2;
        SkinMeshRender.material = Game.Instance.staticData.GetPlayerSkinInfo(selectIndex, 0).FootballMaterial;
        UpdateSkinBuyBtn(selectIndex);
    }

    //更新购买按钮图标(皮肤)
    public void UpdateSkinBuyBtn(int i)
    {
        state = gm.CheckSkinState(i);
        switch (state)
        {
            case ItemState.Buy:
                imgSkinBuybtn.transform.gameObject.SetActive(true);
                imgSkinBuybtn.sprite = sprEquip;
                break;
            case ItemState.UnBuy:
                imgSkinBuybtn.transform.gameObject.SetActive(true);
                imgSkinBuybtn.sprite = sprBuy;
                break;
            case ItemState.Equiped:
                imgSkinBuybtn.transform.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }


    public void OnBuySkinBtnClick()
    {
        Game.Instance.sound.PlayEffect("Se_UI_Button");
        state = gm.CheckSkinState(selectIndex);
        switch (state)
        {
            case ItemState.Buy:
                //装备物品
                BuySkinArgs e = new BuySkinArgs
                {
                    Coin = Game.Instance.staticData.GetPlayerSkinInfo(selectIndex, 0).coin,
                    ID = new SkinId { SkinID = selectIndex, ClothID = 0 }
                };
                SendEvent(Const.E_EquipSkin, e);
                break;
            case ItemState.UnBuy:
                //发起购买
                BuySkinArgs ee = new BuySkinArgs
                {
                    Coin = Game.Instance.staticData.GetPlayerSkinInfo(selectIndex,0).coin,
                    ID = new SkinId { SkinID = selectIndex, ClothID = 0 }
                };
                SendEvent(Const.E_BuySkin, ee);
                break;
            case ItemState.Equiped:
                break;
            default:
                break;
        }
    }


    //更新皮肤gizmo
    public void UpdateSkinGizmo()
    {
        for (int i = 0; i < 3; i++)
        {
            state = gm.CheckSkinState(i);
            switch (state)
            {
                case ItemState.Buy:
                    SkinStateGizmo[i].overrideSprite = gizmoBuy;
                    break;
                case ItemState.UnBuy:
                    SkinStateGizmo[i].overrideSprite = gizmoUnbuy;
                    break;
                case ItemState.Equiped:
                    SkinStateGizmo[i].overrideSprite = gizmoEquip;
                    break;
                default:
                    break;
            }
        }
    }
    #endregion

    #region 衣服

    public void OnNormalClick()
    {
        Game.Instance.sound.PlayEffect("Se_UI_Dress");
        selectIndex = 0;
        SkinMeshRender.material = Game.Instance.staticData.GetPlayerSkinInfo(gm.TakeonSkin.SkinID, selectIndex).FootballMaterial;
        UpdateClothBuyBtn(selectIndex);
    }

    public void OnBrasilClick()
    {
        Game.Instance.sound.PlayEffect("Se_UI_Dress");
        selectIndex = 1;
        SkinMeshRender.material = Game.Instance.staticData.GetPlayerSkinInfo(gm.TakeonSkin.SkinID, selectIndex).FootballMaterial;
        UpdateClothBuyBtn(selectIndex);
    }

    public void OnArgentinaClick()
    {
        Game.Instance.sound.PlayEffect("Se_UI_Dress");
        selectIndex = 2;
        SkinMeshRender.material = Game.Instance.staticData.GetPlayerSkinInfo(gm.TakeonSkin.SkinID, selectIndex).FootballMaterial;
        UpdateClothBuyBtn(selectIndex);
    }

    //更新购买按钮图标(衣服)
    public void UpdateClothBuyBtn(int i)
    {
        SkinId id = new SkinId
        {
            SkinID = gm.TakeonSkin.SkinID,
            ClothID = i
        };

        state = gm.CheckClothState(id);
        switch (state)
        {
            case ItemState.Buy:
                imgClothBuybtn.transform.gameObject.SetActive(true);
                imgClothBuybtn.sprite = sprEquip;
                break;
            case ItemState.UnBuy:
                imgClothBuybtn.transform.gameObject.SetActive(true);
                imgClothBuybtn.sprite = sprBuy;
                break;
            case ItemState.Equiped:
                imgClothBuybtn.transform.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }

    public void OnBuyClothBtnClick()
    {
        Game.Instance.sound.PlayEffect("Se_UI_Button");
        SkinId id = new SkinId
        {
            SkinID = gm.TakeonSkin.SkinID,
            ClothID = selectIndex
        };

        state = gm.CheckClothState(id);
        switch (state)
        {
            case ItemState.Buy:
                //装备物品
                BuySkinArgs e = new BuySkinArgs
                {
                    Coin = Game.Instance.staticData.GetPlayerSkinInfo(gm.TakeonSkin.SkinID, selectIndex).coin,
                    ID = new SkinId { SkinID = gm.TakeonSkin.SkinID, ClothID = selectIndex }
                };
                SendEvent(Const.E_EquipCloth, e);
                break;
            case ItemState.UnBuy:
                //发起购买
                BuySkinArgs ee = new BuySkinArgs
                {
                    Coin = Game.Instance.staticData.GetPlayerSkinInfo(gm.TakeonSkin.SkinID, selectIndex).coin,
                    ID = new SkinId { SkinID = gm.TakeonSkin.SkinID, ClothID = selectIndex }
                };
                SendEvent(Const.E_BuyCloth, ee);
                break;
            case ItemState.Equiped:
                break;
            default:
                break;
        }
    }


    //更新衣服gizmo
    public void UpdateClothGizmo()
    {
        for (int i = 0; i < 3; i++)
        {
            SkinId id = new SkinId
            {
                SkinID = gm.TakeonSkin.SkinID,
                ClothID = i
            };

            state = gm.CheckClothState(id);
            switch (state)
            {
                case ItemState.Buy:
                    ClothStateGizmo[i].overrideSprite = gizmoBuy;
                    break;
                case ItemState.UnBuy:
                    ClothStateGizmo[i].overrideSprite = gizmoUnbuy;
                    break;
                case ItemState.Equiped:
                    ClothStateGizmo[i].overrideSprite = gizmoEquip;
                    break;
                default:
                    break;
            }
        }
    }
    #endregion


    public void OnHeadClick()
    {
        Game.Instance.sound.PlayEffect("Se_UI_Button");
        UpdateSkinGizmo();
        HideBtn();
        ballMesh.material = Game.Instance.staticData.GetFootballInfo(gm.TakeonFootball).FootballMaterial;
        SkinMeshRender.material = Game.Instance.staticData.GetPlayerSkinInfo(gm.TakeonSkin.SkinID,gm.TakeonSkin.ClothID).FootballMaterial;
    }

    public void OnClothClick()
    {
        Game.Instance.sound.PlayEffect("Se_UI_Button");
        UpdateClothGizmo();
        HideBtn();
        ballMesh.material = Game.Instance.staticData.GetFootballInfo(gm.TakeonFootball).FootballMaterial;
        SkinMeshRender.material = Game.Instance.staticData.GetPlayerSkinInfo(gm.TakeonSkin.SkinID, gm.TakeonSkin.ClothID).FootballMaterial;
    }

    public void OnFootballClick()
    {
        Game.Instance.sound.PlayEffect("Se_UI_Button");
        UpdateFootBallGizmo();
        HideBtn();
        ballMesh.material = Game.Instance.staticData.GetFootballInfo(gm.TakeonFootball).FootballMaterial;
        SkinMeshRender.material = Game.Instance.staticData.GetPlayerSkinInfo(gm.TakeonSkin.SkinID, gm.TakeonSkin.ClothID).FootballMaterial;
    }


    public void HideBtn()
    {
        imgBuybtn.transform.gameObject.SetActive(false);
        imgClothBuybtn.transform.gameObject.SetActive(false);
        imgSkinBuybtn.transform.gameObject.SetActive(false);
    }

    public void OnPlayClick()
    {
        Game.Instance.sound.PlayEffect("Se_UI_Button");
        Game.Instance.LoadLevel(3);
    }
    
    public void OnReturnClick()
    {
        Game.Instance.sound.PlayEffect("Se_UI_Button");
        if (gm.lastsecenIndex == 4)
        {
            gm.lastsecenIndex = 1;
        }

        Game.Instance.LoadLevel(gm.lastsecenIndex);
    }

    #endregion
    #region Unity回调
    private void Awake()
    {
        gm = GetModel<GameModel>();
        ballMesh.material = Game.Instance.staticData.GetFootballInfo(gm.TakeonFootball).FootballMaterial;
        SkinMeshRender.material = Game.Instance.staticData.GetPlayerSkinInfo(gm.TakeonSkin.SkinID, gm.TakeonSkin.ClothID).FootballMaterial;
        UpdateUI();
        UpdateSkinGizmo();
        PlayerLevel.text = gm.Level.ToString();
    }
    #endregion

    #region 事件回调

    public override void HandleEvent(string eventName, object data)
    {

    }
    #endregion

    #region 帮助方法
    #endregion

}
