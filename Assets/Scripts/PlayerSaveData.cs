using SaveLoadsSystem; 
using System.Collections;
using System.Collections.Generic; 
using System.ComponentModel.Design; 
using UnityEngine; 
using TMPro; 
using UnityEngine.InputSystem; 
using UnityEngine.SceneManagement; 
using static PlayerSaveData; // ϳ��������� ��������� ����� ����� PlayerSaveData
using System.Linq; 
using System; 

public class PlayerSaveData : MonoBehaviour
{
    public float TotalPrice; 

    [SerializeField] private GameObject prefab; // ������, ���� ���� ����������������� ��� ��������� ��'����
    public bool isCreated = false; // ���������, ���� �����, �� ���� ������� �������

    
    private static PlayerSaveData instance;
    public static PlayerSaveData Instance { get { return instance; } }

    [SerializeField] private List<CollectibleState> GetObject; // ������ ��'���� � ������

    private void Awake()
    {
        // ����������, �� ��������� ����� ��� ����
        if (instance != null && instance != this)
        {
            // ���� ���, �� ������� ����� ���������
            Destroy(this.gameObject);
        }
        else
        {
            // ������������ �������� ���������
            instance = this;
            // ������������, �� ��������� �� ���� �������� ��� ������� �� �������
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
    }

    void LateUpdate()
    {
        // ���� ������� ����� � ������ � �������� 11
        if (SceneManager.GetSceneByBuildIndex(11) == SceneManager.GetActiveScene())
        {
            // ��������� �������
            createPrefabs();
        }
    }

    private void createPrefabs()
    {
        // ����������, �� ������� ��� �������
        if (!isCreated)
        {
            // ����������� �� ������� ��'���� � ������
            foreach (var obj in GetObject)
            {
                // ��������� ����� ������
                var obj2 = Instantiate(prefab);
                // ������������ ������� �������� ��� ������� ��'���� �������
                obj2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = obj.name;
                obj2.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = obj.count.ToString();
                obj2.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = obj.price.ToString();
                // ������ ���� �� �������� ����
                TotalPrice += obj.price;
            }
            // ������������ ��������� ��������� �������
            isCreated = true;
            // ��������� �������� ���� ���� �� ���������
            UpdateTotalPrice();
        }
    }

    private void loaded(GameObject obj2, GameObject obj)
    {
       
        //StartCoroutine(CreateObjectWithDelay(obj2, obj));
    }

    public void AddProductToCart(ProductAd product)
    {
        isCreated = false;
        bool productExists = false; // ��������� ��� �������� �������� �������� � ������

        // ����������, �� ��� � ��'��� � ����� � ������ � ������
        foreach (var item in GetObject)
        {
            if (item.name == product.nameProduct)
            {
                // ��������� ���� �����, ��� ����������� ������� ������
                var updatedItem = item;

                // �������� count �� 1
                updatedItem.count += 1;
                updatedItem.price = updatedItem.priceOne * updatedItem.count;

                // �������� ����������� ������� ��������� ���������
                int index = GetObject.IndexOf(item);
                GetObject[index] = updatedItem;

                productExists = true;
                break;
            }
        }

        // ���� ��'��� �� �� ����, ������ ���� � ������
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
        // ��������� �������� ���� ����
        UpdateTotalPrice();
        // �������� ������ ��'���� � ������ � �������
        Debug.Log(GetObject);
    }

    public static void RemoveProductFromCart(string productName)
    {
        // ������ ������� � ������ �� ������
        CollectibleState productToRemove = Instance.GetObject.FirstOrDefault(item => item.name == productName);

        // ���� �������� �������, �� ��������� ���� � ������
        if (productToRemove.name != null)
        {
            Instance.GetObject.Remove(productToRemove);
            Instance.UpdateTotalPrice();
            Debug.Log("Product " + productName + " removed from cart.");
        }
        else
        {
            Debug.Log("Product " + productName + " not found in cart.");
        }
    }

    private void UpdateTotalPrice()
    {
        // ��������� �������� ���� ���� ��� ������ � ������
        TotalPrice = GetObject.Sum(item => item.price);
        // ��������� ���������
        UpdateTotalPriceUI();
    }

    private void UpdateTotalPriceUI()
    {
        // ��������� ��'��� "Total" � ��������� ���� �������� ��������
        var total = GameObject.Find("Total").GetComponent<TextMeshProUGUI>();
        total.text = TotalPrice.ToString("F2"); // ���������� �� 2 ���������� �����
    }

    public List<CollectibleState> GetCartItems()
    {
        // ��������� ������ �������� ������
        return GetObject;
    }
}

[System.Serializable] // �������� ��� ���������� ���������
public struct CollectibleState
{
    public string name; // ����� ������
    public int count; // ʳ������ ������
    public float price; // �������� ���� ������ (������� * ���� �� �������)
    public float priceOne; // ֳ�� �� ������� ������
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



//        // ������ ����� ��'��� �� ������
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