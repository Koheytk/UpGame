using UnityEngine;
using System.Collections;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private GameObject ShopImage;
    private RectTransform ShopImageRT;
    private Player player;

    private int jumpLevelUpValue=1;
    void Start()
    {
        player=GameObject.Find("Player").GetComponent<Player>();
        if (ShopImage != null)
        {
            ShopImageRT = ShopImage.GetComponent<RectTransform>();
            ShopImage.SetActive(false); // 初期状態で非表示
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShopOpen()
    {   
        ShopImage.SetActive(true);
        StartCoroutine(SizeUp());
        GameManager.isActive=false;
    }

    IEnumerator SizeUp()
    {
        for(int size=1;size<150;size++)
        {
            ShopImageRT.localScale+=new Vector3(0.1f,0.1f,0.1f);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        
    }

    public void JumpLevelUp()
    {
        if(Coin.CoinCount>=jumpLevelUpValue)
        {
            Coin.CoinCount-=jumpLevelUpValue;
            Coin.CoinSet();
            player.jumpLevel++;
        } 
    }

    public void ShopExit()
    {
        ShopImageRT.localScale=new Vector3(0.1f,0.1f,0.1f);
        ShopImage.SetActive(false);
        GameManager.isActive=true;
    }
}
