#build vue project
FROM node:12.18.1-alpine AS build
WORKDIR /app
copy package.json ./
run npm install
copy . .
run npm run build

#create nginx srever
from nginx:1.19.0-alpine as prod-stage
copy --from=build /app/dist /usr/share/nginx/html
expose 80
cmd ["nginx","-g","daemon off;"]



#
# Dockerfile
#
# FROM alpine AS builder 
# RUN apk add build-base 
# WORKDIR /home
# COPY hello.c .
# RUN gcc "-DARCH=\"`uname -a`\"" hello.c -o hello
 
# FROM alpine 
# WORKDIR /home
# COPY --from=builder /home/hello .
# ENTRYPOINT ["./hello"] 