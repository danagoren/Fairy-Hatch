using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private Fairy[] Fairies;
    [SerializeField] private Flower[] Flowers;
    [SerializeField] private Egg egg;
    [SerializeField] private GameObject infoOverlay;
    [SerializeField] private GameObject timerObject;
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
    [SerializeField] private TextMeshProUGUI TimerNum;
    public float timer;
    private int timerMinutes;
    private int timerSeconds;
    //add: priceCalculator(eggCount), print price on Update (hover), timer, maybe upgrade nest, call GetItem()
    public enum ItemType
    {
        XP, Fairy1, Fairy2, Fairy3, Flower1, Flower2, Flower3, Leaf, Branch
    }

    private void Awake()
    {
        timerObject.SetActive(false);
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
        timerObject.SetActive(false);
        timer = 0.01f;
        timerMinutes = 0;
        timerSeconds = 0;
    }

    private void Update()
    {
        SpawnFlowers();
        UpdateTimer();
        if (timer <= 1) timerObject.SetActive(false); //+ egg clickable
    }

    public void InventoryLog() //for testing
    {
        Debug.Log("Fairy1=" + inventory[ItemType.Fairy1] + " Fairy2=" + inventory[ItemType.Fairy2] + " Fairy3=" + inventory[ItemType.Fairy3] + " Flower1=" + inventory[ItemType.Flower1] + " Flower2=" + inventory[ItemType.Flower2] + " Flower:" + inventory[ItemType.Flower3] + " eggCount:" + eggCount + " Leaf:" + inventory[ItemType.Leaf] + " Branch:" + inventory[ItemType.Branch]);
    }

    public void AddItem(ItemType type, int amount)
    {
        if (type == ItemType.Fairy1 || type == ItemType.Fairy2 || type == ItemType.Fairy3) timerObject.SetActive(false);
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
        timerObject.SetActive(true);
        timer = (eggCount * 5) + 1;
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
            Vector3 flowerPosition = new Vector3(Random.Range(leftSpawnFlowerRange, rightSpawnFlowerRange), aboveScreen, 0);
            Instantiate(Flowers[flowerIndex], flowerPosition, Quaternion.identity);
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
        private void UpdateTimer()
    {
        timer -= Time.deltaTime;
        timerMinutes = (int)(timer / 60f);
        timerSeconds = (int)(timer % 60f);
        TimerNum.text = $"{timerMinutes}:{timerSeconds:00}";
    }
}