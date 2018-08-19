/*
*  Copyright Jireh Chew / fractalD
*
*
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LocalisationDictionary : SerializableDictionary<string, string> { }

[CreateAssetMenu]
public class Localisation : ScriptableObject   
{
    public LocalisationDictionary localDict = new LocalisationDictionary();
    string missingKeyText = "KeyNotFound";	

    public string Translate(string key)
    {
       if (localDict.ContainsKey(key))
       {
          return localDict[key];
       } else
       {
          return missingKeyText;
       }
    }

    public void SetTranslation(string key, string value)
    {

    }

    public void AddKeyValuePairTranslation(string key, string value)
    {

    }

    public void RemoveKeyValuePairTranslation(string key, string value)
    {

    }


}
