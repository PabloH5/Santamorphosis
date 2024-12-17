using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetamorphosisController : MonoBehaviour
{
    [SerializeField] private List<GameObject> transformItems;

    public bool isTransforming;

    private void Transformation(int itemId = 0)
    {
        TurnItems();
        switch (itemId)
        {
            case 0:
                transformItems[0].SetActive(true);
                isTransforming = false;
                break;

            case 1:
                transformItems[3].SetActive(true);
                isTransforming = true;
                break;

            case 2:
                transformItems[2].SetActive(true);
                isTransforming = true;
                break;

            case 3:
                transformItems[3].SetActive(true);
                isTransforming = true;
                break;

            default:
                transformItems[0].SetActive(true);
                isTransforming = false;
                break;
        }
    }

    private void TurnItems()
    {
        foreach (var item in transformItems)
        {
            item.SetActive(false);
        }
    }
}
