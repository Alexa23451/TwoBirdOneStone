using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LocalisationManager : BaseManager<LocalisationManager>
{
    public Action OnLanguageChange;

    private const string PATH = "LocalisationData";

    private Dictionary<Message, string> _messagesKeyValuePairs;

    private Dictionary<Language, string> _shortNames;

    public override void Init()
    {
        _messagesKeyValuePairs = new Dictionary<Message, string>();
        _shortNames = new Dictionary<Language, string>();

        foreach (var data in Resources.LoadAll<MessagesData>(PATH))
            _shortNames.Add(data.Language, data.ShortName);
    }

    public void SetLanguage(Language language)
    {
        _messagesKeyValuePairs.Clear();

        foreach (var data in Resources.LoadAll<MessagesData>(PATH))
            if (data.Language == language)
            {
                data.Init();
                _messagesKeyValuePairs = new Dictionary<Message, string>(data.Messages);
            }

        OnLanguageChange?.Invoke();
    }

    public static string GetString(Message key)
    {
        string message = "";
        if (Instance._messagesKeyValuePairs.ContainsKey(key))
            message = Instance._messagesKeyValuePairs[key];

        return message;
    }

    public string GetShortName(Language language)
    {
        string shortName = "";

        if (_shortNames.ContainsKey(language))
            shortName = _shortNames[language];

        return shortName;
    }
}