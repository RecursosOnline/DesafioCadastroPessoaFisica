# Configuração do build do Angular
FROM node:latest as node
WORKDIR /app
COPY package.json .
RUN npm install
RUN npm install -g @angular/cli
COPY . .
#RUN npm i
#RUN npm run build --prod
EXPOSE 4200
CMD ng serve --host 0.0.0.0 --port 4200
## Configurando nginx para rodar aplicação.
#FROM nginx:latest
#COPY --from=node /app/dist/desafio-cadastro-pessoa-fisica-front-angular/browser /usr/share/nginx/html
##COPY /nginx.conf /etc/nginx/conf.d/default.conf
#EXPOSE 80
#CMD ["nginx", "-g", "daemon off;"]
