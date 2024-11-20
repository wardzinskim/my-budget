1. Install helm

- https://helm.sh/docs/intro/install/

2. Install all required helm

```bash
docker run --rm --net=host -v "${HOME}/.kube:/helm/.kube" -v "${HOME}/.config/helm:/helm/.config/helm" -v "${PWD}:/wd" --workdir /wd ghcr.io/helmfile/helmfile:v0.156.0 helmfile sync
```

3. Inside vault container

```bash
vault operator init
```

3. Run postgres cluster

```bash
kubectl create -f postgres-manifest.yaml
```

4. Destroy all helms

```bash
docker run --rm --net=host -v "${HOME}/.kube:/helm/.kube" -v "${HOME}/.config/helm:/helm/.config/helm" -v "${PWD}:/wd" --workdir /wd ghcr.io/helmfile/helmfile:v0.156.0 helmfile destroy
```
