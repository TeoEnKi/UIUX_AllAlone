using UnityEngine;
using UnityEngine.UI;

public class CraftSections : MonoBehaviour
{
    [SerializeField] Image[] RecipeImgPlaceholders;
    [SerializeField] CraftingRecipes[] craftingRecipes;
    [SerializeField] Image[] craftBtns;
    [SerializeField] Color disableCraftBtnColour;
    bool[] craftable = new bool[2];
    int page1_recipeId = 0;


    void OnEnable()
    {
        for (int i = 0; i < 2; i++)
        {
            RecipeImgPlaceholders[i].sprite = craftingRecipes[i + page1_recipeId].recipeImg.GetComponent<Image>().sprite;
        }
        CheckCraftabilty();

    }

    private void CheckCraftabilty()
    {
        for (int i = 0; i < 2; i++)
        {
            craftable[i] = true;
            foreach (NeededMaterial needMat in craftingRecipes[page1_recipeId + i].neededMaterials)
            {
                if ((needMat.collectable.quanity - needMat.neededQty) < 0)
                {
                    craftable[i] = false;
                    break;
                }
            }

            if (craftable[i] == true)
            {
                craftBtns[i].color = Color.white;
            }
            else
            {
                craftBtns[i].color = disableCraftBtnColour;
            }
        }
    }

    public void Craft(Button button)
    {
        int selectedPageId = 0;
        if (button.transform.parent.name.Contains("2"))
        {
            selectedPageId = 1;
        }

        if (craftable[selectedPageId] == false) return;


        foreach (NeededMaterial needMat in craftingRecipes[page1_recipeId + selectedPageId].neededMaterials)
        {
            needMat.collectable.quanity -= needMat.neededQty;
        }


        craftingRecipes[page1_recipeId + selectedPageId].result.quanity++;
        CheckCraftabilty();

    }
}