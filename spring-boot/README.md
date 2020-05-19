# spring-boot
docker build -t spring-boot .
docker run -p 8080:8080 spring-boot

curl localhost:8080/actuator/health