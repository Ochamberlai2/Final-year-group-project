using UnityEngine.UI;

public class BlacksmithFunction : BuildingFunction {

    public bool beingWorked;

    public override void Function()
    {
        //if the focused panel isnt null
        if (UIPanels.Instance.focusedPanel != null && UIPanels.Instance.focusedPanel != UIPanels.Instance.blacksmithPanel)
        {
            //deactivate it, because this buildings panel will become the focused panel
            UIPanels.Instance.focusedPanel.SetActive(false);
        }
        //if the ui panel was inactive at the time of the function being called
        if (!UIPanels.Instance.blacksmithPanel.activeSelf)
        {
            //activate the panel
            UIPanels.Instance.blacksmithPanel.SetActive(true);
            //then reset its foldout and buttons
            UIPanels.Instance.ResetPanel(UIPanels.Instance.blacksmithPanel.transform);
            UIPanels.Instance.focusedPanel = UIPanels.Instance.blacksmithPanel;
        }
        //otherwise it was activated
        else
        {
            //and we want to close it
            UIPanels.Instance.blacksmithPanel.SetActive(false);
            //and remove it as the focused panel
            UIPanels.Instance.focusedPanel = null;
        }

    }
    public override void OnBuilt()
    {
        UIPanels.Instance.hudMainBar.transform.Find("BuildingButtons").Find("blacksmith").GetComponent<Button>().interactable = true;
    }
}
