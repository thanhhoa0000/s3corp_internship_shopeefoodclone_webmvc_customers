#!/bin/sh

# Set permissions so the app can access all files
chmod -R 777 /uploads/images

echo "[sync] Installing inotify-tools..."
apk update && apk add inotify-tools rsync

echo "[sync] Watching for changes..."

inotifywait -m -r -e modify,create,delete --format '%w%f %e' /uploads/images | while read FILE EVENT
do
  echo "[sync] Change detected at $FILE. Syncing..."
  rsync -a --update /uploads/images/ /host-images/
done