#!/usr/bin/env nu

def main [] {
    [1, 2] | par-each { |it|
	    if $it == 1 {
		    cd GameServer/bin/Debug/net8.0
		    ./GameServer.exe
		} else if $it == 2 {
		    cd SDKServer/bin/Debug/net8.0
		    ./SDKServer.exe
		}
	}
}
