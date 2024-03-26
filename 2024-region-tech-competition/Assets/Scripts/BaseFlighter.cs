using System.Collections;
using UnityEngine;

public class BaseFlighter : MonoBehaviour
{
    [SerializeField] protected Transform model;

    protected float slow = 1;
    protected float defaultFriction;
    protected bool canMove = true;
    protected bool isSlowing;
    protected bool isSliping;

    protected new Collider collider;
    public bool IsSliping => isSliping;
    public bool IsSlowing => isSlowing;

    protected virtual void Start()
    {
        collider = GetComponent<Collider>();
        defaultFriction = collider.sharedMaterial.dynamicFriction;
    }

    protected void SetFriction(float friction)
    {
        collider.sharedMaterial.staticFriction = friction;
        collider.sharedMaterial.dynamicFriction = friction;
    }

    protected IEnumerator SlipRoutine()
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

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SlipArea") && !isSliping)
            StartCoroutine(SlipRoutine());
        if (other.CompareTag("SlowArea"))
        {
            slow = 0.7f;
            isSlowing = true;
        }
        if (other.CompareTag("End"))
            GameManager.Instance.GameEnd(this);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SlowArea"))
        {
            slow = 1;
            isSlowing = false;
        }
    }
}