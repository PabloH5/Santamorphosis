using System.Collections.Generic;
using UnityEngine;

public class MetamorphosisController : MonoBehaviour
{
    [SerializeField] private List<GameObject> transformItems;

    // Indicates if currently transforming
    public bool isTransforming = false;
    // Indicates if can transform into states 1, 2 or 3
    public bool canTransform = true;

    [SerializeField] private float transformCoolDown = 2f; // Example value
    private float transformTimer = 0f;

    void Update()
    {
        // Decrease the transformTimer if it's above zero
        if (!canTransform && transformTimer > 0f)
        {
            transformTimer -= Time.deltaTime;
            if (transformTimer <= 0f)
            {
                // Once timer reaches zero, reset canTransform
                canTransform = true;
            }
        }
    }

    public void TransformController(int itemId = 0)
    {
        // If the requested transformation is 1,2 or 3, check if can transform
        // If it's 0, we allow it at any time.
        if (itemId == 0 || (itemId > 0 && canTransform))
        {
            Transformation(itemId);
        }
    }

    public void Transformation(int itemId = 0)
    {
        TurnItems();
        

        switch (itemId)
        {
            case 0:
                // Activate the first item and stop transformation mode
                transformItems[0].SetActive(true);
                SetTag(0);
                isTransforming = false;
                // No cooldown triggered here
                break;

            case 1:
                // Activate the corresponding item for state 1
                transformItems[1].SetActive(true);
                SetTag(1);
                isTransforming = true;
                StartCooldown();
                break;

            case 2:
                // Activate the corresponding item for state 2
                transformItems[2].SetActive(true);
                SetTag(2);
                isTransforming = true;
                StartCooldown();
                break;

            case 3:
                // Activate the corresponding item for state 3
                transformItems[3].SetActive(true);
                SetTag(3);
                isTransforming = true;
                StartCooldown();
                break;

            default:
                // Default case: Activate the first item
                transformItems[0].SetActive(true);
                SetTag(0);
                isTransforming = false;
                // No cooldown triggered here
                break;
        }
    }

    private void TurnItems()
    {
        // Disable all items before enabling the requested one
        foreach (var item in transformItems)
        {
            item.SetActive(false);
        }
    }

    private void SetTag(int tagItem)
    {
        gameObject.tag = transformItems[tagItem].tag;
    }

    private void StartCooldown()
    {
        // Set the timer for cooldown and prevent further transformations to 1,2,3
        canTransform = false;
        transformTimer = transformCoolDown;
    }
}
