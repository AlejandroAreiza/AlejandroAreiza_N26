#!/bin/bash
echo "🗑️  Uninstalling app..."

ADB_PATH=~/Library/Android/sdk/platform-tools/adb
PACKAGE_NAME="$1"

if [ -z "$PACKAGE_NAME" ]; then
  echo "❌ Package name not provided. Usage: ./uninstall.sh <package_name>"
  exit 1
fi

# Check if ADB exists
if ! command -v $ADB_PATH &> /dev/null; then
  echo "❌ ADB not found at $ADB_PATH"
  exit 1
fi

# Check for connected device
DEVICE_COUNT=$($ADB_PATH devices | grep -w "device" | wc -l)

if [ "$DEVICE_COUNT" -eq 0 ]; then
  echo "❌ No Android device or emulator connected"
  exit 1
fi

# Run uninstall
$ADB_PATH uninstall "$PACKAGE_NAME"

if [ $? -eq 0 ]; then
  echo "✅ App '$PACKAGE_NAME' uninstalled successfully"
else
  echo "⚠️  App '$PACKAGE_NAME' uninstall failed or was not installed"
  exit 1
fi
