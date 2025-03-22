package service

import (
	"context"
	"log"

	"github.com/Sahil2k07/Redis-MQ-Go/src/config"
	"github.com/Sahil2k07/Redis-MQ-Go/src/enum"
)

func PushToDeadLetterQueue(message string) error {
	ctx := context.Background()

	_, err := config.DeadLetterQueue.LPush(ctx, enum.DEAD_LETTER_QUEUE_KEY, message).Result()
	if err != nil {
		log.Println("Error pushing to DLQ")
	}

	return err
}
