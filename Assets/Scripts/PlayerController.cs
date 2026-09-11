
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float moveSpeed = 5.0f;
    float rotateSpeed = 30.0f;

    KeyCode forwardKey;
    KeyCode rightKey;
    KeyCode leftKey;

    [Header("HP Settings")]
    [SerializeField] float maxHp = 500f;
    float currentHp;

    public float CurrentHp => currentHp;
    public float MaxHp => maxHp;

    [Header("Run Settings")]
    [SerializeField] float runSpeed = 10.0f;
    [SerializeField] float hpDecreaseRate = 20.0f;
    KeyCode runKey;

    [Header("UI Settings")]
    public UnityEngine.UI.Slider hpSlider;
    
    //Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
        forwardKey = KeyCode.W;
        rightKey = KeyCode.D;
        leftKey = KeyCode.A;
        runKey = KeyCode.LeftShift;

        if (hpSlider != null)
        {
            hpSlider.maxValue = maxHp;
            hpSlider.value = currentHp;
        }
    }

    // Update is called once per frame
    void Update()
    { 
        float currentSpeed = moveSpeed;

        if (Input.GetKey(runKey) && currentHp > 0)
        {
            currentSpeed = runSpeed;
            if (Input.GetKey(forwardKey))
            {
                currentHp -= hpDecreaseRate * Time.deltaTime;
                if (currentHp < 0)
                {
                    currentHp = 0;
                }
            }
        }

        if(Input.GetKey(forwardKey))
        {
            transform.Translate(0, 0, currentSpeed * Time.deltaTime);
        }

        if(Input.GetKey(rightKey))
        {
            transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
        }

        if(Input.GetKey(leftKey))
        {
            transform.Rotate(0, -rotateSpeed * Time.deltaTime, 0);
        }

        if (hpSlider != null)
        {
            hpSlider.value = currentHp;
        }
        
    }
}