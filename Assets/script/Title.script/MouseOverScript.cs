using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class MouseOverScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image image { get { return GetComponent<Image>(); } }

    //マウスオーバー時の色変化

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(image.color == Color.white)
        image.color = new Color(0.8f, 0.8f, 1.0f, 0.9f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (image.color == new Color(0.8f, 0.8f, 1.0f, 0.9f))
            image.color = Color.white;
        
    }
}

