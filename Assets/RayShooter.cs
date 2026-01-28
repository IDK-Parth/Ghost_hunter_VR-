using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayGun : MonoBehaviour
{
    // Use this OVR button when in VR
    public OVRInput.RawButton shootingButton = OVRInput.RawButton.RIndexTrigger;

    public LayerMask layerMask;
    public LineRenderer linePrefab;
    public GameObject rayImpactPrefab;
    // For VR, assign the controller's transform; for desktop, you can assign an origin point.
    public Transform shootingPoint; 
    public AudioSource source;
    public AudioClip shootingAudioClip;
    public float maxLineDistance = 25f;
    public float lineShowTimer = 0.3f;
    public ParticleSystem msVFX_StylizedSmoke;
    

    void Update()
    {
        // Auto-detect input method.
        if (OVRManager.isHmdPresent)
        {
            // If a VR headset is present, listen for the OVR button.
            if (OVRInput.GetDown(shootingButton))
            {
                // For VR, cast the ray from the shootingPoint (usually the controller's transform)
                Ray ray = new Ray(shootingPoint.position, shootingPoint.forward);
                ShootRay(ray, shootingPoint.position);
            }
        }
        else
        {
            // If no VR headset detected, use mouse input.
            if (Input.GetMouseButtonDown(0))
            {
                // Cast a ray from the main camera to the mouse position.
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                // Using shootingPoint.position as the line's start gives you a consistent origin
                ShootRay(ray, shootingPoint.position);
            }
        }
    }

    // Common method for shooting a ray and processing the hit
    void ShootRay(Ray ray, Vector3 startPoint)
    {
        bool hasHit = Physics.Raycast(ray, out RaycastHit hit, maxLineDistance, layerMask);
        Vector3 endPoint = Vector3.zero;

        if (hasHit)
        {
            endPoint = hit.point;

            // If the hit object is tagged as "Ghost", destroy it.
            if (hit.collider.CompareTag("Ghost"))
            {
                Destroy(hit.collider.gameObject);
            }

            // Create an impact effect at the hit point.
            Quaternion rayImpactRotation = Quaternion.LookRotation(-hit.normal);
            GameObject rayImpact = Instantiate(rayImpactPrefab, hit.point, rayImpactRotation);
            Destroy(rayImpact, 1f);
        }
        else
        {
            endPoint = ray.origin + ray.direction * maxLineDistance;
        }

        // Create a visual line effect for the ray.
        LineRenderer line = Instantiate(linePrefab);
        line.positionCount = 2;
        line.SetPosition(0, startPoint);
        line.SetPosition(1, endPoint);
        Destroy(line.gameObject, lineShowTimer);

        // Play the shooting sound if available.
        if (source && shootingAudioClip)
        {
            source.PlayOneShot(shootingAudioClip);
        }
    }
}
