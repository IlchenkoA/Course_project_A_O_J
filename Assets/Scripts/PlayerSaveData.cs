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
    public static PlayerSaveData Instance { get { return instance; } }

    [SerializeField] private List<CollectibleState> GetObject;

    [SerializeField] private AudioClip paymentSound;
    [SerializeField] private AudioClip removeSound;
    private AudioSource audioSource;


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

        if (!string.IsNullOrEmpty(productToRemove.name))
        {
            Instance.GetObject.Remove(productToRemove);
            Instance.UpdateTotalPrice();

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
}

[System.Serializable]
public struct CollectibleState
{
    public string name;
    public int count;
    public float price;
    public float priceOne;
}




































//public class PlayerSaveData : MonoBehaviour
//{
//    private CartData MyData = new CartData();

//    [SerializeField] private string Name;
//    [SerializeField] private int Count;
//    [SerializeField] private float Price;


//    [SerializeField] private List<CollectibleState> GetObject;
//    private void Start()
//    {
//        MyData.collectedObjects = new List<CollectibleState>();
//    }


//    void LateUpdate()
//    {


//        if (Input.GetButtonDown("O"))
//        {
//            SaveMyScene();
//        }
//        if (Input.GetButtonDown("L"))
//        {
//            LoadMyScene();

//        }
//    }
//    [System.Serializable]
//    public struct CollectibleState
//    {
//        //public bool isActive;
//        //public Vector3 position;
//        public string name;
//        public int count;
//        public float price;
//    }
//    public void SaveMyScene()
//    {
//        //MyData.name = Name;
//        //MyData.count = Count;
//        //MyData.price = Price;



//        // Äîäàºìî íîâèé îá'ºêò äî ñïèñêó
//        MyData.collectedObjects.Add(new CollectibleState
//        {
//            name = Name,
//            count = Count,
//            price = Price
//        });


//        SaveGameManager.CurrentSaveData.PlayerData = MyData;
//        Debug.Log(MyData.collectedObjects);
//        SaveGameManager.Save();
//    }
//    public void LoadMyScene()
//    {
//        SaveGameManager.Load();
//        MyData = SaveGameManager.CurrentSaveData.PlayerData;

//        GetObject = MyData.collectedObjects;
//    }
//}
//[System.Serializable]
//public struct CartData
//{
//    public List<CollectibleState> collectedObjects;
//}