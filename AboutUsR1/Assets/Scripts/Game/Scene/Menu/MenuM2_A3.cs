using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuM2_A3 : MenuParent
{
    [SerializeField]
    GameObject prefabClassify;
    [SerializeField]
    GameObject prefabProduct;
    [SerializeField]
    RectTransform classifyContent;
    [SerializeField]
    private Toggle toggleWarn0_10;
    [SerializeField]
    private Toggle toggleWarn10_40;
    [SerializeField]
    private Toggle toggleWarn40_77;
    [SerializeField]
    private Toggle toggleWarn40;
    [SerializeField]
    private Toggle toggleCool0_1;
    [SerializeField]
    private Toggle toggleCool1_10;
    [SerializeField]
    private Toggle toggleCool10_100;
    [SerializeField]
    private Toggle toggleCool100;

    //所有产品数据
    List<DataM2_A2_Tab_Product> products = new List<DataM2_A2_Tab_Product>();
    //分类大组
    Dictionary<string, Classify> itemClassifys = new Dictionary<string, Classify>();

    public override void Init()
    {
        base.Init();
        prefabClassify.SetActive(false);
        prefabProduct.SetActive(false);
        products.AddRange(Main.Instance.transform.GetComponentsInChildren<DataM2_A2_Tab_Product>(true));
        //开关事件
        List<Toggle> toggles = new List<Toggle>();
        toggles.AddRange(transform.GetComponentsInChildren<Toggle>());
        toggles.ForEach((v) =>
        {
            v.onValueChanged.AddListener((isOn) =>
            {
                Flush();
            });
        });
        //初始化
        products.ForEach((v) =>
        {
            //初始化预先生成分类Item
            if (!itemClassifys.ContainsKey(v.classify))
            {
                var o = GameObject.Instantiate(prefabClassify, classifyContent);
                o.SetActive(false);
                o.transform.localScale = Vector3.one;
                o.transform.Find("Text").GetComponent<Text>().text = v.classify;
                Classify classify = new Classify()
                {
                    Name = v.classify,
                    Root = o.GetComponent<RectTransform>(),
                    parentProduct = o.transform.Find("Products").GetComponent<RectTransform>(),
                    itemProducts = new List<Product>(),
                };
                itemClassifys.Add(v.classify, classify);
            }
            //初始化预先生成产品Item
            Classify itemClassify = itemClassifys[v.classify];
            Product product = new Product();
            product.Root = GameObject.Instantiate(prefabProduct, itemClassify.parentProduct);
            product.Root.transform.localScale = Vector3.one;
            product.Root.SetActive(false);
            product.raw = product.Root.transform.GetComponent<RawImage>();
            product.raw.texture = v.head;
            product.raw.SetNativeSize();
            product.data = v;
            itemClassify.itemProducts.Add(product);
        });

    }
    public override void Show()
    {
        base.Show();
        toggleWarn0_10.SetIsOnWithoutNotify(true);
        toggleWarn10_40.SetIsOnWithoutNotify(false);
        toggleWarn40_77.SetIsOnWithoutNotify(false);
        toggleWarn40.SetIsOnWithoutNotify(false);
        toggleCool0_1.SetIsOnWithoutNotify(true);
        toggleCool1_10.SetIsOnWithoutNotify(false);
        toggleCool10_100.SetIsOnWithoutNotify(false);
        toggleCool100.SetIsOnWithoutNotify(false);
        Flush();
    }
    public override void Hide()
    {
        base.Hide();
        toggleWarn0_10.SetIsOnWithoutNotify(false);
        toggleWarn10_40.SetIsOnWithoutNotify(false);
        toggleWarn40_77.SetIsOnWithoutNotify(false);
        toggleWarn40.SetIsOnWithoutNotify(false);
        toggleCool0_1.SetIsOnWithoutNotify(false);
        toggleCool1_10.SetIsOnWithoutNotify(false);
        toggleCool10_100.SetIsOnWithoutNotify(false);
        toggleCool100.SetIsOnWithoutNotify(false);
        Flush();
    }
    private void Flush()
    {
        List<Product> allProducts = new List<Product>();
        List<Product> hideProducts = new List<Product>();
        foreach (var itemClassify in itemClassifys)
        {
            allProducts.AddRange(itemClassify.Value.itemProducts);
        }
        if (!toggleWarn0_10.isOn)
        {
            allProducts.ForEach((v) =>
            {
                if (v.data.warmArea <= 10)
                {
                    //v.Root.SetActive(false);
                    hideProducts.Add(v);
                }
            });
        }
        if (!toggleWarn10_40.isOn)
        {
            allProducts.ForEach((v) =>
            {
                if (10 <= v.data.warmArea && v.data.warmArea <= 10)
                {
                    //v.Root.SetActive(false);
                    hideProducts.Add(v);
                }
            });
        }
        if (!toggleWarn40_77.isOn)
        {
            allProducts.ForEach((v) =>
            {
                if (40 <= v.data.warmArea && v.data.warmArea <= 77)
                {
                    //v.Root.SetActive(false);
                    hideProducts.Add(v);
                }
            });
        }
        if (!toggleWarn40.isOn)
        {
            allProducts.ForEach((v) =>
            {
                if (40 <= v.data.warmArea)
                {
                    //v.Root.SetActive(false);
                    hideProducts.Add(v);
                }
            });
        }

        if (!toggleCool0_1.isOn)
        {
            allProducts.ForEach((v) =>
            {
                if (v.data.coolingCapacity <= 1)
                {
                    //v.Root.SetActive(false);
                    hideProducts.Add(v);
                }
            });
        }
        if (!toggleCool1_10.isOn)
        {
            allProducts.ForEach((v) =>
            {
                if (1 <= v.data.coolingCapacity && v.data.coolingCapacity <= 10)
                {
                    //v.Root.SetActive(false);
                    hideProducts.Add(v);
                }
            });
        }
        if (!toggleCool10_100.isOn)
        {
            allProducts.ForEach((v) =>
            {
                if (10 <= v.data.coolingCapacity && v.data.coolingCapacity <= 100)
                {
                    //v.Root.SetActive(false);
                    hideProducts.Add(v);
                }
            });
        }
        if (!toggleCool100.isOn)
        {
            allProducts.ForEach((v) =>
            {
                if (100 <= v.data.coolingCapacity)
                {
                    //v.Root.SetActive(false);
                    hideProducts.Add(v);
                }
            });
        }

        allProducts.ForEach((v) =>
        {
            v.Root.SetActive(!hideProducts.Contains(v));
        });
        foreach (var itemClassify in itemClassifys.Values)
        {
            bool isShow = false;
            foreach (var itemProduct in itemClassify.itemProducts)
            {
                if (itemProduct.Root.activeSelf)
                {
                    isShow = true;
                    break;
                }
            }
            itemClassify.Root.gameObject.SetActive(isShow);
            LayoutRebuilder.ForceRebuildLayoutImmediate(itemClassify.parentProduct);
            LayoutRebuilder.ForceRebuildLayoutImmediate(itemClassify.Root);

        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(classifyContent);
    }


    public class Classify
    {
        public string Name;
        public RectTransform Root;
        public RectTransform parentProduct;
        public List<Product> itemProducts;
    }
    public class Product
    {
        public DataM2_A2_Tab_Product data;
        public GameObject Root;
        public RawImage raw;
    }
}
