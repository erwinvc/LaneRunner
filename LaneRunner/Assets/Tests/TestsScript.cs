using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests {
    public class TestsScript {

        struct Combo {
            public KeyCode code;
            public float time;

            public Combo(KeyCode code, float time) {
                this.code = code;
                this.time = time;
            }
        }

        private float time = 0;
        private int comboIndex = 0;

        private List<Combo> combos = new List<Combo>()
        {
            new Combo(KeyCode.A, 1.06f),
            new Combo(KeyCode.D, 0.55f),
            new Combo(KeyCode.D, 0.89f),
            new Combo(KeyCode.A, 1.19f),
            new Combo(KeyCode.A, 0.79f),
            new Combo(KeyCode.D, 1.01f),
            new Combo(KeyCode.D, 0.14f),
            new Combo(KeyCode.A, 0.30f),
            new Combo(KeyCode.A, 0.13f),
            new Combo(KeyCode.D, 0.35f),
            new Combo(KeyCode.D, 0.13f),
            new Combo(KeyCode.A, 0.23f),
            new Combo(KeyCode.A, 0.28f),
            new Combo(KeyCode.D, 0.44f),
            new Combo(KeyCode.Space, 0.53f),
        };


        [UnityTest]
        public IEnumerator TestLevel() {
            SceneManager.LoadScene("MainScene");
            var player = GameObject.Find("Player");
            PlayerController controller = player.GetComponent<PlayerController>();
            time = Time.time;

            while (true) {
                float cTime = Time.time;
                Debug.Log(cTime);
                if (cTime - time > combos[comboIndex].time) {
                    controller.SimulateKey(combos[comboIndex].code);
                    time = cTime;
                    comboIndex++;
                    Debug.Log(comboIndex);
                }
            }

            Assert.IsFalse(controller.HasDied(), "Player has died!");
        }
    }
}
