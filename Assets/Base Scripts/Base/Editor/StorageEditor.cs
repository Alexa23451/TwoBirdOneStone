using UnityEditor;
using UnityEngine;

public static class StorageEditor
{
    [MenuItem("Tools/Deletes/ALL DATA")]
    public static void DeleteAllData()
    {
        RuntimeStorageData.DeleteData(RuntimeStorageData.DATATYPE.PLAYER);
        RuntimeStorageData.DeleteData(RuntimeStorageData.DATATYPE.SOUND);
        PlayerPrefs.DeleteAll();
    }
}
