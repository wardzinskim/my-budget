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

4. Configure kubernetes auth

```bash
vault auth enable kubernetes
```

```bash
 vault write auth/kubernetes/config \
    kubernetes_host="https://$KUBERNETES_PORT_443_TCP_ADDR:443"
```

```bash
cat <<EOF > /home/vault/mybudget-policy.hcl
path "kv/data/app*" {
  capabilities = ["read"]
}
EOF

vault policy write mybudget /home/vault/mybudget-policy.hcl

vault write auth/kubernetes/role/mybudget \
      bound_service_account_names=mybudget \
      bound_service_account_namespaces=my-budget \
      policies=mybudget \
      ttl=24h

```

5. Run postgres cluster

```bash
kubectl create -f postgres-manifest.yaml
```

6. Destroy all helms

```bash
docker run --rm --net=host -v "${HOME}/.kube:/helm/.kube" -v "${HOME}/.config/helm:/helm/.config/helm" -v "${PWD}:/wd" --workdir /wd ghcr.io/helmfile/helmfile:v0.156.0 helmfile destroy
```
