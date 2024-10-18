using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ResourcesLoader : MonoBehaviour
{
    public static ResourcesLoader Instance;


    Dictionary<string, Object> loaded = new Dictionary<string, Object>();

    void Awake()
    {
        Instance = this;
    }

    public Object Load(string path)
    {
        // if (!File.Exists(path))
        // {
        //     Debug.LogError($"Failed to load \"{path}\". Path/File does not exist.");
        //     return null;
        // }

        // Debug.Log($"Loading resources:{path}");
        if (loaded.ContainsKey(path))
            return loaded[path];

        var resource = Resources.Load(path);

        if (resource == null) return null;

        loaded.Add(path, resource);

        return resource;
    }

    public void Unload(string path)
    {
        // if (!File.Exists(path))
        // {
        //     Debug.LogWarning($"Failed to load \"{path}\". Path/File does not exist.");
        //     return;
        // }

        if (!loaded.ContainsKey(path))
            return;
        
        // Debug.Log($"Unloading resources:{path}");
        Resources.UnloadAsset(loaded[path]);
    }
}