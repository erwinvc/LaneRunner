echo Cleaning Up Build Directory
rm -rf ../LaneRunner/Build/

echo Starting Build Process
'D:/Program Files/Unity installs/2019.2.5f1/Editor/Unity.exe' -nographics -quit -bachmode -projectPath ../LaneRunner/ -executeMethod BuildScript.PerformBuild ${JOB_NAME} "${JENKINS_HOME}"/jobs/${JOB_NAME}/builds/"${BUILD_NUMBER}"/output
echo Ended Build Process