#declare variables
unityVersion=$1
hubLocation=$2
projectPath=$3
buildMethod=$4

#build method would be SDK.Builder.GameSDKAndroidBuilder.BuildAndroid
#run unity build method

$hubLocation/$unityVersion/Unity.app/Contents/MacOS/Unity -projectPath $projectPath -batchmode -nographics -executeMethod $buildMethod -buildTarget android -logFile