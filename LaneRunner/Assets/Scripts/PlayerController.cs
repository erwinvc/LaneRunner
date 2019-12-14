using System.Collections;
using System.Collections.Generic;
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
    void Start() {
        m_rb = GetComponent<Rigidbody>();
        m_camera = GetComponentInChildren<Camera>();
    }

    private void FixedUpdate() {
        transform.position += new Vector3(0, 0, 0.5f);

        m_hue += 0.001f;
        if (m_hue > 1.0f) m_hue = 0.0f;
        m_environmentMat.SetVector("_CameraPosition", m_camera.gameObject.transform.position);
        m_environmentMat.SetColor("_Color", Color.HSVToRGB(m_hue, 1, 1));
        m_environmentMat.SetColor("_OutlineColor", Color.HSVToRGB(m_hue, 1.0f, 0.5f));
        m_camera.backgroundColor = Color.HSVToRGB(m_hue, 1, 0.1f);
        //RenderSettings.ambientGroundColor = Color.HSVToRGB(m_hue, 1, 1.0f);
        //RenderSettings.ambientEquatorColor = Color.HSVToRGB(m_hue, 1, 0.5f);
        RenderSettings.ambientSkyColor = Color.HSVToRGB(m_hue, 0.5f, 0.5f);

    }

    void Update() {
        if (!m_jumping && Input.GetKeyDown(KeyCode.Space)) {
            m_jumping = true;
            m_rb.AddForce(new Vector3(0, 35, 0), ForceMode.Impulse);
        }

        if (m_jumping) return;
        if (Input.GetKeyDown(KeyCode.A)) {
            if (m_position == HorizontalPosition.RIGHT) m_position = HorizontalPosition.MIDDLE;
            else if (m_position == HorizontalPosition.MIDDLE) m_position = HorizontalPosition.LEFT;
            SetPosition();
        } else if (Input.GetKeyDown(KeyCode.D)) {
            if (m_position == HorizontalPosition.LEFT) m_position = HorizontalPosition.MIDDLE;
            else if (m_position == HorizontalPosition.MIDDLE) m_position = HorizontalPosition.RIGHT;
            SetPosition();
        }


    }

    void OnCollisionEnter(Collision collision) {
        if (collision.collider.CompareTag("Ground")) m_jumping = false;
        if (collision.collider.CompareTag("Obstacle")) Die();
    }

    void Die() {
        transform.position = new Vector3(0, 0, 0.0f);
    }

    void SetPosition() {
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
