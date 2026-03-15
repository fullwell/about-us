using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MenuM3_DetailType
{
    none,
    model,
    feature,
    parameter,
    size,
    application,
    suit,
    reset,
}
public class MenuM3_Detail : MenuParent
{
    public DataM2_A2_Tab_Product DataProduct { get; set; }
    public MenuM3_DetailType CurrentDetailType { get; set; } = MenuM3_DetailType.model;

    private RawImage rawModel;
    private RawImage rawDetail;
    private bool tweening = false;
    public override void Init()
    {
        base.Init();
        rawModel = transform.Find("model").GetComponent<RawImage>();
        rawDetail = transform.Find("detail").GetComponent<RawImage>();
    }
    public override void Show()
    {
        base.Show();
        
        rawModel.texture = DataProduct.model;
        rawModel.SetNativeSize();
        rawModel.rectTransform.anchoredPosition = new Vector2(0, 0);

        if(MenuM3_DetailType.none == CurrentDetailType)
        {   // 初始化
            rawModel.texture = DataProduct.model;
            rawModel.SetNativeSize();
            rawModel.gameObject.SetActive(true);
            rawDetail.gameObject.SetActive(false);
        }
        else if(MenuM3_DetailType.model == CurrentDetailType 
            || MenuM3_DetailType.reset == CurrentDetailType)
        {
            TweenHideDetail();
        }
        else
        {
            TweenShowDetail();
        }


    }

    private void TweenHideDetail()
    {
        if(tweening || !rawDetail.gameObject.activeSelf)
        { return; }
        DOTween.To(() => 0f, (v) =>
        {
            rawDetail.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, v);
        }, 1f, 0.3f).SetEase( Ease.Linear).OnComplete(() =>
        {
            rawDetail.gameObject.SetActive(false);
            rawDetail.texture = null;
            tweening = false;
        });
    }
    private void TweenShowDetail()
    {
        if (tweening)
        { return; }

        Texture2D texture = null;
        switch (CurrentDetailType)
        {
            case MenuM3_DetailType.feature: texture = DataProduct.feature; break;
            case MenuM3_DetailType.parameter: texture = DataProduct.parameter; break;
            case MenuM3_DetailType.size: texture = DataProduct.size; break;
            case MenuM3_DetailType.application: texture = DataProduct.application; break;
            case MenuM3_DetailType.suit: texture = DataProduct.suit; break;
        }

        if(texture == rawDetail.texture)
        { return; }

        tweening = true;
        if (rawDetail.gameObject.activeSelf)
        {
            DOTween.To(()=>0f, (v) =>
            {
                float lerp = Mathf.Sin(Mathf.PI * v);
                rawDetail.color = Color.Lerp(Color.white, new Color(1, 1, 1, 0), lerp);
                if(rawDetail.texture != texture && 0.5f < v)
                {
                    rawDetail.texture = texture;
                    rawDetail.SetNativeSize();
                }
            }, 1f, 0.3f).OnComplete(() =>
            {
                tweening = false;
            });
        }
        else
        {
            rawDetail.gameObject.SetActive(true);
            rawDetail.texture = texture;
            rawDetail.SetNativeSize();
            rawDetail.transform.localScale = Vector3.zero;
            DOTween.To(() => 0f, (v) =>
            {
                rawDetail.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, v);
            }, 1f, 0.3f).SetEase( Ease.Linear).OnComplete(() =>
            {
                tweening = false;
            });
        }



    }

}
