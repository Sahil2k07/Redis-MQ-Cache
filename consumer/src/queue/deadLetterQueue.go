package queue

import (
	"context"
	"time"

	"github.com/Sahil2k07/Redis-MQ-Go/src/config"
	"github.com/Sahil2k07/Redis-MQ-Go/src/enum"
	"github.com/Sahil2k07/Redis-MQ-Go/src/service"
	"github.com/redis/go-redis/v9"
)

func DeadLetterQueueInit() {
	ctx := context.Background()

	for {
		result, err := config.DeadLetterQueue.BRPop(ctx, 2*time.Minute, enum.DEAD_LETTER_QUEUE_KEY).Result()
		if err != nil {
			if err == redis.Nil {
				continue
			}

			if err == redis.ErrClosed {
				config.DeadLetterQueueClientInit()
			}

			continue
		}

		service.ProcessDeadMessage(result)
	}
}
