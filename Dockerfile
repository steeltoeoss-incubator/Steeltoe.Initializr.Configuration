FROM alpine:3.12 as build
RUN apk add zip
COPY bin/ ./bin/
COPY src/ ./src/
RUN bin/build

FROM nginx:1.19
COPY --from=build build/ /usr/share/initializr/templates/
COPY deploy/docker/initializr-templates.conf /etc/nginx/conf.d/default.conf
