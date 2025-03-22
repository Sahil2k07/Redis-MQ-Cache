# Redis-MQ-Cache

Made this simple project to learn `Redis` and implement it as both a `cache` and a `message queue` service. It consists of two services: Producer and Consumer.

1. `Producer` pushes messages to the queue and also uses Redis as a caching layer.

2. `Consumer` runs two parallel queue services:

   - `Main Queue`: Listens for messages from the Producer.

   - `Dead Letter Queue (DLQ)`: If a message fails processing in the main queue, it gets pushed to the DLQ for a retry, simulating real-world fault tolerance in messaging systems.

This project showcases how Redis can be effectively used for asynchronous message processing and caching in distributed applications.

## Tech Used

- `Redis` – Acts as the message broker and caching layer.

- `Docker` – Used to easily set up and run Redis in an isolated container.

- `.NET (C#)` – Implements the Producer, which publishes messages to Redis.

- `Go (Golang)` – Implements the Consumer, which listens for messages and processes them.

## Try it for yourself

### Redis

If you have the Redis package installed locally, then cool

Well if not, use `docker` for ease

1. First pull the redis image

   ```bash
   docker pull redis
   ```

2. Start the container

   ```bash
   docker run --name redis-container -p 6379:6379 -d redis
   ```

   for each consecutive use

   ```bash
   docker start redis-container
   ```

3. If you want to use the Redis-CLI

   ```bash
   docker exec -it redis-container redis-cli
   ```

### Producer

1. Change the directory first

   ```bash
   cd producer
   ```

2. Install all the packages used

   ```bash
   dotnet restore
   ```

3. Start the Producer Server

   ```bash
   dotnet run
   ```

### Consumer

1. In another terminal, navigate to the directory

   ```bash
   cd consumer
   ```

2. Install all the packages locally

   ```bash
   go mod vendor
   ```

3. Start the Consumer service

   ```bash
   go run .
   ```
