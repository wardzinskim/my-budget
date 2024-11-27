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
cat <<EOF > /home/vault/my-budget-api-sa-policy.hcl
path "kv/data/app/mybudget.api*" {
  capabilities = ["read"]
}
EOF

vault policy write my-budget-api-sa /home/vault/my-budget-api-sa-policy.hcl

vault write auth/kubernetes/role/my-budget-api-sa \
      bound_service_account_names=my-budget-api-sa \
      bound_service_account_namespaces=my-budget \
      policies=my-budget-api-sa \
      ttl=24h

```

```bash
cat <<EOF > /home/vault/my-budget-identity-policy.hcl
path "kv/data/app/mybudget.identity*" {
  capabilities = ["read"]
}
EOF

vault policy write my-budget-identity-sa /home/vault/my-budget-identity-policy.hcl

vault write auth/kubernetes/role/my-budget-identity-sa \
      bound_service_account_names=my-budget-identity-sa \
      bound_service_account_namespaces=my-budget \
      policies=my-budget-identity-sa \
      ttl=24h

```

```bash
cat <<EOF > /home/vault/my-budget-frontend-sa-policy.hcl
path "kv/data/app/mybudget.frontend*" {
  capabilities = ["read"]
}
EOF

vault policy write my-budget-frontend-sa /home/vault/my-budget-frontend-sa-policy.hcl

vault write auth/kubernetes/role/my-budget-frontend-sa \
      bound_service_account_names=my-budget-frontend-sa \
      bound_service_account_namespaces=my-budget \
      policies=my-budget-frontend-sa \
      ttl=24h

```

5. Run postgres cluster

```bash
kubectl create -f postgres-manifest.yaml
```

6. install apps

```bash
helm install  my-budget-frontend .\chart\ --values .\my-budget-frontend-chart-overrides.yaml
```

7. Destroy all helms

```bash
docker run --rm --net=host -v "${HOME}/.kube:/helm/.kube" -v "${HOME}/.config/helm:/helm/.config/helm" -v "${PWD}:/wd" --workdir /wd ghcr.io/helmfile/helmfile:v0.156.0 helmfile destroy
```
