using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module.Achievement.Data;
using Module.Achievement.Tool;

using UnityEditor;

[CustomPropertyDrawer(typeof(IdRangeAttribute))]
public class IdRangeAttributeEditor : PropertyDrawer
{
    public static string[] idNames;
    public static int[] ids;


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var target = (IdRangeAttribute)this.attribute;

        if(target.SupportType == typeof(AchievementData))
        {
            idNames = AchievementTool.GetDisplayNames().ToArray();
            ids = AchievementTool.GetIDs().ToArray();
        }
        else {
            idNames = ActivityID.GetDisplayString;
            ids = ActivityID.GetInt;
        }


        GUI.enabled = ((IdRangeAttribute)this.attribute).IsEditable;

        property.intValue = EditorGUI.IntPopup( position, "id", property.intValue, idNames, ids);

        GUI.enabled = true;
    }
}
