using UnityEngine;

public class FieldDrawingTest : MonoBehaviour
{
    public int integerField = 10;
    public float floatField = 3.14f;
    public bool boolField = true;
    public string stringField = "Hello, World!";
    public GameObject gameObjectField;
    public CustomClass complexField = new CustomClass();
    public Vector2 Vector2;
    public Vector3 Vector3;

    [System.Serializable]
    public class CustomClass
    {
        public int nestedInt = 42;
        public string nestedString = "Nested Hello";
    }
}
