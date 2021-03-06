using UnityEngine;
using TMPro;

public class UIPanels : SingletonClass<UIPanels> {
    [Header("The point on the screen each panel is docked")]
    [SerializeField]
    private Vector2 panelSharedAnchorPoint;

    public Transform screenSpaceCanvas;
    public GameObject textAlertPanel;
    public GameObject houseCompletionPanel;
 
    [SerializeField]
    public GameObject buildingPanel;

    //barracks info
    [HideInInspector]
    public TMP_InputField barracksInputField;
    [HideInInspector]
    public GameObject barracksPanel;
    [HideInInspector]
    public MonsterType barracksTertiaryFocusedMonster;
    [HideInInspector]
    public HUDMainBar hudMainBar;

    //armoury info
    [HideInInspector]
    public GameObject armouryPanel;

    //blacksmith info
    [HideInInspector]
    public GameObject blacksmithPanel;
    [HideInInspector]
    public TMP_InputField blacksmithInputField;
    [HideInInspector]
    public GameObject blacksmithFocusedJob;
    public Craftable[] blacksmithCraftingRecipes;

    //tooltip
    [HideInInspector]
    public GameObject blacksmithTooltipPanel;
    [HideInInspector]
    public GameObject buildingTooltipPanel;

    [HideInInspector]
    public GameObject focusedPanel;


    [SerializeField]
    private Sprite[] resourceSprites;

    [HideInInspector]
    public GameObject huntSelectedButton;

    [HideInInspector]
    public GameObject clearHuntButton;

	public override void Awake ()
    {
        base.Awake();


        barracksPanel = GameObject.Find("Barracks panel");
        barracksInputField = barracksPanel.transform.GetChild(1).GetChild(1).GetChild(2).GetComponent<TMP_InputField>();
        ResetPanel(barracksPanel.transform);
        barracksPanel.SetActive(false);

        armouryPanel = GameObject.Find("Armoury panel");
        ResetPanel(armouryPanel.transform);
        armouryPanel.SetActive(false);

        blacksmithPanel = GameObject.Find("Blacksmith panel");
        blacksmithInputField = blacksmithPanel.transform.GetChild(1).GetChild(2).GetChild(1).GetComponent<TMP_InputField>();
        ResetPanel(blacksmithPanel.transform);
        blacksmithPanel.SetActive(false);

        huntSelectedButton = GameObject.Find("Hunt Selected");
        clearHuntButton = GameObject.Find("ReturnFromHunt");
        huntSelectedButton.SetActive(false);
        clearHuntButton.SetActive(false);

        buildingPanel = GameObject.Find("BuildingCreationPanelBG");
        buildingPanel.SetActive(false);

        blacksmithTooltipPanel = blacksmithPanel.transform.Find("Item Tooltip").gameObject;
        blacksmithTooltipPanel.SetActive(false);

        buildingTooltipPanel = buildingPanel.transform.Find("BuildingTooltip").gameObject;
        buildingTooltipPanel.SetActive(false);

        hudMainBar = FindObjectOfType<HUDMainBar>();
    }


    public void ResetPanel(Transform panel)
    {
        //set the tertiary panel to false
        panel.GetChild(1).gameObject.SetActive(false);
        //set the close tertiary button to false
        if(panel.childCount > 2)
        {
            panel.GetChild(2).gameObject.SetActive(false);
        }
        panel.GetComponent<RectTransform>().anchoredPosition = panelSharedAnchorPoint;
    }

    public Sprite GetResourceSprite(ItemType resourceType)
    {
        switch (resourceType)
        {
            case ItemType.Wood:
                return resourceSprites[0];
            case ItemType.Stone:
                return resourceSprites[1];
            case ItemType.Iron:
                return resourceSprites[2];
            case ItemType.Bone:
                return resourceSprites[3];
            case ItemType.Crystal:
                return resourceSprites[4];
            case ItemType.Nutrition:
                return resourceSprites[5];
            default:
                return null;
        }
    }
}
