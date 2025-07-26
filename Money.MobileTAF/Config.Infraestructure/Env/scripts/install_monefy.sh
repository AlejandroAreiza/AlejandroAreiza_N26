#!/bin/bash

echo "📦 Installing Monefy APK bundles..."

ADB_PATH=~/Library/Android/sdk/platform-tools/adb

# Check if ADB exists
if ! command -v $ADB_PATH &> /dev/null; then
  echo "❌ ADB not found at $ADB_PATH"
  exit 1
fi

# Check for connected device
DEVICE_COUNT=$($ADB_PATH devices | grep -w "device" | wc -l)

if [ "$DEVICE_COUNT" -eq 0 ]; then
  echo "❌ No Android device or emulator connected."
  exit 1
fi

# Install APKs
$ADB_PATH install-multiple -r ~/Downloads/monefy_base.apk ~/Downloads/monefy_split_arm64.apk ~/Downloads/monefy_split_en.apk ~/Downloads/monefy_split_xxhdpi.apk

if [ $? -eq 0 ]; then
  echo "✅ APK installation succeeded."
else
  echo "❌ APK installation failed."
  exit 1
fi
