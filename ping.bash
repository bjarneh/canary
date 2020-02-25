#!/bin/bash

set -e

while true; do
        now=$(date);
        wget -q --method=POST http://localhost:5000/api/canary/resman.dns/7/s -O /dev/null;
        echo "POST api/canary/resman.dns/7/s | ${now}";
        wget -q --method=POST http://localhost:5000/api/canary/resman.vpn/5/s -O /dev/null;
        echo "POST api/canary/resman.vpn/5/s | ${now}";
        sleep 4;
done;
