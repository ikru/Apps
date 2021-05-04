#!/bin/sh

docker-compose exec "spfx-debug" bash -c "npm install && gulp trust-dev-cert"
