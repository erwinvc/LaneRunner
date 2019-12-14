using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BuildScript {

    static void PerformBuild() {
        string[] defaultScene = { "Assets/Scenes/MainScene.unity" };
        BuildPipeline.BuildPlayer(defaultScene, "./Build/game.exe", BuildTarget.StandaloneWindows64, BuildOptions.AllowDebugging);
    }
}