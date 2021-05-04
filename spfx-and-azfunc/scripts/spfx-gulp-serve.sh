#!/bin/sh

docker-compose exec "spfx-debug" bash -c "gulp trust-dev-cert && gulp serve --nobrowser"
