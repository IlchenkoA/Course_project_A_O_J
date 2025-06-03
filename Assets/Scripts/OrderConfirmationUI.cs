using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OrderConfirmationUI : MonoBehaviour
{
    [SerializeField] private GameObject confirmationPanel;
    [SerializeField] private TextMeshProUGUI confirmationText;
    [SerializeField] private TextMeshProUGUI totalPriceText;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button okButton;
    [SerializeField] private Transform cartContainer; // Контейнер для всіх товарів

    // Окремі змінні для текстів
    [SerializeField] private string emptyOrderMessage = "Ви ще нічого не замовили.";
    [SerializeField] private string orderConfirmationMessageTemplate = "Ваше замовлення на суму {0:F2} грн готове.\nДякуємо за замовлення! З вами зв’яжуться найближчим часом.";

    private void Start()
    {
        if (confirmationPanel != null)
            confirmationPanel.SetActive(false);

        if (confirmButton != null)
            confirmButton.onClick.AddListener(HandleOrderConfirmation);

        if (okButton != null)
            okButton.onClick.AddListener(HideConfirmation);
    }

    public void HandleOrderConfirmation()
    {
        float total = 0f;

        if (totalPriceText != null && float.TryParse(totalPriceText.text, out float parsedTotal))
        {
            total = parsedTotal;
        }

        if (total == 0f)
        {
            if (confirmationPanel != null && confirmationText != null)
            {
                confirmationText.text = emptyOrderMessage;
                confirmationPanel.SetActive(true);
            }
        }
        else
        {
            if (confirmationPanel != null && confirmationText != null)
            {
                confirmationText.text = string.Format(orderConfirmationMessageTemplate, total);
                confirmationPanel.SetActive(true);
            }
        }
    }

    public void HideConfirmation()
    {
        if (confirmationPanel != null)
            confirmationPanel.SetActive(false);

        // Очищення кошика на сцені
        if (cartContainer != null)
        {
            foreach (Transform child in cartContainer)
            {
                Destroy(child.gameObject);
            }
        }

        // Скидання тексту загальної суми
        if (totalPriceText != null)
        {
            totalPriceText.text = "0.00";
        }

        //  Очищення локального сховища (GetObject)
        if (PlayerSaveData.Instance != null)
        {
            PlayerSaveData.Instance.ClearCart();
        }
    }

}
