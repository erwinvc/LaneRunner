echo Cleaning Up Build Directory
rm -rf ../LaneRunner/Build/

echo Starting Build Process
'D:/Program Files/Unity installs/2019.2.5f1/Editor/Unity.exe' -quit -bachmode -projectPath ../LaneRunner/ -executeMethod BuildScript.PerformBuild
echo Ended Build Process