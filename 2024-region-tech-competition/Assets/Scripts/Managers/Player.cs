using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : Singleton<Player>
{
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform model;
    [SerializeField] private LayerMask modelAlignLayer;
    [SerializeField] private float acc;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float steerSpeed;
    [SerializeField] private float driftSteerSpeed;

    private bool canMove = true;
    private bool isSliping;
    private float defaultFriction;
    private float curMaxSpeed;
    private float curSteerSpeed;
    private new Rigidbody rigidbody;
    private new Collider collider;
    private List<TrailRenderer> driftTrails;
    private GameObject speedParticle;
    private List<MeshRenderer> improveWheelMeshs;
    private List<MeshRenderer> engineUpgradeMeshs;

    public Transform Orientation => orientation;
    public float Speed => rigidbody?.velocity.magnitude ?? 0;
    public bool IsSliping => isSliping;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        defaultFriction = collider.sharedMaterial.dynamicFriction;
        driftTrails = this.GetComponentsInChildren<TrailRenderer>("drift-trail");
        speedParticle = GetComponentsInChildren<Transform>().Where(x => x.name == "speed-particle").First().gameObject;
        improveWheelMeshs = this.GetComponentsInChildren<MeshRenderer>("improve-wheel");
        engineUpgradeMeshs = this.GetComponentsInChildren<MeshRenderer>("engine-upgrade");
        speedParticle.SetActive(false);
        curMaxSpeed = maxSpeed;
    }

    private void Update()
    {
        Physics.Raycast(model.position, Vector3.down, out var hit, 10f, modelAlignLayer);
        model.rotation = Quaternion.Lerp(model.rotation, Quaternion.FromToRotation(Vector3.up, hit.normal) * orientation.rotation, Time.deltaTime * 3);

        if (!GameManager.Instance.IsGameRunning) return;
        if (!canMove) return;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            driftTrails.ForEach(t => t.gameObject.SetActive(true));
            curSteerSpeed = Mathf.Lerp(curSteerSpeed, driftSteerSpeed, Time.deltaTime * 2f);
        }
        else
        {
            driftTrails.ForEach(t => t.gameObject.SetActive(false));
            curSteerSpeed = Mathf.Lerp(curSteerSpeed, steerSpeed, Time.deltaTime * 2f);
        }
        orientation.Rotate(0, Input.GetAxis("Horizontal") * curSteerSpeed * Time.deltaTime, 0);

        var velY = rigidbody.velocity.y;
        float mul = 1;
        if (UpgradeManager.Instance.EngineStep == 1) mul = 1.1f;
        if (UpgradeManager.Instance.EngineStep == 2) mul = 1.25f;
        rigidbody.velocity = Vector3.ClampMagnitude(new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z), curMaxSpeed * mul);
        rigidbody.velocity = new Vector3(rigidbody.velocity.x, velY, rigidbody.velocity.z);

        improveWheelMeshs.ForEach(x => x.gameObject.SetActive(UpgradeManager.Instance.ImprovedWheel));
        engineUpgradeMeshs.For((x, index) => x.gameObject.SetActive(UpgradeManager.Instance.EngineStep == index + 1));
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.IsGameRunning) return;
        rigidbody.AddForce(Input.GetAxisRaw("Vertical") * model.forward * acc * (UpgradeManager.Instance.ImprovedWheel ? 1.25f : 1), ForceMode.Acceleration);
    }

    private void SetFriction(float friction)
    {
        collider.sharedMaterial.staticFriction = friction;
        collider.sharedMaterial.dynamicFriction = friction;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SlipArea") && !isSliping)
            StartCoroutine(SlipRoutine());
        if (other.CompareTag("Item") && !ItemManager.Instance.IsPickingItem)
        {
            Destroy(other.gameObject);
            ItemManager.Instance.GetRandomItem(item =>
            {
                switch (item)
                {
                    case ItemType.Money10: UpgradeManager.Instance.Money += 10; break;
                    case ItemType.Money50: UpgradeManager.Instance.Money += 50;  break;
                    case ItemType.Money100: UpgradeManager.Instance.Money += 100;  break;
                    case ItemType.SpeedFast: StartCoroutine(SpeedFastRoutine()); break;
                    case ItemType.SpeedSuperFast: StartCoroutine(SpeedSuperFastRoutine()); break;
                    case ItemType.JoinShop: UpgradeManager.Instance.Display(); break;
                    case ItemType.EndEnum:
                        break;
                }
            });
        }
    }

    private IEnumerator SpeedFastRoutine()
    {
        speedParticle.SetActive(true);
        TweenUtility.DOFloat(curMaxSpeed, maxSpeed * 1.5f, 0.5f, x => curMaxSpeed = x);
        yield return new WaitForSeconds(3f);
        TweenUtility.DOFloat(curMaxSpeed, maxSpeed, 0.5f, x => curMaxSpeed = x);
        speedParticle.SetActive(false);
    }

    private IEnumerator SpeedSuperFastRoutine()
    {
        speedParticle.SetActive(true);
        TweenUtility.DOFloat(curMaxSpeed, maxSpeed * 2, 0.5f, x => curMaxSpeed = x);
        yield return new WaitForSeconds(3f);
        TweenUtility.DOFloat(curMaxSpeed, maxSpeed, 0.5f, x => curMaxSpeed = x);
        speedParticle.SetActive(false);
    }

    private IEnumerator SlipRoutine()
    {
        canMove = false;
        isSliping = true;
        SetFriction(0.05f);
        
        var dir = Input.GetAxis("Horizontal") > 0 ? 1 : -1;
        model.rotation = Quaternion.Euler(0, 0, 0);
        model.DOLocalRotate(new Vector3(0, dir * 1080, 0), 2f, Ease.OutQuad);
        yield return new WaitForSeconds(2f);
        model.localRotation = Quaternion.Euler(Vector3.zero);
        SetFriction(defaultFriction);
        isSliping = false;
        canMove = true;
    }
}