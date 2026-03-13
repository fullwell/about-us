using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class R : MonoBehaviour
{
    private static R instance;
    void Awake()
    {
        instance = this;
        if(null == atlas)
        {
            atlas = Resources.Load<SpriteAtlas>("Sprite/Sprite");
        }
        Debug.Log("R.Init");
    }

    #region ÕººØ
    public SpriteAtlas atlas;
    public static Sprite GetSprite(string name)
    {
        return instance.atlas.GetSprite(name);
    }
    #endregion


    #region …˘“Ù
    private AudioSource audioBg;
    private List<AudioSource> audioEffect = new List<AudioSource>();
    public static void PlayBg(string name)
    {
        if(null == instance.audioBg)
        {
            instance.audioBg = instance.gameObject.AddComponent<AudioSource>();
            instance.audioBg.loop = true;
            instance.audioBg.playOnAwake = false;
            instance.audioBg.volume = D.volumeBg;
        }

        var clip = Resources.Load<AudioClip>("Audio/" + name);
        if(clip != instance.audioBg.clip)
        {
            instance.audioBg.clip = clip;
            instance.audioBg.Play();
            //Debug.Log("PlayMusicBg : " + name);
        }

    }
    public static void StopBg()
    {
        if(null != instance.audioBg)
        {
            if(instance.audioBg.isPlaying)
            {
                instance.audioBg.Stop();
            }
        }
    }
    public static float PlayEffect(string name)
    {
        AudioSource audio = null;
        for (int i = instance.audioEffect.Count - 1; i >= 0; i--)
        {
            if(!instance.audioEffect[i].isPlaying)
            {
                audio = instance.audioEffect[i];
                break;
            }
        }
        if(null == audio)
        {
            audio = instance.gameObject.AddComponent<AudioSource>();
            instance.audioEffect.Add(audio);
            audio.loop = false;
            audio.playOnAwake = false;
            audio.volume = D.volumeEffect;
        }

        audio.clip = Resources.Load<AudioClip>("Audio/" + name);
        audio.Play();
        //Debug.Log("PlayMusicEffect : " + name);

        return audio.clip.length;
    }
    public static void PlayEffect(string name, float delay, System.Action action)
    {
        float time = PlayEffect(name);
        instance.StartCoroutine(instance.TDelayCall(delay, action));
    }
    
         

    #endregion

    public static Image CreateImage(string SpriteName, Transform Parent)
    {
        var o = new GameObject(SpriteName);
        o.transform.parent = Parent;
        o.transform.localPosition = Vector3.zero;
        o.transform.localScale = Vector3.one;
        var image = o.AddComponent<Image>();
        image.sprite = GetSprite(SpriteName);
        image.SetNativeSize();
        return image;
    }
    public static Text CreateText(string fontResourcePath, string objName, Transform Parent, TextAnchor textAnchor)
    {
        var o = new GameObject(objName);
        o.transform.parent = Parent;
        o.transform.localPosition = Vector3.zero;
        o.transform.localScale = Vector3.one;
        var text = o.AddComponent<Text>();
        text.font = Resources.Load<Font>(fontResourcePath);
        text.color = Color.white;
        text.alignment = textAnchor;
        text.horizontalOverflow = HorizontalWrapMode.Overflow;
        text.verticalOverflow = VerticalWrapMode.Overflow;
        text.raycastTarget = false;
        return text;
    }

    IEnumerator TDelayCall(float delay, System.Action action)
    {
        yield return new WaitForSeconds(delay);
        action.Invoke();
    }

}
