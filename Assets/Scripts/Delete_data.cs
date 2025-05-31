//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;

//public class Delete_data : MonoBehaviour
//{
//    public GameObject name;

//    public GameObject conteiner;

//    private void Start()
//    {

//    }

//    public void dell()
//    {

//        PlayerSaveData.RemoveProductFromCart(name.GetComponent<TextMeshProUGUI>().text);

//        Destroy(this.gameObject);
//    }
//    private void LateUpdate()
//    {
//        conteiner = GameObject.Find("Container");

//        this.gameObject.transform.SetParent(conteiner.transform);
//    }
//}

using TMPro;
using UnityEngine;

public class Delete_data : MonoBehaviour
{
    public GameObject name;
    public GameObject conteiner;

    private void LateUpdate()
    {
        conteiner = GameObject.Find("Container");
        this.transform.SetParent(conteiner.transform);
    }

    public void OnDeleteButtonPressed()
    {
        string productName = name.GetComponent<TextMeshProUGUI>().text;
        PlayerSaveData.Instance.AskRemoveProduct(productName, this.gameObject);
    }
}
