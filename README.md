# Zeebe Dapr Example

An example that allows to orchestrate [Dapr] microservices with the [Zeebe] process engine.

This example contains a .NET microservice that will be orchestrated with the [Zeebe] process engine. The example uses
the Dapr [input](https://docs.dapr.io/reference/components-reference/supported-bindings/zeebe-jobworker/)/
[output](https://docs.dapr.io/reference/components-reference/supported-bindings/zeebe-command/) bindings for Zeebe.

The repository contains a simple Zeebe [BPMN] process
![Alt text](process.png?raw=true "Calc process")

The microservice contains the following endpoints:

| Endpoint                     | Description                                                              |
|------------------------------|--------------------------------------------------------------------------|
| `command/topology`           | Request cluster topology                                                 |
| `command/deploy-process`     | Deploys a process                                                        |
| `command/create-instance`    | Creates an instance of a deployed process                                |
| `command/cancel-instance`    | Cancels a started process instance                                       |
| `command/set-variables`      | Sets new variable for an element (process, activity, ...)                |
| `command/resolve-incident`   | Resolves a job incident                                                  |
| `command/publish-message`    | Publishes a message                                                      |
| `command/activate-jobs`      | Activates jobs                                                           |
| `command/complete-job`       | Completes a job                                                          |
| `command/fail-job`           | Fails a job                                                              |
| `command/update-job-retries` | Updates the job retries                                                  |
| `command/throw-error`        | Throws an error for a job                                                |
| `calc`                       | Worker implementation that will be executed by the Zeebe process engine  |

## Setup Zeebe locally

Camunda provides the [camunda-platform] repository with different docker compose configurations. We use the `docker-compose-core` configuration, which is 
good for development purposes and provides the [Operate] UI.

The following tools need to be installed on the local developer machine. Please refer to the documentation of these tools to learn how to
install them.
- [Docker]
- [Docker compose]

The following commands must be executed to run Zeebe:

```bash
git clone https://github.com/camunda/camunda-platform.git
docker compose -f docker-compose-core.yaml up
```

After all services are started the following URIs can be used:

| Tool                   | URI                   |
|------------------------|-----------------------|
| Operate                | http://localhost:8081 |
| Zeebe Gateway          | localhost:26500       |

The default username and password is: `demo`

# Run the worker service

The following command will run the service `Zeebe.Worker` with dapr. The repo contains some example requests in the `requests` folder, 
which can be executed against the service.

```bash
dotnet run --project "./Zeebe.Worker/Zeebe.Worker.csproj"
```

[BPMN]: https://en.wikipedia.org/wiki/Business_Process_Model_and_Notation
[Zeebe]: https://camunda.com/platform/zeebe/
[camunda-platform]: https://github.com/camunda/camunda-platform
[Operate]: https://docs.camunda.io/docs/components/operate/operate-introduction/
[Elasticsearch]: https://www.elastic.co/de/elasticsearch/
[Dapr]: https://dapr.io/
[Docker]: https://www.docker.com/
[Docker Compose]: https://docs.docker.com/compose/
[Docker Desktop]: https://docs.docker.com/docker-for-windows/install/
