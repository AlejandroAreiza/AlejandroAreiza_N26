#!/bin/bash
echo "📱 Installing APK(s)"
ADB_PATH="$HOME/Library/Android/sdk/platform-tools/adb"
APK_DIR="$1"

if [ ! -x "$ADB_PATH" ]; then
  echo "❌ ADB not found or not executable at $ADB_PATH"
  exit 1
fi

if [ -z "$APK_DIR" ] || [ ! -d "$APK_DIR" ]; then
  echo "❌ Invalid or missing APK directory"
  exit 1
fi

# Check for connected devices
DEVICE_COUNT=$($ADB_PATH devices | grep -w "device" | wc -l)

if [ "$DEVICE_COUNT" -eq 0 ]; then
  echo "❌ No Android device or emulator connected"
  exit 1
fi

APK_FILES=("$APK_DIR"/*.apk)

if [ ${#APK_FILES[@]} -eq 0 ]; then
  echo "❌ No APKs found in $APK_DIR"
  exit 1
fi

# Install all APKs as a bundle
"$ADB_PATH" install-multiple -r "${APK_FILES[@]}"
RESULT=$?

if [ $RESULT -eq 0 ]; then
  echo "✅ APK installation succeeded"
else
  echo "❌ APK installation failed"
  exit $RESULT
fi