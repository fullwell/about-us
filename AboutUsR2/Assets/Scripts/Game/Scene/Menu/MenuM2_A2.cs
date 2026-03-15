using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuM2_A2 : MenuParent
{
    public GameObject prefab;
    public RectTransform Content;

    List<Picture> datas = new List<Picture>();

    float radius = 1000;
    float rad = 15 * Mathf.Deg2Rad;
    Vector3 roundBegin = Vector3.zero;
    Vector3 roundEnd = Vector3.zero;
    Vector3 roundCurrent = Vector3.zero;


    void OnEnable()
    {
        StartCoroutine("TUpdate");
    }
    public override void Init()
    {
        base.Init();
        prefab.SetActive(false);
        var datas = Main.Instance.GetComponentsInChildren<DataZhanHui>();
        float totalRad = (datas.Length - 1)* rad;
        float countRad = totalRad * -0.5f;
        roundBegin = new Vector3(0, countRad * Mathf.Rad2Deg, 0);
        roundEnd = new Vector3(0, countRad * -Mathf.Rad2Deg, 0);

        foreach (var data in datas)
        {
            Picture pic = new Picture();
            pic.data = data;
            pic.root = GameObject.Instantiate(prefab, Content).GetComponent<RectTransform>();
            pic.root.gameObject.SetActive(true);
            var raw = pic.root.Find("Mask/Texture").GetComponent<RawImage>();
            raw.texture = data.Texture2D;

            raw.rectTransform.sizeDelta = new Vector2(1f * data.Texture2D.width / data.Texture2D.height * 600f, 600);
            
            pic.root.anchoredPosition3D = new Vector3(Mathf.Sin(countRad) * radius, 0, Mathf.Cos(countRad) * radius);
            pic.root.LookAt(Content);
            countRad += rad;
        }

    }
    public override void Show()
    {
        base.Show();
        roundCurrent = Vector3.zero;
    }
    private IEnumerator TUpdate()
    {
        bool start = false;
        float startpos = 0f;
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray, 2000))
                {
                    Debug.LogError("yes");
                    start = true;
                    startpos = Input.mousePosition.y;
                }
            }
            if(Input.GetMouseButtonUp(0))
            {
                start = false;
            }
            if (Input.GetMouseButtonUp(0) && start)
            {
                // Input.mousePosition.x - startpos;

            }


            Debug.LogError(Input.mousePosition);


            yield return null;
        }
    }

    class Picture
    {
        public DataZhanHui data;
        public RectTransform root;
    }

}
