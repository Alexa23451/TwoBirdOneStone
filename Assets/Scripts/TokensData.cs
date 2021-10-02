using System.Collections.Generic;
using UnityEngine;

namespace Score
{
    class TokensData : ScriptableObject
    {
        public List<int> Tokens = new List<int>();
        public List<string> FoundTokensHash = new List<string>();
    }
}