using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MessagesData", menuName = "Localisation/MessagesData", order = 2)]
public class MessagesData : ScriptableObject
{
    public Language Language => _language;
    public string ShortName => _shortName;

    public Dictionary<Message, string> Messages => _messagesKeyValuePairs;

    [SerializeField] private Language _language = Language.ENGLISH;
    [SerializeField] private string _shortName = "UK";

    [SerializeField] private List<MessageKeyValuePair> _messages = new List<MessageKeyValuePair>();
    private Dictionary<Message, string> _messagesKeyValuePairs;

    public void Init()
    {
        _messagesKeyValuePairs = new Dictionary<Message, string>();
        foreach (var message in _messages)
            _messagesKeyValuePairs.Add(message.key, message.value);
    }

#if UNITY_EDITOR
    private void OnEnable()
    {
        var count = Enum.GetValues(typeof(Message)).Cast<Message>().Count();
        if (count > _messages.Count)
            for (int i = _messages.Count; i < count; i++)
                _messages.Add(new MessageKeyValuePair { key = (Message)i, value = "" });
    }
#endif

    [Serializable]
    private class MessageKeyValuePair
    {
        public Message key;

        [Multiline]
        public string value;
    }
}