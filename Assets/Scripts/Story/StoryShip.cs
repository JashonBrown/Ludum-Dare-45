using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryShip : MonoBehaviour
{
    public float forceIncrement;
    public float maxForce;
    public float currentForce = 0;
    public Material backgroundMaterial;
    public Material waterMaterial;
    public float backgroundSpeed;
    public float baseWaterSpeed;

    private Rigidbody2D _rb;
    private Animator _animator;

    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _Move();
        _animator.SetFloat("Speed", currentForce/maxForce);

        var increment = new Vector2((currentForce / maxForce) * backgroundSpeed, 0);

        backgroundMaterial.mainTextureOffset += increment;

        Vector2 waterIncrement;

        if ((currentForce / maxForce) * backgroundSpeed < baseWaterSpeed) {
            waterIncrement = new Vector2(baseWaterSpeed, 0);
        }
        else {
            waterIncrement = increment;
        }

        waterMaterial.mainTextureOffset += waterIncrement;
    }

    private void _Move()
    {
        if (!ObjectReferencer.Instance.CanMove) return;

        if (Input.GetKey(KeyCode.D)) currentForce += forceIncrement;
        else currentForce -= forceIncrement;


        if (currentForce > maxForce) currentForce = maxForce;
        if (currentForce < 0) currentForce = 0;

        _rb.AddForce(new Vector3(1,0,0) * currentForce);
    }
}
