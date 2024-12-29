using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private GameObject ShopImage;
    private RectTransform ShopImageRT;
    private Player player;

    [SerializeField]
    private GameObject jumpLevelUpValueText;
    [SerializeField]
    private GameObject maxHPUpValueText;

    private int jumpLevelUpValue=2;

    private int maxHPUpValue=5;

    private HitPoint HP;
    void Start()
    {
        HP=GameObject.Find("HP").GetComponent<HitPoint>();
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
            Text text=GameObject.Find("PlayerLevel").GetComponent<Text>();
            text.text="Lv. "+player.jumpLevel;
            //増え方は後から変更
            jumpLevelUpValue++;
            jumpLevelUpValueText.GetComponent<Text>().text=""+jumpLevelUpValue;
        } 
    }

    public void MaxHpUp()
    {
        if(Coin.CoinCount>=maxHPUpValue && GameManager.MaxHP<8)
        {
            Coin.CoinCount-=maxHPUpValue;
            Coin.CoinSet();
            HP.images[GameManager.MaxHP].color=new Color(0,0,0,255);
            GameManager.MaxHP++;
            maxHPUpValue+=5;
            maxHPUpValueText.GetComponent<Text>().text=""+maxHPUpValue;
            
        }
    }

    public void ShopExit()
    {
        ShopImageRT.localScale=new Vector3(0.1f,0.1f,0.1f);
        ShopImage.SetActive(false);
        GameManager.isActive=true;
    }
}
