package main

import (
	"github.com/Sahil2k07/Redis-MQ-Go/src/config"
	"github.com/Sahil2k07/Redis-MQ-Go/src/queue"
)

func init() {
	config.MainQueueClientInit()

	config.DeadLetterQueueClientInit()
}

func main() {
	go queue.MainQueueInit()

	go queue.DeadLetterQueueInit()

	select {}
}
