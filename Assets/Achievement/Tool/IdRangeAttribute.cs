using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdRangeAttribute : PropertyAttribute
{
    public bool IsEditable { get; set; } = true;

    public System.Type SupportType { get; set; }
}