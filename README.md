# Zeebe Dapr Example

An example that allows to orchestrate [Dapr] microservices with the [Zeebe] process engine.

This example contains a .NET microservice that will be orchestrated with the [Zeebe] process engine. The example uses
the new [Dapr input/output bindings](https://docs.dapr.io/developing-applications/building-blocks/bindings/) for Zeebe, 
which will bew released with Dapr version 1.2.

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

## Setup Zeebe

There are two possibilities to setup Zeebe locally:

- Run Zeebe with Kubernetes
- Run Zeebe with Docker

For development purpose it's easier to run Zeebe with docker. For testing Zeebe with the developed services it makes sense to test it all in Kubernetes.

### Docker installation

Camunda provides the [zeebe-docker-compose] repository with different docker compose configurations. We use the `operate-simple-monitor` configuration, which is 
good for development purposes. It provides the [Operate] UI as also the [Zeebe Simple Monitor] tool.

The following tools need to be installed on the local developer machine. Please refer to the documentation of these tools to learn how to
install them.
- [Docker]
- [Docker compose]

The following commands must be executed to run Zeebe:

```bash
git clone https://github.com/zeebe-io/zeebe-docker-compose.git
cd zeebe-docker-compose/operate-simple-monitor
docker-compose up
```

After all all services are started the following URIs can be used:

| Tool                   | URI                    |
|------------------------|------------------------|
| Zeebe Simple Monitor   | http://localhost:8082  |
| Operate                | http://localhost:8080  |
| Zeebe Gateway          | localhost:26500        |

### Kubernetes installation

The following tools need to be installed on the local developer machine. Please refer to the documentation of these tools to learn how to 
install them.
- [Docker]
- [k3d]  
- [Helm]
- [Dapr]
- [kubectl]

To run [Zeebe] locally we need a local [Kubernetes] cluster installed. There are multiple possibilities to spin up a local cluster. 
This example will use [k3d] which is a lightweight wrapper to run [k3s] (Rancher Lab’s lightweight Kubernetes distribution) in docker.

**Note**: If you already have a local cluster or if you prefer to use another local Kubernetes distribution like [minikube], [microk8s], [kind] or
[Docker Desktop], please jump to [Install Zeebe into the cluster](#install-zeebe-into-the-cluster).

#### Install k3d and spin up cluster

The installation process for various operating systems is described in the [k3d installation page]. After k3d was installed, we create 
a new cluster. The cluster needs at least three nodes because the [Operate] instance that we later install with [Zeebe], installs an 
[Elasticsearch] cluster which needs three nodes. We also disable the default ingress controller that comes with [k3d] because the [Zeebe]
cluster that we install, will deploy it's own ingress controller. The ports for the ingress controller will be mapped to localhost:8080 
for HTTP and localhost:8443 for HTTPS.

```bash
k3d cluster create zeebe \
    --servers 3 \
    --k3s-server-arg "--no-deploy=traefik" \
    -p 8080:80@loadbalancer \
    -p 8443:443@loadbalancer
```

#### Install Zeebe into the cluster

Zeebe provides a [Helm] chart that deploys a complete [Zeebe] Cluster, the Operate UI and an ingress controller to [Kubernetes]. To use this 
helm chart we must add the official [Zeebe Helm] repo to [Helm]:

```bash
helm repo add zeebe https://helm.zeebe.io && helm repo update
```

Now we can install the helm chart into our cluster:
```bash
helm install zeebe-cluster zeebe/zeebe-full
```

The installation process takes some time. With the `kubeclt` command we can monitor the progress.

```bash
kubectl get pods
```

If the cluster is ready it shows something similar to this:
```                                                         
NAME                                                           READY   STATUS    RESTARTS   AGE
elasticsearch-master-0                                         1/1     Running   0          26m
elasticsearch-master-1                                         1/1     Running   0          26m
elasticsearch-master-2                                         1/1     Running   0          26m
svclb-zeebe-cluster-nginx-ingress-controller-97bvx             2/2     Running   0          26m
svclb-zeebe-cluster-nginx-ingress-controller-hcv4c             2/2     Running   0          26m
svclb-zeebe-cluster-nginx-ingress-controller-scgkv             2/2     Running   0          26m
zeebe-cluster-nginx-ingress-controller-c574d7794-wjfkx         1/1     Running   0          26m
zeebe-cluster-nginx-ingress-default-backend-68b8969bf4-jvftm   1/1     Running   0          26m
zeebe-cluster-operate-7b88d758fd-rsnpz                         1/1     Running   1          26m
zeebe-cluster-tasklist-678f4f7b9d-7tt8t                        1/1     Running   1          26m
zeebe-cluster-zeebe-0                                          1/1     Running   0          26m
zeebe-cluster-zeebe-1                                          1/1     Running   0          26m
zeebe-cluster-zeebe-2                                          1/1     Running   0          26m
zeebe-cluster-zeebe-gateway-7fdf747878-jr9tp                   1/1     Running   0          26m
```

Now we are able to access the [Operate] UI over `https://localhost:8443` and login with user `demo` and password `demo`.

# Run the worker service

The following command will run the service `Zeebe.Worker` with dapr. The repo contains some example requests in the `requests` folder, 
which can be executed against the service.

```bash
dapr run --app-id zeebe-worker --app-port 5000 --dapr-http-port 5001 --components-path ./Zeebe.Worker/components -- dotnet run --project "./Zeebe.Worker/Zeebe.Worker.csproj"
```
[BPMN]: https://en.wikipedia.org/wiki/Business_Process_Model_and_Notation
[Zeebe]: https://zeebe.io/
[Zeebe Helm]: https://helm.zeebe.io
[Zeebe Simple Monitor]: https://github.com/camunda-community-hub/zeebe-simple-monitor
[zeebe-docker-compose]: https://github.com/zeebe-io/zeebe-docker-compose
[Operate]: https://docs.zeebe.io/operate-user-guide/index.html
[Elasticsearch]: https://www.elastic.co/de/elasticsearch/
[Dapr]: https://dapr.io/
[Kubernetes]: https://kubernetes.io/
[Helm]: https://helm.sh/
[kubectl]: https://kubernetes.io/docs/reference/kubectl/kubectl/
[k3d]: https://k3d.io/
[k3s]: https://k3s.io/
[minikube]: https://minikube.sigs.k8s.io/
[microk8s]: https://microk8s.io/
[kind]: https://kind.sigs.k8s.io/
[Docker]: https://www.docker.com/
[Docker Compose]: https://docs.docker.com/compose/
[Docker Desktop]: https://docs.docker.com/docker-for-windows/install/
[k3d installation page]: https://k3d.io/#installation