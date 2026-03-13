using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Main : MonoBehaviour
{
    public static Main Instance;
    public static List<MenuParent> MenuParents => Instance.menuParents;


    List<MenuParent> menuParents = new List<MenuParent>();


    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        menuParents.AddRange(transform.GetComponentsInChildren<MenuParent>(true));
        menuParents.ForEach((m) =>
        {
            m.Init();
            m.gameObject.SetActive(false);
        });
        menuParents[0].gameObject.SetActive(true);




    }


}
