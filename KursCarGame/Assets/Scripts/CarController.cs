using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    public float horizontalInput;
    public float verticalInput;

    private float currentSteerAngle;
    private float currentSteerForce;
    private float currentBreakForce;

    public bool isBreaking;

    public float motorForce;
    public float motorForceEquals;
    public float breakForce;
    public float maxSteeringAngle;

    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    public WheelCollider rearLeftWheelCollider;
    public WheelCollider rearRightWheelCollider;

    public Transform frontLeftWheelTransform;
    public Transform frontRightWheelTransform;
    public Transform rearLeftWheelTransform;
    public Transform rearRightWheelTransform;

    Rigidbody rb;
    //public GameObject nitro;
    public Image nitroRed;
    public float nitroDeger = 0;
    public bool nitroDurum = true;

    //public bool nitrosBool = false;

    public Volume vol;
    ChromaticAberration aberration;
    MotionBlur motion;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        vol.profile.TryGet<ChromaticAberration>(out aberration);
        vol.profile.TryGet<MotionBlur>(out motion);
        StartCoroutine(nitroBar());
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.V))
        {
            if(!nitroDurum)
            {
                rb.velocity += 0.7f * rb.velocity.normalized;
                //nitro.SetActive(true);

                aberration.intensity.value = Mathf.Lerp(aberration.intensity.value, 1, 20 * Time.deltaTime);
                motion.intensity.value = Mathf.Lerp(motion.intensity.value, 1f, 5 * Time.deltaTime);

                nitroDeger -= 2;

                nitroRed.fillAmount = nitroDeger / 100;

                if(nitroDeger <= 0)
                {
                    //nitro.SetActive(false);
                    nitroDeger = 0;
                    nitroDurum = true;
                    return;
                }
            }
        }
        else
        {
            //nitro.SetActive(false);
            nitroDurum = true;
            aberration.intensity.value = Mathf.Lerp(aberration.intensity.value, 0, 2f * Time.deltaTime);
            motion.intensity.value = Mathf.Lerp(motion.intensity.value , 0f , 1 * Time.deltaTime);
        }
    }

    IEnumerator nitroBar()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);

            if (nitroDurum)
            {
                nitroDeger += 2;

                nitroRed.fillAmount = nitroDeger / 100;

                if (nitroDeger >= 100)
                {
                    nitroDeger = 100;
                    nitroDurum = false;
                }
            }
        }

    }

    private void FixedUpdate()
    {
        if(!isBreaking)
        {
            ApplyBreaking();
        }
        else
        {
            frontLeftWheelCollider.brakeTorque = breakForce;
            frontRightWheelCollider.brakeTorque = breakForce;
            rearLeftWheelCollider.brakeTorque = breakForce;
            rearRightWheelCollider.brakeTorque = breakForce;
        }

        GetInput();
        HandleMotor();
        UpdateWheels();
        HandleSteering();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
    }

    private void ApplyBreaking()
    {
        frontLeftWheelCollider.brakeTorque = currentBreakForce;
        frontRightWheelCollider.brakeTorque = currentBreakForce;
        rearLeftWheelCollider.brakeTorque = currentBreakForce;
        rearRightWheelCollider.brakeTorque = currentBreakForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteeringAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheels(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheels(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheels(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateSingleWheels(rearRightWheelCollider, rearRightWheelTransform);
    }

    private void UpdateSingleWheels(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Finish"))
        {

        }
    }
}