using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests {
    public class TestsScript {

        struct KeyTimePair {
            public KeyCode code;
            public float time;

            public KeyTimePair(KeyCode code, float time) {
                this.code = code;
                this.time = time;
            }
        }

        private List<KeyTimePair> combos = new List<KeyTimePair>()
        {
            new KeyTimePair(KeyCode.A, 1.06f),
            new KeyTimePair(KeyCode.D, 0.55f),
            new KeyTimePair(KeyCode.D, 0.89f),
            new KeyTimePair(KeyCode.A, 1.19f),
            new KeyTimePair(KeyCode.A, 0.79f),
            new KeyTimePair(KeyCode.D, 1.01f),
            new KeyTimePair(KeyCode.D, 0.14f),
            new KeyTimePair(KeyCode.A, 0.30f),
            new KeyTimePair(KeyCode.A, 0.13f),
            new KeyTimePair(KeyCode.D, 0.35f),
            new KeyTimePair(KeyCode.D, 0.13f),
            new KeyTimePair(KeyCode.A, 0.23f),
            new KeyTimePair(KeyCode.A, 0.28f),
            new KeyTimePair(KeyCode.D, 0.44f),
            new KeyTimePair(KeyCode.Space, 0.53f),
            new KeyTimePair(KeyCode.Space, 2.00f),
        };


        [UnityTest]
        public IEnumerator TestLevel() {
            int comboIndex = 0;
            var worldPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/World.prefab");
            var playerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Player.prefab");
            Assert.NotNull(worldPrefab, "World prefab null");
            Assert.NotNull(playerPrefab, "Player prefab null");

            GameObject world = GameObject.Instantiate(worldPrefab);
            GameObject player = GameObject.Instantiate(playerPrefab);
            Assert.NotNull(world, "World null");
            Assert.NotNull(player, "Player null");

            PlayerController controller = player.GetComponent<PlayerController>();
            float time = Time.time;

            while (comboIndex < combos.Count) {
                float cTime = Time.time;
                if (cTime - time > combos[comboIndex].time) {
                    controller.OnKey(combos[comboIndex].code);
                    time = cTime;
                    comboIndex++;
                }
                yield return null;
            }

            GameObject.DestroyImmediate(world);
            GameObject.DestroyImmediate(player);

            Assert.IsFalse(controller.HasDied(), "Player has died!");
        }

        private List<KeyTimePair> combosMashing = new List<KeyTimePair>()
        {
            new KeyTimePair(KeyCode.A, 0.567f),
            new KeyTimePair(KeyCode.Space, 1.959f),
            new KeyTimePair(KeyCode.Space, 1.582f),
            new KeyTimePair(KeyCode.Space, 1.131f),
        };

        [UnityTest]
        public IEnumerator TestLevelJumpMashing() {
            int comboIndex = 0;
            var worldPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/World.prefab");
            var playerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Player.prefab");
            Assert.NotNull(worldPrefab, "World prefab null");
            Assert.NotNull(playerPrefab, "Player prefab null");

            GameObject world = GameObject.Instantiate(worldPrefab);
            GameObject player = GameObject.Instantiate(playerPrefab);
            Assert.NotNull(world, "World null");
            Assert.NotNull(player, "Player null");

            PlayerController controller = player.GetComponent<PlayerController>();
            float time = Time.time;

            while (comboIndex < combosMashing.Count) {
                float cTime = Time.time;
                if (cTime - time > combosMashing[comboIndex].time) {
                    controller.OnKey(combosMashing[comboIndex].code);
                    time = cTime;
                    comboIndex++;
                }
                yield return null;
            }

            GameObject.DestroyImmediate(world);
            GameObject.DestroyImmediate(player);

            Assert.IsTrue(controller.HasDied(), "Player hasn't died!");
        }
    }
}
