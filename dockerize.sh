docker build -t carwash-frontend .
docker run -it -p 80:80 --rm --name carwash-frontend-1 carwash-frontend