using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class InventoryItem
{
    public int ObjCount;
    public Text ObjCountText;
}


public class Inventory : MonoBehaviour
{
    public InventoryItem[] InInventory;

    // systeme de singletone 1 seul classe inventory dans le jeu, on peut y accéder depuis n'importe où
    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de Inventory dans la scène");
            return;
        }

        instance = this;
    }

    public void AddObject(int index)
    {
        InInventory[index].ObjCount++;
        InInventory[index].ObjCountText.text = InInventory[index].ObjCount.ToString();
    }
}
