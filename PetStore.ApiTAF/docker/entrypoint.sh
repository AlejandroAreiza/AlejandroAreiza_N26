#!/bin/bash
set -e

# Resolve the actual .csproj path based on TEST_PROJECT prefix
if [[ "$TEST_PROJECT" == *.csproj ]]; then
  PROJECT_PATH="$TEST_PROJECT"
else
  # Try to find the full path using wildcard search
  MATCHED_PATH=$(find . -type f -iname "${TEST_PROJECT}.tests.csproj" | head -n 1)
  if [ -z "$MATCHED_PATH" ]; then
    echo "Could not find any test project matching: ${TEST_PROJECT}.tests.csproj"
    exit 1
  fi
  PROJECT_PATH="$MATCHED_PATH"
fi

echo "Running tests for: $PROJECT_PATH"
echo "Filter: ${TEST_FILTER:-[none]}"
echo "Using BASE_URI=${BASE_URI:-http://swaggerapi-petstore3:8080/api}"

RESULTS_DIR="/app/allure-results"
mkdir -p "$RESULTS_DIR"

cd /app

if [ -z "$TEST_FILTER" ]; then
  dotnet test "$PROJECT_PATH" \
    --logger:"trx;LogFileName=TestResults.trx" \
    --results-directory "$RESULTS_DIR" \
    --no-build \
    --nologo
else
  dotnet test "$PROJECT_PATH" \
    --logger:"trx;LogFileName=TestResults.trx" \
    --results-directory "$RESULTS_DIR" \
    --filter "$TEST_FILTER" \
    --no-build \
    --nologo
fi
