# Docker for Angular Development
# FROM node:16-alpine3.11
# WORKDIR /app
# COPY ./ReserbizOnlineDocumentation/package.json ./ReserbizOnlineDocumentation/package-lock.json ./
# RUN npm install
# COPY . .
# EXPOSE 4200 49153
# CMD npm run start

# Docker for Angular Production
FROM node:16-alpine3.11 AS builder
# Set working directory
WORKDIR /app
# Copy all files from current directory to working dir in image
COPY ./ReserbizOnlineDocumentation .
# install node modules and build assets
RUN npm i && npm run build

# nginx state for serving content
FROM nginx:alpine
# Set working directory to nginx asset directory
WORKDIR /usr/share/nginx/html
# Remove default nginx static assets
RUN rm -rf ./*
# Copy static assets from builder stage
COPY --from=builder /app/dist/ReserbizOnlineHelp .
# Containers run nginx with global directives and daemon off
ENTRYPOINT ["nginx", "-g", "daemon off;"]
