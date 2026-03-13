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
using JetBrains.Annotations;

public class MenuM2_A2 : MenuParent
{
    List<ButtonM2_A2_Left> buttonLefts = new List<ButtonM2_A2_Left>();
    List<ButtonM2_A2_Tab> buttonTabs = new List<ButtonM2_A2_Tab>();
    List<ButtonM2_A2_Product> buttonProducts = new List<ButtonM2_A2_Product>();
    ButtonM2_A2_Left currentButtonLeft;
    ButtonM2_A2_Tab currentButtonTab;
    ButtonM2_A2_Product currentButtonProduct;


    public override void Init()
    {
        base.Init();
        
        //初始化 button Lefts
        buttonLefts.AddRange( transform.GetComponentsInChildren<ButtonM2_A2_Left>());
        //批量创建button Tab
        var prefabTab = transform.Find("Right/Tabs/prefab");
        for(int i  =0; i < 10; i++)
        {
            var o = GameObject.Instantiate(prefabTab.gameObject, transform.Find("Right/Tabs"));
            o.transform.localScale = Vector3.one;
            o.SetActive(false);
            var com = o.AddComponent<ButtonM2_A2_Tab>();
            com.Init();
            buttonTabs.Add(com);
        }
        prefabTab.gameObject.SetActive(false);
        //批量创建button Product
        var prefabProduct = transform.Find("Right/Products/prefab");
        for (int i = 0; i < 24; i++)
        {
            var o = GameObject.Instantiate(prefabProduct.gameObject, transform.Find("Right/Products"));
            o.transform.localScale = Vector3.one;
            o.SetActive(false);
            var com = o.AddComponent<ButtonM2_A2_Product>();
            com.Init();
            buttonProducts.Add(com);
        }
        prefabProduct.gameObject.SetActive(false);

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

    public void InitView(ButtonM2_A2_Left buttonLeft)
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
                buttonTabs[i].DataTab = new DataM2_A2_Tab();
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
    public void SetupTabView(ButtonM2_A2_Tab buttonTab)
    {
        if (currentButtonTab == buttonTab)
            return;
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
            }
        }
        //装载button Product数据
        List<DataM2_A2_Tab_Product> dataProducts = buttonTab.DataTab.products;
        for (int i = 0; i < buttonProducts.Count; i++)
        {
            var btn_i = buttonProducts[i];
            if(i >= dataProducts.Count)
            {
                btn_i.gameObject.SetActive(false);
                btn_i.DataProduct = null;
            }
            else
            {
                btn_i.gameObject.SetActive(true);
                btn_i.DataProduct = dataProducts[i];
            }
        }
        //刷新Product视图
        currentButtonProduct = null;
        SetupProductView(buttonProducts[0]);

    }

    //切换图片动画
    public void SetupProductView(ButtonM2_A2_Product buttonProdrct)
    {
        if (currentButtonProduct == buttonProdrct)
            return;
        currentButtonProduct = buttonProdrct;

        for (int i = 0; i < buttonProducts.Count; i++)
        {
            var btn_i = buttonProducts[i];
            var data = btn_i.DataProduct;
            if (!btn_i.gameObject.activeSelf)
                break;
            //更新Tab标题
            btn_i.GetComponentInChildren<Text>().text = data.tabTitle.Replace('|', '\n');
            //更新Tab颜色
            if (btn_i != buttonProdrct)
            {
                btn_i.GetComponent<Image>().color = D.colorTab2;
            }
            else
            {
                btn_i.GetComponent<Image>().color = D.colorTab3;
            }
        }


    }

}
