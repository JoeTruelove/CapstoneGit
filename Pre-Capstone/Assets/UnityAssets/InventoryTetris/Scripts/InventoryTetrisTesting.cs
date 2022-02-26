using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTetrisTesting : MonoBehaviour {

    [SerializeField] private Transform outerInventoryTetrisBackground;
    [SerializeField] private InventoryTetris inventoryTetris;
    [SerializeField] private InventoryTetris outerInventoryTetris;
    [SerializeField] private InventoryTetris outerInventoryTetris2;
    [SerializeField] private List<string> addItemTetrisSaveList;
    [SerializeField] private List<string> addItemTetrisSaveList2;
    [SerializeField] private List<List<string>> listsoflist;
    [SerializeField] private List<Chest> chests;
    private int chestInt = 0;

    private int addItemTetrisSaveListIndex;

    private void Start() {
        outerInventoryTetrisBackground.gameObject.SetActive(false);
        
    }

    public void ChestFound(int current)
    {
        chestInt = current;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            outerInventoryTetrisBackground.gameObject.SetActive(true);
            outerInventoryTetris.Load(addItemTetrisSaveList[addItemTetrisSaveListIndex]);
            outerInventoryTetris.Save();

            addItemTetrisSaveListIndex = (addItemTetrisSaveListIndex + 1) % addItemTetrisSaveList.Count;
        }
        /*if (Input.GetKeyDown(KeyCode.K))
        {
            outerInventoryTetrisBackground.gameObject.SetActive(true);
            outerInventoryTetris2.Load(addItemTetrisSaveList2[addItemTetrisSaveListIndex]);
            outerInventoryTetris2.Save();

            addItemTetrisSaveListIndex = (addItemTetrisSaveListIndex + 1) % addItemTetrisSaveList2.Count;
        }*/

        if(Input.GetKeyDown(KeyCode.K))
        {
            outerInventoryTetrisBackground.gameObject.SetActive(true);
        }

        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            outerInventoryTetrisBackground.gameObject.SetActive(true);
            //outerInventoryTetris.Load(listsoflist.(chests[addItemTetrisSaveListIndex]));

            addItemTetrisSaveListIndex = (addItemTetrisSaveListIndex + 1) % addItemTetrisSaveList.Count;
        }*/
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            outerInventoryTetrisBackground.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.P)) {
            Debug.Log(inventoryTetris.Save());
        }
    }

    public void ChestFound(Chest chest)
    {
        //outerInventoryTetris.Load(listsoflist[chests[chest]));
    }

}
