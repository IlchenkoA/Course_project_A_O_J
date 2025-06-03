using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerSaveData : MonoBehaviour
{
    public float TotalPrice;

    [SerializeField] private GameObject prefab;
    public bool isCreated = false;

    private static PlayerSaveData instance;
    public static PlayerSaveData Instance => instance;

    [SerializeField] private List<CollectibleState> GetObject;

    [SerializeField] private AudioClip paymentSound;
    [SerializeField] private AudioClip removeSound;
    private AudioSource audioSource;

 
    private string pendingDeleteProductName = null;
    private GameObject pendingDeleteObject = null;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            audioSource = gameObject.AddComponent<AudioSource>();
        }

    }

    void LateUpdate()
    {
        if (SceneManager.GetSceneByBuildIndex(11) == SceneManager.GetActiveScene())
        {
            createPrefabs();
        }
    }

    private void createPrefabs()
    {
        if (!isCreated)
        {
            foreach (var obj in GetObject)
            {
                var obj2 = Instantiate(prefab);
                obj2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = obj.name;
                obj2.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = obj.count.ToString();
                obj2.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = obj.price.ToString();
                TotalPrice += obj.price;
            }
            isCreated = true;
            UpdateTotalPrice();
        }
    }

    public void AddProductToCart(ProductAd product)
    {
        if (product == null)
        {
            Debug.LogError("❌ Product is null!");
            return;
        }

        isCreated = false;
        bool productExists = false;

        PlayerPrefs.SetString("LastVisitedFlowerScene", SceneManager.GetActiveScene().name);

        foreach (var item in GetObject)
        {
            if (item.name == product.nameProduct)
            {
                var updatedItem = item;
                updatedItem.count += 1;
                updatedItem.price = updatedItem.priceOne * updatedItem.count;
                int index = GetObject.IndexOf(item);
                GetObject[index] = updatedItem;
                productExists = true;
                break;
            }
        }

        if (!productExists)
        {
            GetObject.Add(new CollectibleState
            {
                name = product.nameProduct,
                count = product.countProduct,
                price = product.priceProduct,
                priceOne = product.priceOneProduct
            });
        }

        UpdateTotalPrice();

        if (paymentSound != null)
        {
            audioSource.PlayOneShot(paymentSound);
        }
    }
    public static void RemoveProductFromCart(string productName)
    {
        CollectibleState productToRemove = Instance.GetObject.FirstOrDefault(item => item.name == productName);

        if (productToRemove.name != null)
        {
            Instance.GetObject.Remove(productToRemove);
            Instance.UpdateTotalPrice();

            // Використовуємо Instance для доступу до audioSource
            if (Instance.removeSound != null)
            {
                Instance.audioSource.PlayOneShot(Instance.removeSound);
            }
            Debug.Log("Product " + productName + " removed from cart.");
        }
        else
        {
            Debug.Log("Product " + productName + " not found in cart.");
        }
    }
    private void UpdateTotalPrice()
    {
        TotalPrice = GetObject.Sum(item => item.price);
        UpdateTotalPriceUI();
    }

    private void UpdateTotalPriceUI()
    {
        var total = GameObject.Find("Total")?.GetComponent<TextMeshProUGUI>();
        if (total != null)
        {
            total.text = TotalPrice.ToString("F2");
        }
    }

    public List<CollectibleState> GetCartItems()
    {
        return GetObject;
    }
    public void ClearCart()
    {
        GetObject.Clear();
        UpdateTotalPrice(); // також скине TotalPrice до 0 і оновить UI
    }

}

[System.Serializable]
public struct CollectibleState
{
    public string name;
    public int count;
    public float price;
    public float priceOne;
}

