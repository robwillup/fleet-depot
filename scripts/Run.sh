#!/bin/bash

# Get the absolute path to this script's directory
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

# Default environment variables
ENV_VARS=""

# Parse arguments
if [[ "$1" == "--connstr" && -n "$2" ]]; then
  export DB_CONNECTION_STRING="$2"
  ENV_VARS="export DB_CONNECTION_STRING='$DB_CONNECTION_STRING';"
else
  export ASPNETCORE_ENVIRONMENT=""
  ENV_VARS="export ASPNETCORE_ENVIRONMENT='';"
fi

run_in_terminal() {
  local title="$1"
  local cmd="$2"

  if [[ "$OSTYPE" == "darwin"* ]]; then
    osascript <<EOF
tell application "Terminal"
  do script "cd \"$SCRIPT_DIR\" && echo \"$title\" && $ENV_VARS $cmd"
end tell
EOF
  elif command -v gnome-terminal &>/dev/null; then
    gnome-terminal -- bash -c "cd \"$SCRIPT_DIR\" && echo \"$title\" && $ENV_VARS $cmd; exec bash"
  else
    echo "No supported terminal found to launch '$title'."
  fi
}

run_in_terminal "FleetDepot API is running..." "dotnet run --project ../src/FleetDepot.Api/FleetDepot.Api.csproj"
run_in_terminal "FleetDepot UI is running..." "dotnet run --project ../src/FleetDepot.Ui/FleetDepot.Ui.csproj"
