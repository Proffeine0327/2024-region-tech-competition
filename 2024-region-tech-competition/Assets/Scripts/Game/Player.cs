using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Resources;

public class Player : BaseFlighter
{
    public static Player Instance { get; private set; }

    private DataManager dataManager => DataManager.Instance;
    private GameManager gameManager => GameManager.Instance;
    private ResourceContainer resourceContainer => ResourceContainer.Instance;

    public Transform orientation;
    public LayerMask modelAlignLayer;

    [NonSerialized] public int money;
    [NonSerialized] public float curMaxSpeed;
    [NonSerialized] public float curSteerSpeed;
    [NonSerialized] public new Rigidbody rigidbody;
    [NonSerialized] public AudioSource audioSource;
    [NonSerialized] public PlayerModel playerModel;

    public PlayerData PlayerData => dataManager.playerDatas[dataManager.playerSelect];
    public float Speed => rigidbody.velocity.magnitude;

    private void Awake()
    {
        Instance = this;
    }

    protected override void Start()
    {
        base.Start();
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        playerModel = Instantiate(resourceContainer.playerModels[dataManager.playerSelect], orientation);
        playerModel.GetComponent<MeshRenderer>().sharedMaterial = resourceContainer.playerColors[dataManager.playerColorSelect];
        playerModel.transform.localPosition = Vector3.zero;
        model = playerModel.transform;

        curMaxSpeed = PlayerData.maxSpeed;
    }

    private void Update()
    {
        Physics.Raycast(model.position, Vector3.down, out var hit, 10f, modelAlignLayer);
        model.rotation = Quaternion.Lerp(model.rotation, Quaternion.FromToRotation(Vector3.up, hit.normal) * orientation.rotation, Time.deltaTime * 3);

        if (!GameManager.Instance.isGameRunning) return;
        if (!canMove) return;

        playerModel.isDrift = Input.GetKey(KeyCode.LeftShift);
        curSteerSpeed = Mathf.Lerp(curSteerSpeed, Input.GetKey(KeyCode.LeftShift) ? PlayerData.driftSteerSpeed : PlayerData.steerSpeed, Time.deltaTime * 2f);

        orientation.Rotate(0, Input.GetAxis("Horizontal") * curSteerSpeed * Time.deltaTime, 0);

        var velY = rigidbody.velocity.y;
        float mul = dataManager.engine switch
        {
            EngineType.Engine6 => 1.1f,
            EngineType.Engine8 => 1.25f,
            _ => 1
        };
        rigidbody.velocity = Vector3.ClampMagnitude(new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z), curMaxSpeed * mul * slow);
        rigidbody.velocity = new Vector3(rigidbody.velocity.x, velY, rigidbody.velocity.z);
        audioSource.pitch = Mathf.LerpUnclamped(0.75f, 1.75f, Mathf.LerpUnclamped(0, 1, Speed / PlayerData.maxSpeed));
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.isGameRunning) return;

        var displayWheel = gameManager.stage switch
        {
            1 => dataManager.desertWing,
            2 => dataManager.mountainWing,
            3 => dataManager.cityWing,
            _ => false
        };
        rigidbody.AddForce(Input.GetAxisRaw("Vertical") * model.forward * PlayerData.acceleration * (displayWheel ? 1.25f : 1), ForceMode.Acceleration);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag("Item") && !ItemManager.Instance.IsPickingItem)
        {
            Destroy(other.gameObject);
            ItemManager.Instance.GetRandomItem(item =>
            {
                switch (item)
                {
                    case ItemType.Money10: money += 10; gameManager.gainMoney += 10; break;
                    case ItemType.Money50: money += 50; gameManager.gainMoney += 50; break;
                    case ItemType.Money100: money += 100; gameManager.gainMoney += 100; break;
                    case ItemType.SpeedFast: StartCoroutine(SpeedFastRoutine()); break;
                    case ItemType.SpeedSuperFast: StartCoroutine(SpeedSuperFastRoutine()); break;
                    //case ItemType.JoinShop: UpgradeManager.Instance.Display(); break;
                    case ItemType.EndEnum: break;
                }
            });
        }
    }

    private IEnumerator SpeedFastRoutine()
    {
        playerModel.isSpeed = true;
        TweenUtility.DOFloat(curMaxSpeed, PlayerData.maxSpeed * 1.5f, 0.5f, x => curMaxSpeed = x);
        yield return new WaitForSeconds(3f);
        TweenUtility.DOFloat(curMaxSpeed, PlayerData.maxSpeed, 0.5f, x => curMaxSpeed = x);
        playerModel.isSpeed = false;
    }

    private IEnumerator SpeedSuperFastRoutine()
    {
        playerModel.isSpeed = true;
        TweenUtility.DOFloat(curMaxSpeed, PlayerData.maxSpeed * 2, 0.5f, x => curMaxSpeed = x);
        yield return new WaitForSeconds(3f);
        TweenUtility.DOFloat(curMaxSpeed, PlayerData.maxSpeed, 0.5f, x => curMaxSpeed = x);
        playerModel.isSpeed = false;
    }
}