echo Starting tests
'D:/Program Files/Unity installs/2019.2.5f1/Editor/Unity.exe' -quit -runTests -batchmode -projectPath ../LaneRunner/ -testResults C:\temp\results.xml -testPlatform StandaloneWindows64

echo Printing results
cat \temp\results.xml