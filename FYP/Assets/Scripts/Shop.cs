using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Shop : MonoBehaviour
{

    [SerializeField] GameObject ShopUI;
    [SerializeField] TextMeshProUGUI soldierCost;
    [SerializeField] TextMeshProUGUI kingCost;
    [SerializeField] TextMeshProUGUI dmgUpCost;
    [SerializeField] TextMeshProUGUI hpUpCost;
    [SerializeField] TextMeshProUGUI receipt;
    // Start is called before the first frame update
    private void Start()
    {
        ShopUI.SetActive(false);
        
    }
   
    private void OnTriggerEnter2D(Collider2D other)
    {
        ShopUI.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ShopUI.SetActive(false);
    }

    public int GetSoldierCost()
    {
        return int.Parse(soldierCost.text);
    }
    public int GetKingCost()
    {
        return int.Parse(kingCost.text);
    }
    public int GetDmgUpCost()
    {
        return int.Parse(dmgUpCost.text);
    }
    public int GetHpUpCost()
    {
        return int.Parse(hpUpCost.text);
    }

    public void SetReceipt(int shortBy)
    {
        receipt.text = "Need " + shortBy +  " more diamonds";
        StartCoroutine(ReceiptOutcome());
    }

    IEnumerator ReceiptOutcome()
    {
        receipt.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        receipt.gameObject.SetActive(false);
    }
}
