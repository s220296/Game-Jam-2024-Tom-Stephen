using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class DestructibleObject : MonoBehaviour
{
    public enum DestructionType
    {
        NONE = 0
        , EXPLOSION
        , DIRECTION
    }

    [SerializeField] private GameObject _brokenVersion;
    [SerializeField] private DestructionType _destructionType;
    [SerializeField, Tooltip("If destruction type is explosion; only use X component")] 
    private Vector3 _destructionForce;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void ApplyDestructionForce(GameObject broken)
    {
        if (broken.transform.childCount == 0)
        {
            Rigidbody bRb = broken.GetComponent<Rigidbody>();
            if (bRb) bRb.AddForce(_destructionForce);
            return;
        }
        
        for(int i = 0; i < broken.transform.childCount; i++)
        {
            Transform child = broken.transform.GetChild(i);
            
            Rigidbody childRb = null;
            if (child) childRb = child.GetComponent<Rigidbody>();
            if (childRb)
            {
                switch(_destructionType)
                {
                    case DestructionType.EXPLOSION:
                    childRb.AddExplosionForce(_destructionForce.x, broken.transform.position, 100f);
                        break;
                    case DestructionType.DIRECTION:
                    childRb.AddForce(_destructionForce);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void Destruct()
    {
        GameObject broken = Instantiate(_brokenVersion, transform.position, transform.rotation);

        ApplyDestructionForce(broken);

        Destroy(broken, 15f);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
