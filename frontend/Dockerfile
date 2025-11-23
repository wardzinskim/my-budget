FROM node:22-alpine as build

RUN apk update && apk upgrade && apk add openjdk17-jre

WORKDIR /usr/src/app

# Copy root package.json and lockfile
COPY package.json ./
COPY package-lock.json ./

COPY apps/web/package.json ./apps/web/package.json 
COPY packages/api-client/package.json ./packages/api-client/package.json
COPY packages/config-eslint/package.json ./packages/config-eslint/package.json
COPY packages/config-typescript/package.json ./packages/config-typescript/package.json
COPY packages/minimal-ui/package.json ./packages/minimal-ui/package.json

RUN npm ci

COPY . .

RUN npm run build

FROM nginxinc/nginx-unprivileged:alpine
USER nginx

COPY --from=build /usr/src/app/apps/web/dist /usr/share/nginx/html
COPY --from=build /usr/src/app/nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 8080

CMD ["nginx", "-g", "daemon off;"]