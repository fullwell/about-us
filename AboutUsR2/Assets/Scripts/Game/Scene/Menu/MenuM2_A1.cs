using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuM2_A1 : MenuParent
{
    List<DataDaJiShi> datas = new List<DataDaJiShi>();

    CanvasGroup view;
    Text title;
    Text year;
    RawImage content;
    Button btnNext;
    Button btnPrev;

    private CanvasGroup copy { get; set; }
    public int CurrentPageIndex { get; set; } = 0;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {

        }
        if (Input.GetKeyDown(KeyCode.O))
        {

        }
    }

    public override void Init()
    {
        base.Init();
        view = transform.Find("View").GetComponent< CanvasGroup>();
        title = transform.Find("View/Title").GetComponent<Text>();
        year = transform.Find("View/Year").GetComponent<Text>();
        content = transform.Find("View/Content").GetComponent<RawImage>();

        datas.AddRange(Main.Instance.transform.GetComponentsInChildren<DataDaJiShi>());

        CurrentPageIndex = datas.Count - 1;

        btnNext = transform.Find("BtnNext").GetComponent<Button>();
        btnPrev= transform.Find("BtnPrev").GetComponent<Button>();
        btnNext.onClick.AddListener(OnNext);
        btnPrev.onClick.AddListener(OnPrev);

    }

    public override void Show()
    {
        CancelInvoke();
        if (null != copy)
        {
            Destroy(copy.gameObject);
        }
        SetupData();
        view.transform.localPosition = Vector3.zero;
        StartAnimtaion();
    }
    public override void Hide()
    {
        base.Hide();
        CancelInvoke();
    }
    private void SetupData()
    {
        FlushBtn();
        var data = datas[CurrentPageIndex];
        view.alpha = 1;
        title.text = data.title;
        year.text = data.year;
        content.texture = data.introduce;
        content.SetNativeSize();
    }
    private void TweenView(bool move_to_left)
    { 
        var o = GameObject.Instantiate(view.gameObject, transform);
        copy = o.GetComponent<CanvasGroup>();
        copy.alpha = 1;
        copy.transform.localPosition  = new Vector3(0, 0, 0);
        SetupData();
        view.alpha = 0f;
        var posLeft = new Vector3(-1080, 0, 0);
        var posRight = new Vector3(1080, 0, 0);
        DOTween.To(() => 0f, (v) =>
        {
            if (move_to_left)
            {
                copy.transform.localPosition = Vector3.Lerp(Vector3.zero, posLeft, v);
                view.transform.localPosition = Vector3.Lerp(posRight, Vector3.zero, v);
            }
            else
            {
                copy.transform.localPosition = Vector3.Lerp(Vector3.zero, posRight, v);
                view.transform.localPosition = Vector3.Lerp(posLeft, Vector3.zero, v);
            }
            copy.alpha = 1 - v;
            view.alpha = v;
        }, 1f, 0.4f).OnComplete(() =>
        {
            view.alpha = 1;
            Destroy(copy.gameObject);
        });
    }
    private void FlushBtn()
    {
        btnNext.gameObject.SetActive(CurrentPageIndex < datas.Count - 1);
        btnPrev.gameObject.SetActive(CurrentPageIndex > 0);
    }
    private void OnNext()
    {
        bool move_to_left = PageIndexUpdate(true);
        TweenView(move_to_left);
        FlushBtn();
        StartAnimtaion();
    }
    private void OnPrev()
    {
        bool move_to_left = PageIndexUpdate(false);
        TweenView(move_to_left);
        FlushBtn();
        StartAnimtaion();
    }



    private bool PageIndexUpdate(bool increase)
    {
        bool move_to_left;
        if (increase)
        {
            CurrentPageIndex++;
            move_to_left = true;
        }
        else
        {
            CurrentPageIndex--;
            move_to_left = false;
        }
        if(CurrentPageIndex < 0)
        {
            CurrentPageIndex = datas.Count - 1;
            move_to_left = true;
        }
        if(CurrentPageIndex > datas.Count - 1)
        {
            CurrentPageIndex = 0;
            move_to_left = false;
        }
        return move_to_left;
    }
    private void StartAnimtaion()
    {
        CancelInvoke();
        InvokeRepeating("AnimationUpdate", 2, 2);
    }
    private void AnimationUpdate()
    {
        bool move_to_left = PageIndexUpdate(false);
        TweenView(move_to_left);
        FlushBtn();
    }

}
