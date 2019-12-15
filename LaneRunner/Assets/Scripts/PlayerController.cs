using System.Collections;
using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public enum HorizontalPosition {
        LEFT,
        MIDDLE,
        RIGHT
    }

    private bool m_jumping = false;
    private Rigidbody m_rb;
    HorizontalPosition m_position = HorizontalPosition.MIDDLE;
    public Material m_environmentMat;
    private Camera m_camera;
    private float m_hue = 0;
    private bool m_died = false;

    void Start() {
        m_rb = GetComponent<Rigidbody>();
        m_camera = GetComponentInChildren<Camera>();
    }

    private void FixedUpdate() {
        transform.position += new Vector3(0, 0, 0.5f);
        time = Time.time;
        m_hue += 0.001f;
        if (m_hue > 1.0f) m_hue = 0.0f;
        m_environmentMat.SetVector("_CameraPosition", m_camera.gameObject.transform.position);
        m_environmentMat.SetColor("_Color", Color.HSVToRGB(m_hue, 1, 1));
        m_environmentMat.SetColor("_OutlineColor", Color.HSVToRGB(m_hue, 1.0f, 0.5f));
        m_camera.backgroundColor = Color.HSVToRGB(m_hue, 1, 0.1f);
        RenderSettings.ambientSkyColor = Color.HSVToRGB(m_hue, 0.5f, 0.5f);

    }

    private float time;

    struct KeyTimePair {
        public KeyCode code;
        public float time;

        public KeyTimePair(KeyCode code, float time) {
            this.code = code;
            this.time = time;
        }
    }

    private List<KeyTimePair> pairs = new List<KeyTimePair>();

    void RegisterKey(KeyCode code) {
        float cTime = Time.time;
        pairs.Add(new KeyTimePair(code, cTime - time));
        time = cTime;
    }


    public void OnKey(KeyCode keyCode) {
        RegisterKey(keyCode); 
        switch (keyCode) {
            case KeyCode.A: {
                    if (!m_jumping) {
                        if (m_position == HorizontalPosition.RIGHT) m_position = HorizontalPosition.MIDDLE;
                        else if (m_position == HorizontalPosition.MIDDLE) m_position = HorizontalPosition.LEFT;
                        MovePlayerToHorizontalPosition();
                    }
                }
                break;
            case KeyCode.D: {
                    if (!m_jumping) {
                        if (m_position == HorizontalPosition.LEFT) m_position = HorizontalPosition.MIDDLE;
                        else if (m_position == HorizontalPosition.MIDDLE) m_position = HorizontalPosition.RIGHT;
                        MovePlayerToHorizontalPosition();
                    }
                }
                break;
            case KeyCode.Space: {
                    if (!m_jumping) {
                        m_jumping = true;
                        m_rb.AddForce(new Vector3(0, 35, 0), ForceMode.Impulse);
                    }
                    break;
                }
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.A)) OnKey(KeyCode.A);
        if (Input.GetKeyDown(KeyCode.D)) OnKey(KeyCode.D);
        if (Input.GetKeyDown(KeyCode.Space)) OnKey(KeyCode.Space);

        if (Input.GetKeyDown(KeyCode.H)) {
            string[] str = new string[pairs.Count];
            for (int i = 0; i < pairs.Count; i++) {
                str[i] = pairs[i].time + " + " + pairs[i].code;
            }
            File.WriteAllLines("test.txt", str);
        }

    }

    void OnCollisionEnter(Collision collision) {
        if (collision.collider.CompareTag("Ground")) m_jumping = false;
        if (collision.collider.CompareTag("Obstacle")) Die();
    }

    void Die() {
        transform.position = new Vector3(0, 0, 0.0f);
        m_died = true;
    }

    public bool HasDied() {
        return m_died;
    }

    void MovePlayerToHorizontalPosition() {
        Vector3 position = transform.position;
        switch (m_position) {
            case HorizontalPosition.LEFT:
                position.x = -1.5f; break;
            case HorizontalPosition.MIDDLE:
                position.x = 0.0f; break;
            case HorizontalPosition.RIGHT:
                position.x = 1.5f; break;
        }
        transform.position = position;
    }
}
