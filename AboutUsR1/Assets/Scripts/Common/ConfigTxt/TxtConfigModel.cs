using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TxtConfigModel
{
    bool isInit = false;
    string ConfigPath
    {
        get
        {
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                return Application.dataPath.Replace("Assets", "config.txt");
            }
            else
            {
                string parent = Directory.GetParent(Application.dataPath).FullName;
                return Path.Combine(parent, "config.txt");
            }
        }
    }


    public void InitTxtConfig()
    {
        if(isInit) { return; }
        isInit = true;

        if (!File.Exists(ConfigPath))
        {
            var sw = File.CreateText(ConfigPath);
            var data = GetData();
            sw.Write(data);
            sw.Close();
            sw.Dispose();
        }
        SetDefaultValue();
        ReadTextValue();
        Debug.Log(ConfigPath);
    }

    private string GetData()
    {
        var fields = this.GetType().GetFields(
            System.Reflection.BindingFlags.NonPublic 
            | System.Reflection.BindingFlags.Public
            | System.Reflection.BindingFlags.Instance);
        string result = "";
        foreach (var field in fields)
        {
            {
                var atts = field.GetCustomAttributes(typeof(BoolAttribute), true);
                if (0 < atts.Length)
                {
                    var attBool = (BoolAttribute)atts[0];
                    result += $"//{attBool.Note}，默认值({attBool.Value})" +
                        $"\n{field.Name}={attBool.Value}\n\n";
                    //Debug.LogError(field.Name +", " + attBool.Note + ", " + attBool.Value);
                }
            }
            {
                var atts = field.GetCustomAttributes(typeof(FloatAttribute), true);
                if (0 < atts.Length)
                {
                    var attFloat = (FloatAttribute)atts[0];
                    result += $"//{attFloat.Note}，默认值({attFloat.Value})" +
                        $"\n{field.Name}={attFloat.Value}\n\n";
                    //Debug.LogError(field.Name +", " + attFloat.Note + ", " + attFloat.Value);
                }
            }
            {
                var atts = field.GetCustomAttributes(typeof(IntAttribute), true);
                if (0 < atts.Length)
                {
                    var attInt = (IntAttribute)atts[0];
                    result += $"//{attInt.Note}，默认值({attInt.Value})" +
                        $"\n{field.Name}={attInt.Value}\n\n";
                    //Debug.LogError(field.Name +", " + attInt.Note + ", " + attInt.Value);
                }
            }
            {
                var atts = field.GetCustomAttributes(typeof(StringAttribute), true);
                if (0 < atts.Length)
                {
                    var attString = (StringAttribute)atts[0];
                    result += $"//{attString.Note}，默认值({attString.Value})" +
                        $"\n{field.Name}={attString.Value}\n\n";
                    //Debug.LogError(field.Name +", " + attString.Note + ", " + attString.Value);
                }
            }
        }
        Debug.LogError(result);
        return result;
    }
    private void SetDefaultValue()
    {
        var fields = this.GetType().GetFields(
            System.Reflection.BindingFlags.NonPublic
            | System.Reflection.BindingFlags.Public
            | System.Reflection.BindingFlags.Instance);
        foreach (var field in fields)
        {
            {
                var atts = field.GetCustomAttributes(typeof(BoolAttribute), true);
                if (0 < atts.Length)
                {
                    var attBool = (BoolAttribute)atts[0];
                    field.SetValue(this, attBool.Value);
                }
            }
            {
                var atts = field.GetCustomAttributes(typeof(FloatAttribute), true);
                if (0 < atts.Length)
                {
                    var attFloat = (FloatAttribute)atts[0];
                    field.SetValue(this, attFloat.Value);
                }
            }
            {
                var atts = field.GetCustomAttributes(typeof(IntAttribute), true);
                if (0 < atts.Length)
                {
                    var attInt = (IntAttribute)atts[0];
                    field.SetValue(this, attInt.Value);
                }
            }
            {
                var atts = field.GetCustomAttributes(typeof(StringAttribute), true);
                if (0 < atts.Length)
                {
                    var attString = (StringAttribute)atts[0];
                    field.SetValue(this, attString.Value);
                }
            }
        }
    }
    private void ReadTextValue()
    {
        //收集Config数据
        Dictionary<string, string> txtValues = new Dictionary<string, string>();
        foreach(string line in File.ReadAllLines(ConfigPath))
        {
            if (string.IsNullOrEmpty(line))
                continue;
            if (line.Contains("//"))
                continue;
            if (!line.Contains("="))
                continue;
            string[] strs = line.Replace(" ", "").Split('=');
            if (null == strs || 2 != strs.Length)
                continue;
            if (!txtValues.ContainsKey(strs[0]))
            {
                txtValues.Add(strs[0], strs[1]);
            }
        }
        //更新数值
        var fields = this.GetType().GetFields(
            System.Reflection.BindingFlags.NonPublic
            | System.Reflection.BindingFlags.Public
            | System.Reflection.BindingFlags.Instance);


        foreach (var field in fields)
        {
            if (!txtValues.ContainsKey(field.Name))
            {
                continue;
            }
            {
                var atts = field.GetCustomAttributes(typeof(BoolAttribute), true);
                if (0 < atts.Length)
                {
                    if (bool.TryParse(txtValues[field.Name], out bool v))
                    {
                        field.SetValue(this, v);
                    }
                }
            }
            {
                var atts = field.GetCustomAttributes(typeof(FloatAttribute), true);
                if (0 < atts.Length)
                {
                    if (float.TryParse( txtValues[field.Name], out float v))
                    {
                        field.SetValue(this, v);
                    }
                }
            }
            {
                var atts = field.GetCustomAttributes(typeof(IntAttribute), true);
                if (0 < atts.Length)
                {
                    if (int.TryParse(txtValues[field.Name], out int v))
                    {
                        field.SetValue(this, v);
                    }
                }
            }
            {
                var atts = field.GetCustomAttributes(typeof(StringAttribute), true);
                if (0 < atts.Length)
                {
                    field.SetValue(this, txtValues[field.Name]);
                }
            }
        }

    }


}
