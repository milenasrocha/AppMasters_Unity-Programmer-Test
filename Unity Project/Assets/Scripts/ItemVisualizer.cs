using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using AppMasters.Extensions.UI;
using AppMasters.UI.GameObjectRef;
using UnityEditor;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using AppMasters.Json;
using UnityEngine.SceneManagement;
using TMPro;

public class ItemVisualizer : MonoBehaviour
{
    #region vars
    [Header("Prefabs")]
    [SerializeField] GameObject uiItem; //"card"
    [SerializeField] TMP_Text title; //"card"

    [Header("UI Refs")]
    [SerializeField] string currentCollection; //debug/inspector purposes only
    [SerializeField] ToggleGroup collectionsToggleGroup;
    [SerializeField] Transform collectionsContent;
    Dictionary<string, JsonItem> collections = new Dictionary<string, JsonItem>();


    [Space(20)]
    [SerializeField] string currentItem; //debug/inspector purposes only
    [SerializeField] ToggleGroup itemsToggleGroup;
    [SerializeField] Transform itemsContent;
    Dictionary<string, Dictionary<string, JsonItem>> items = new Dictionary<string, Dictionary<string, JsonItem>>(); //example: itemsDic["autograph"]["autograph-01"]

    [Space(20)]
    [SerializeField] Transform itemSizeContent;
    [SerializeField] SizeUI itemSize;
    [SerializeField] float itemSizeWidth;
    [SerializeField] float itemSizeDepth;

    [Space(20)]
    [SerializeField] Transform renderContent;

    [Space(20)]
    [SerializeField] UnityEngine.UI.Button next;
    [SerializeField] UnityEngine.UI.Button back;
    [SerializeField] UnityEngine.UI.Button restart;




    JsonLoader jsonLoader;
    #endregion


    void Awake()
    {
        jsonLoader = new JsonLoader();
    }

    void Start()
    {
        collections = jsonLoader.StringToJsonItem("Database/collections", "collections");

        GoToCollectionScreen();
        // ChooseCollection("area");
        // ShowItems();


        // collectionsToggleGroup.toggleon.AddListener((tog) => ChooseCollection(tog.gameObject.name));
        // itemsToggleGroup.toggleon.AddListener((tog) => ChooseItem(tog.gameObject.name));
        collectionsToggleGroup.m_OnValueChanged.AddListener(() => ChooseCollection(collectionsToggleGroup.GetFirstActiveToggle()?.gameObject.name));
        itemsToggleGroup.m_OnValueChanged.AddListener(() => ChooseItem(itemsToggleGroup.GetFirstActiveToggle()?.gameObject.name));
        itemSize.width.onValueChanged.AddListener(ChooseSizeWidth);
        itemSize.depth.onValueChanged.AddListener(ChooseSizeDepth);


        next.onClick.AddListener(Next);
        back.onClick.AddListener(Back);
        restart.onClick.AddListener(Restart);
    }



    public void ChooseCollection(string id = null)
    {
        Debug.Log($"ChooseCollection {id}");

        // Catch errors
        // if (!collections.ContainsKey(id))
        // {
        //     Debug.LogError($"Collection key \"{id}\" does not exist.");
        //     return;
        // }

        currentCollection = id;

        if (collectionsContent.gameObject.activeSelf)
            next.interactable = id != null;

        title.text = $"{currentCollection}";

        if (id == null) return;


        if (!items.ContainsKey(id)) //if wasnt loaded yet
            items.Add(id, jsonLoader.StringToJsonItem($"Database/items/items-{id}", "items")); //auto load items in this collection


        Debug.Log($"collection {collections[id]}");
    }

    public void ChooseItem(string id = null)
    {
        Debug.Log($"ChooseItem {id}");
        // Catch errors
        // if (currentCollection == null)
        // {
        //     Debug.LogError($"No collection selected yet.");
        //     return;
        // }

        // if (!itemsDic[currentCollection].ContainsKey(id))
        // {
        //     Debug.LogError($"Collection \"{currentCollection}\" does not contain \"{id}\" item.");
        //     return;
        // }

        currentItem = id;

        if (itemsContent.gameObject.activeSelf)
            next.interactable = id != null;

        title.text = $"{currentCollection}/{currentItem}";

        if (id == null) return;


        Debug.Log($"collection {currentCollection} / item {items[currentCollection].ContainsKey(id)}");
    }

    public void ChooseSize(string width = null, string depth = null)
    {
        Debug.Log($"ChooseSize {width} | {depth}");
        // Catch errors
        // if (currentCollection == null)
        // {
        //     Debug.LogError($"No collection selected yet.");
        //     return;
        // }

        // if (!itemsDic[currentCollection].ContainsKey(id))
        // {
        //     Debug.LogError($"Collection \"{currentCollection}\" does not contain \"{id}\" item.");
        //     return;
        // }
        if (width != null && float.TryParse(width, out float w)) itemSizeWidth = w;
        if (depth != null && float.TryParse(depth, out float d)) itemSizeDepth = d;

        if (itemSizeWidth > 0 && itemSizeDepth > 0 && itemSizeContent.gameObject.activeSelf) next.interactable = true;

        Debug.Log($"width {width ?? "0"} | depth{depth ?? "0"}");
    }
    public void ChooseSizeWidth(string value) => ChooseSize(width: value);
    public void ChooseSizeDepth(string value) => ChooseSize(depth: value);



