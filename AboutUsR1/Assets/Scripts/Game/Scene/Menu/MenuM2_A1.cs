using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening.Core;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Core;
using DG.Tweening.Plugins.Options;

public class MenuM2_A1 : MenuParent
{
    List<ButtonM2_A1_Left> buttonLefts = new List<ButtonM2_A1_Left>();
    List<ButtonM2_A1_Tab> buttonTabs = new List<ButtonM2_A1_Tab>();
    ButtonM2_A1_Left currentButtonLeft;
    ButtonM2_A1_Tab currentButtonTab;
    RawImage infoRaw0;
    RawImage infoRaw1;

    int pageIndex = 0;
    float pageWaitTime = 2f;
    TweenerCore<float, float, FloatOptions> tweenAnimation;

    public override void Init()
    {
        base.Init();
        
        //初始化 button Lefts
        buttonLefts.AddRange( transform.GetComponentsInChildren<ButtonM2_A1_Left>());
        //批量创建button Tab
        var prefabTab = transform.Find("Right/Tabs/prefab");
        for(int i  =0; i < 10; i++)
        {
            var o = GameObject.Instantiate(prefabTab.gameObject, transform.Find("Right/Tabs"));
            o.transform.localScale = Vector3.one;
            o.SetActive(false);
            var com = o.AddComponent<ButtonM2_A1_Tab>();
            com.Init();
            buttonTabs.Add(com);
        }
        prefabTab.gameObject.SetActive(false);
        //初始化Info Raw
        infoRaw0 = transform.Find("Right/Info/0").GetComponent<RawImage>();
        infoRaw1 = transform.Find("Right/Info/1").GetComponent<RawImage>();
        infoRaw0.color = Color.clear;
        infoRaw1.color = Color.clear;

    }

    public override void Show()
    {
        base.Show();
        InitView(buttonLefts[0]);
    }

    public override void Hide()
    {
        base.Hide();

    }

    public void InitView(ButtonM2_A1_Left buttonLeft)
    {
        if (currentButtonLeft == buttonLeft)
            return;
        currentButtonLeft = buttonLeft;
        //更新按钮颜色
        foreach(var btn in buttonLefts)
        {
            if(btn == buttonLeft)
            {
                btn.GetComponent<Image>().color = D.colorTab1;
            }
            else
            {
                btn.GetComponent<Image>().color = D.colorTab0;
            }
        }
        //button tab 装载数据
        for (int i = 0; i < buttonTabs.Count; i++)
        {
            if(i >= buttonLeft.dataTabs.Count)
            {
                buttonTabs[i].gameObject.SetActive(false);
                buttonTabs[i].DataTab = null;
            }
            else
            {
                buttonTabs[i].gameObject.SetActive(true);
                buttonTabs[i].DataTab = buttonLeft.dataTabs[i];
            }
        }
        //切换Tab视图
        currentButtonTab = null;
        SetupTabView(buttonTabs[0]);
    }
    public void SetupTabView(ButtonM2_A1_Tab buttonTab)
    {
        if (currentButtonTab == buttonTab)
            return;
        CancelInvoke();
        if (null != tweenAnimation)
        {
            tweenAnimation.Kill();
            tweenAnimation = null;
        }
        currentButtonTab = buttonTab;

        for (int i = 0; i < buttonTabs.Count; i++)
        {
            var btn_i = buttonTabs[i];
            var data = btn_i.DataTab;
            if (!btn_i.gameObject.activeSelf)
                break;
            //更新Tab标题
            btn_i.GetComponentInChildren<Text>().text = data.tabTitle.Replace('|', '\n');
            //更新Tab颜色
            if (btn_i != buttonTab)
            {
                btn_i.GetComponent<Image>().color = D.colorTab0;
            }
            else
            {
                btn_i.GetComponent<Image>().color = D.colorTab1;
                //更新下面资料
                if(1 == data.infoTextures.Count)
                {
                    infoRaw1.texture = data.infoTextures[0];
                    infoRaw1.color = Color.white;
                    infoRaw0.color = Color.clear;
                    Debug.LogError("1");
                }
                else if(1 < data.infoTextures.Count)
                {
                    pageIndex = 0;
                    infoRaw1.texture = data.infoTextures[0];
                    infoRaw1.color = Color.white;
                    infoRaw0.color = Color.clear;
                    InvokeRepeating("PageAnimation", pageWaitTime, pageWaitTime);
                    /*if (null == infoRaw1.texture)
                    {
                        pageIndex = 0;
                        infoRaw1.texture = data.infoTextures[0];
                        infoRaw1.color = Color.white;
                        infoRaw0.color = Color.clear;
                        InvokeRepeating("PageAnimation", pageWaitTime, pageWaitTime);
                    }
                    else
                    {
                        pageIndex = -1;
                        InvokeRepeating("PageAnimation", 0, pageWaitTime);
                    }*/
                }
                else
                {
                    Debug.LogError("数据错误");
                }
            }
        }

    }

    //切换图片动画
    private void PageAnimation()
    {
        var data = currentButtonTab.DataTab;
        pageIndex++;
        if(pageIndex >= data.infoTextures.Count)
        {
            pageIndex = 0;
        }

        infoRaw0.texture = infoRaw1.texture;
        infoRaw1.texture = data.infoTextures[pageIndex];
        infoRaw0.color = Color.white;
        infoRaw1.color = new Color(1, 1, 1, 0);
        tweenAnimation = DOTween.To(() => 0f, (lerp) =>
        {
            infoRaw0.color = Color.Lerp(Color.white, new Color(1, 1, 1, 0), lerp);
            infoRaw1.color = Color.Lerp(new Color(1, 1, 1, 0), Color.white, lerp);
        }, 1f, 0.6f).OnComplete(() =>
        {
            infoRaw0.color = new Color(1, 1, 1, 0);
            infoRaw1.color = Color.white;
        }).SetEase( Ease.InOutSine);


    }

}
