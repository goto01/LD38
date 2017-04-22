using UnityEngine;
public class DisplayFPS : MonoBehaviour
{
    float deltaTime = 0.0f;

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
    }

    void OnGUI()
    {
        Rect rect = new Rect(0, 50, 300, 100);
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.color = Color.blue;
        var style = new GUIStyle(GUI.skin.label);
        style.fontSize = 30;
        GUI.Label(rect, text, style);
        GUI.color = Color.blue;

    }
    
}