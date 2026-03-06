using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private Fairy[] Fairies;
    [SerializeField] private Flower[] Flowers;
    [SerializeField] private Egg egg;
    [SerializeField] private GameObject infoOverlay;
    [SerializeField] private Vector3 spawnFairyPoint = new Vector3(0, 0, 0);
    [SerializeField] private float aboveScreen = 4.6f;
    [SerializeField] private float leftSpawnFlowerRange = -7f;
    [SerializeField] private float rightSpawnFlowerRange = 7f;
    private Dictionary<ItemType, int> inventory = new Dictionary<ItemType, int>();
    private SpriteRenderer eggSprite;
    private float flowerDelay = 2f;
    private float timePassedFlower = 0;
    Fairy fairy;
    private int eggCount = 0;
    //add: priceCalculator(eggCount), print price on Update (hover), timer, maybe upgrade nest
    public enum ItemType
    {
        XP, Fairy1, Fairy2, Fairy3, Flower1, Flower2, Flower3
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        foreach (ItemType item in System.Enum.GetValues(typeof(ItemType))) inventory[item] = 0;
    }
    private void Start()
    {
        eggSprite = egg.GetComponent<SpriteRenderer>();
        egg.gameObject.SetActive(false);
        timePassedFlower = 0;
        eggCount = 0;
    }

    private void Update()
    {
        SpawnFlowers();
    }

    public void InventoryLog()
    {
        Debug.Log("Fairy1=" + inventory[ItemType.Fairy1] + " Fairy2=" + inventory[ItemType.Fairy2] + " Fairy3=" + inventory[ItemType.Fairy3] + " Flower1=" + inventory[ItemType.Flower1] + " Flower2=" + inventory[ItemType.Flower2] + " Flower3=" + inventory[ItemType.Flower3] + " eggCount:" + eggCount);
    }

    public void AddItem(ItemType type, int amount)
    {
        inventory[type] += amount;
        if (inventory[type] < 0) inventory[type] = 0;
        InventoryLog();
    }
    public int GetItem(ItemType type)
    {
        return inventory[type];
    }

    public void InfoOverlay(bool state)
    {
        infoOverlay.SetActive(state);
    }

    public bool BuyEgg()
    {
        //check funds
        egg.Init();
        eggCount += 1;
        return true;
    }
    public void SpawnFairy() //add ratio
    {
        int fairyIndex = Random.Range(0, Fairies.Length);
        fairy = Instantiate(Fairies[fairyIndex], spawnFairyPoint, Quaternion.identity);
    }
    private void SpawnFlowers() //add ratio
    {
        if (timePassedFlower > flowerDelay)
        {
            timePassedFlower = 0;
            int flowerIndex = Random.Range(0, Flowers.Length);
            Vector3 flowerPoint = new Vector3(Random.Range(leftSpawnFlowerRange, rightSpawnFlowerRange), aboveScreen, 0);
            Instantiate(Flowers[flowerIndex], flowerPoint, Quaternion.identity);
        }
        timePassedFlower += Time.deltaTime;
    }
    public bool IsEggAvailable()
    {
        if (fairy) return (false);
        return (!egg.gameObject.activeSelf && !fairy);
    }
    public void CollectEgg()
    {
        egg.gameObject.SetActive(false);
    }
}