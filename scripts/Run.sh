#!/bin/bash

# Get the absolute path to this script's directory
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

run_in_terminal() {
  local title="$1"
  local cmd="$2"

  if [[ "$OSTYPE" == "darwin"* ]]; then
    osascript <<EOF
tell application "Terminal"
  do script "cd \"$SCRIPT_DIR\" && echo \"$title\" && $cmd"
end tell
EOF
  elif command -v gnome-terminal &>/dev/null; then
    gnome-terminal -- bash -c "cd \"$SCRIPT_DIR\" && echo \"$title\" && $cmd; exec bash"
  else
    echo "No supported terminal found to launch '$title'."
  fi
}

run_in_terminal "FleetDepot API is running..." "dotnet run --project ../src/FleetDepot.Api/FleetDepot.Api.csproj"
run_in_terminal "FleetDepot UI is running..." "dotnet run --project ../src/FleetDepot.Ui/FleetDepot.Ui.csproj"
