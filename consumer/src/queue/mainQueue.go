package queue

import (
	"context"
	"time"

	"github.com/Sahil2k07/Redis-MQ-Go/src/config"
	"github.com/Sahil2k07/Redis-MQ-Go/src/enum"
	"github.com/Sahil2k07/Redis-MQ-Go/src/service"
	"github.com/redis/go-redis/v9"
)

func MainQueueInit() {
	ctx := context.Background()

	for {
		result, err := config.MainQueue.BRPop(ctx, 30*time.Second, enum.MAIN_QUEUE_KEY).Result()
		if err != nil {
			if err == redis.Nil {
				continue
			}

			if err == redis.ErrClosed {
				config.MainQueueClientInit()
			}

			continue
		}

		err = service.ProcessMessage(result)
		if err != nil {
			go service.PushToDeadLetterQueue(result[1])
		}
	}
}
