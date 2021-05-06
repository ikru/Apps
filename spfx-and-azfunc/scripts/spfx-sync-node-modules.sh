#!/bin/sh

docker-compose exec "spfx-debug" bash -c "rsync --archive --verbose --copy-links /npm/node_modules/ ./node_modules/"

