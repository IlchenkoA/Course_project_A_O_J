using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.InputSystem;
using TMPro;

public class PlantPriceController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject priceLabel;
    [SerializeField] private string priceText = "Ціна: 100 грн";

    private bool _isPriceVisible = false;

    void Start()
    {
        if (priceLabel != null)
            priceLabel.SetActive(false);
    }

    void Update()
    {
        if (Touchscreen.current == null || Touchscreen.current.primaryTouch.press.wasPressedThisFrame == false)
            return;

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == transform)
            {
                TogglePrice();
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        TogglePrice();
    }



public void TogglePrice()
{
    _isPriceVisible = !_isPriceVisible;

    if (priceLabel != null)
    {
        priceLabel.SetActive(_isPriceVisible);

        if (_isPriceVisible)
        {
            var labelText = priceLabel.GetComponent<TextMeshProUGUI>();
            if (labelText != null)
            {
                labelText.text = priceText;
            }
            else
            {
                Debug.LogError("TextMeshProUGUI компонент не знайдений на priceLabel.");
            }
        }
    }
    else
    {
        Debug.LogError("priceLabel не призначений у інспекторі.");
    }
}

}
