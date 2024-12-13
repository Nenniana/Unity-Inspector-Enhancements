using UnityEngine;
using Nenn.InspectorEnhancements;
using Nenn.InspectorEnhancements.Runtime.Attributes;

public class MethodButtonShowcase : MonoBehaviour
{
    [MethodButton]
    public void ToggleActiveState()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    [MethodButton(21f)]
    public void SetPosition(float x, float y = 20, float z = 10)
    {
        transform.position = new Vector3(x, y, z);
    }

    [MethodButton]
    public void SpawnObject(GameObject prefab, Vector3 position)
    {
        if (prefab == null)
        {
            return;
        }

        Instantiate(prefab, position, Quaternion.identity);
    }
}