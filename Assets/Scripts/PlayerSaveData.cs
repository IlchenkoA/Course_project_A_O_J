using SaveLoadsSystem; 
using System.Collections;
using System.Collections.Generic; 
using System.ComponentModel.Design; 
using UnityEngine; 
using TMPro; 
using UnityEngine.InputSystem; 
using UnityEngine.SceneManagement; 
using static PlayerSaveData; // Підключення статичних членів класу PlayerSaveData
using System.Linq; 
using System; 

public class PlayerSaveData : MonoBehaviour
{
    public float TotalPrice; 

    [SerializeField] private GameObject prefab; // Префаб, який буде використовуватись для створення об'єктів
    public bool isCreated = false; // Прапорець, який вказує, чи були створені префаби

    
    private static PlayerSaveData instance;
    public static PlayerSaveData Instance { get { return instance; } }

    [SerializeField] private List<CollectibleState> GetObject; // Список об'єктів у кошику

    private void Awake()
    {
        // Перевіряємо, чи екземпляр класу вже існує
        if (instance != null && instance != this)
        {
            // Якщо так, то знищуємо даний екземпляр
            Destroy(this.gameObject);
        }
        else
        {
            // Встановлюємо поточний екземпляр
            instance = this;
            // Переконуємося, що екземпляр не буде знищений при переході між сценами
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
    }

    void LateUpdate()
    {
        // Якщо поточна сцена є сценою з індексом 11
        if (SceneManager.GetSceneByBuildIndex(11) == SceneManager.GetActiveScene())
        {
            // Створюємо префаби
            createPrefabs();
        }
    }

    private void createPrefabs()
    {
        // Перевіряємо, чи префаби вже створені
        if (!isCreated)
        {
            // Проходимося по кожному об'єкту у списку
            foreach (var obj in GetObject)
            {
                // Створюємо новий префаб
                var obj2 = Instantiate(prefab);
                // Встановлюємо текстові значення для дочірніх об'єктів префабу
                obj2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = obj.name;
                obj2.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = obj.count.ToString();
                obj2.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = obj.price.ToString();
                // Додаємо ціну до загальної суми
                TotalPrice += obj.price;
            }
            // Встановлюємо прапорець створення префабів
            isCreated = true;
            // Оновлюємо загальну суму ціни на інтерфейсі
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
        bool productExists = false; // Прапорець для перевірки наявності продукту в кошику

        // Перевіряємо, чи вже є об'єкт з такою ж назвою в списку
        foreach (var item in GetObject)
        {
            if (item.name == product.nameProduct)
            {
                // Створюємо нову змінну, яка представляє елемент списку
                var updatedItem = item;

                // Збільшуємо count на 1
                updatedItem.count += 1;
                updatedItem.price = updatedItem.priceOne * updatedItem.count;

                // Замінюємо оригінальний елемент оновленим елементом
                int index = GetObject.IndexOf(item);
                GetObject[index] = updatedItem;

                productExists = true;
                break;
            }
        }

        // Якщо об'єкт ще не існує, додаємо його в список
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
        // Оновлюємо загальну суму ціни
        UpdateTotalPrice();
        // Виводимо список об'єктів в кошику у консоль
        Debug.Log(GetObject);
    }

    public static void RemoveProductFromCart(string productName)
    {
        // Шукаємо елемент зі списку за назвою
        CollectibleState productToRemove = Instance.GetObject.FirstOrDefault(item => item.name == productName);

        // Якщо знайдено елемент, то видаляємо його зі списку
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
        // Оновлюємо загальну суму ціни всіх товарів у кошику
        TotalPrice = GetObject.Sum(item => item.price);
        // Оновлюємо інтерфейс
        UpdateTotalPriceUI();
    }

    private void UpdateTotalPriceUI()
    {
        // Знаходимо об'єкт "Total" і оновлюємо його текстове значення
        var total = GameObject.Find("Total").GetComponent<TextMeshProUGUI>();
        total.text = TotalPrice.ToString("F2"); // Форматуючи до 2 десяткових знаків
    }

    public List<CollectibleState> GetCartItems()
    {
        // Повертаємо список елементів кошика
        return GetObject;
    }
}

[System.Serializable] // Анотація для серіалізації структури
public struct CollectibleState
{
    public string name; // Назва товару
    public int count; // Кількість товару
    public float price; // Загальна ціна товару (кількість * ціна за одиницю)
    public float priceOne; // Ціна за одиницю товару
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



//        // Додаємо новий об'єкт до списку
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