    public void UpdateCollections()
    {
        Debug.Log($"UpdateCollections {collectionsContent.childCount} ");
        if (collectionsContent.childCount != 0) return;

        foreach (var col in collections)
        {
            GameObject go = Instantiate(uiItem, collectionsContent).gameObject;


            JsonItemUI jsonItemUI = go.GetComponent<JsonItemUI>();
            string title = col.Key;
            title = title.First().ToString().ToUpper() + title.Substring(1); //"item" -> "Item"
            jsonItemUI.title.text = title;


            Toggle toggle = go.GetComponent<Toggle>();
            toggle.group = collectionsToggleGroup;

            go.name = col.Key;
        }

    }

    void UpdateItems() => UpdateItems(currentCollection);
    public void UpdateItems(string collection)
    {
        if (collection == null) return;

        for (int i = 0; i < itemsContent.childCount; i++)
            Destroy(itemsContent.GetChild(i).gameObject);

        foreach (var item in items[collection])
        {
            GameObject go = Instantiate(uiItem, itemsContent).gameObject;

            JsonItemUI jsonItemUI = go.GetComponent<JsonItemUI>();
            jsonItemUI.title.text = item.Value.title;

            Toggle toggle = go.GetComponent<Toggle>();
            toggle.group = itemsToggleGroup;

            go.name = item.Value.title;
        }
    }




    public void GoToCollectionScreen()
    {
        DisableAllScreens();

        UpdateCollections();
        // currentCollection = "";
        // currentItem = "";
        // itemSizeWidth = 0;
        // itemSizeDepth = 0;

        back.gameObject.SetActive(false);
        back.interactable = false;
        next.gameObject.SetActive(true);
        next.interactable = currentCollection != null;
        restart.gameObject.SetActive(false);

        title.text = $"{currentCollection}";
        collectionsContent.gameObject.SetActive(true);
    }

    public void GoToItemScreen()
    {
        DisableAllScreens();

        UpdateItems();
        // currentItem = "";
        // itemSizeWidth = 0;
        // itemSizeDepth = 0;

        back.gameObject.SetActive(true);
        back.interactable = true;
        next.gameObject.SetActive(true);
        next.interactable = currentItem != null;
        restart.gameObject.SetActive(false);

        title.text = $"{currentCollection}/{currentItem}";
        itemsContent.gameObject.SetActive(true);
    }

    public void GoToSizeScreen()
    {
        DisableAllScreens();

        // itemSizeWidth = 0;
        // itemSizeDepth = 0;

        back.gameObject.SetActive(true);
        next.interactable = true;
        next.gameObject.SetActive(true);
        next.interactable = itemSizeWidth > 0 && itemSizeDepth > 0;
        restart.gameObject.SetActive(false);


        itemSizeContent.gameObject.SetActive(true);
    }

    public void GoToRenderScreen()
    {
        DisableAllScreens();

        back.gameObject.SetActive(false);
        next.gameObject.SetActive(false);
        restart.gameObject.SetActive(true);

        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(0, -1000, 3);

        float max = 1;
        if (itemSizeWidth > max) max = itemSizeWidth;
        if (itemSizeDepth > max) max = itemSizeDepth;

        cube.transform.localScale = new Vector3(itemSizeWidth / max, 1 / max, itemSizeDepth / max);

        renderContent.gameObject.SetActive(true);
    }



    public void DisableAllScreens()
    {
        collectionsContent.gameObject.SetActive(false);
        itemsContent.gameObject.SetActive(false);
        itemSizeContent.gameObject.SetActive(false);
        renderContent.gameObject.SetActive(false);
    }


    public void Next()
    {
        if (itemSizeWidth != 0 && itemSizeDepth != 0 && itemSizeContent.gameObject.activeSelf)
        {
            GoToRenderScreen();
            return;
        }

        // go to item screen
        if (currentItem != "" && itemsContent.gameObject.activeSelf)
        {
            GoToSizeScreen();
            return;
        }

        // go to item screen
        if (currentCollection != "" && collectionsContent.gameObject.activeSelf)
        {
            GoToItemScreen();
            return;
        }
    }

    public void Back()
    {
        if (itemSizeContent.gameObject.activeSelf)
        {
            GoToItemScreen();
            return;
        }

        // go to item screen
        if (itemsContent.gameObject.activeSelf)
        {
            // for (int i = 0; i < itemsContent.childCount; i++)
            // Destroy(itemsContent.GetChild(i).gameObject);

            GoToCollectionScreen();
            return;
        }
    }

    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        // hard reset pra nao perder tempo

        //TODO:
        // for (int i = 0; i < collectionsContent.childCount; i++)
        //     Destroy(collectionsContent.GetChild(i).gameObject);

        // for (int i = 0; i < itemsContent.childCount; i++)
        //     Destroy(itemsContent.GetChild(i).gameObject);

        // itemSize.width.text = "";
        // itemSize.depth.text = "";

        // currentCollection = null;
        // currentItem = null;
        // itemSizeWidth = 0;
        // itemSizeDepth = 0;


        // GoToCollectionScreen();
    }

    // Update is called once per frame
    // void Update()
    // {
    // 
    // }
}