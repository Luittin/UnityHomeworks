using UnityEngine;
using UnityEngine.UI;

public class CreateMenuItem
{
    public static GameObject CreateSelectItem(GameObject prefabElementMenu, RectTransform parentElement)
    {
        GameObject selectLevel = MonoBehaviour.Instantiate(prefabElementMenu, parentElement);

        return selectLevel;
    }

    public static void SetBackgroundSelectItem(Texture texture, Image menuItem)
    {
        if (texture != null)
        {
            menuItem.sprite = Sprite.Create((Texture2D) texture, new Rect(0, 0, texture.width, texture.height),
                Vector2.zero);
        }
    }

    public static void SetOptionToSelect(Button selectButton)
    {
        ColorBlock colorBlock = selectButton.colors;
        colorBlock.normalColor = new Color(0.0f, 0.0f, 0.0f);
        selectButton.colors = colorBlock;
        selectButton.interactable = false;
    }

    public static void SetStarsSelectItem(SelectedMenuItem selectLevel, int countStars)
    {
        selectLevel.SetStars(countStars);
    }
}
