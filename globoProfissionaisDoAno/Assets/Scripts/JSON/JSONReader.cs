using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public static class JSONReader
{
    public static List<VidUnit> VideosList()
    {
        string jsonString = ReadJSON();

        var heroRoot = JsonUtility.FromJson<VidList>(jsonString);

        return heroRoot.Vids;
    }

    public static string ReadJSON()
    {
        return File.ReadAllText(Application.dataPath + "/StreamingAssets/" + "hNacional.json");
    }
}


