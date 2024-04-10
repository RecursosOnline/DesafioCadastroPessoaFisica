# Configuração do build do Angular
FROM node:latest as node
WORKDIR /app
COPY . .
RUN npm i
RUN npm run build --prod

# Configurando nginx para rodar aplicação.
FROM nginx:latest
COPY --from=node /app/dist/desafio-cadastro-pessoa-fisica-front-angular /usr/share/nginx/html
