package config

import (
	"context"

	"github.com/redis/go-redis/v9"
)

var MainQueue *redis.Client
var DeadLetterQueue *redis.Client

func MainQueueClientInit() {
	client := redis.NewClient(&redis.Options{
		Addr:     "localhost:6379",
		Password: "",
		DB:       0,
		Protocol: 2,
	})

	if err := checkConnection(client); err != nil {
		panic("No main queue")
	}

	MainQueue = client
}

func DeadLetterQueueClientInit() {
	client := redis.NewClient(&redis.Options{
		Addr:     "localhost:6379",
		Password: "",
		DB:       1,
		Protocol: 2,
	})

	if err := checkConnection(client); err != nil {
		panic("No dead letter queue")
	}

	DeadLetterQueue = client
}

func checkConnection(client *redis.Client) error {
	ctx := context.Background()

	_, err := client.Ping(ctx).Result()

	return err
}
