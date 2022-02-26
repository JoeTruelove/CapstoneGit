using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public bool isIn = false;
    public int chestInt = 0;

    [SerializeField] private InventoryTetrisTesting iTT;

    [SerializeField] private Transform outerInventoryTetrisBackground;
    [SerializeField] private InventoryTetris inventoryTetris;
    [SerializeField] private InventoryTetris outerInventoryTetris;

    [SerializeField] private List<string> addItemTetrisSaveList;

    private int addItemTetrisSaveListIndex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space) && isIn)
        {
            outerInventoryTetrisBackground.gameObject.SetActive(true);
            outerInventoryTetris.Load(addItemTetrisSaveList[addItemTetrisSaveListIndex]);

            addItemTetrisSaveListIndex = (addItemTetrisSaveListIndex + 1) % addItemTetrisSaveList.Count;
        }*/
    }

    public bool GetIsIn()
    {
        return isIn;
    }

    public int GetChest()
    {
        return chestInt;
    }

    public void SetChest(int x)
    {
        chestInt = x;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isIn = true;
            iTT.ChestFound(chestInt);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
           isIn = false;
        }
       
    }

}
