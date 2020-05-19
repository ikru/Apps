FROM maven:3.6.3-jdk-8 AS MAVEN_BUILD
WORKDIR /build/

COPY pom.xml /build/
RUN mvn dependency:go-offline -Dmaven.repo.local=/build/repository

COPY src /build/src/
RUN mvn package -Dmaven.repo.local=/build/repository

FROM openjdk:8-jre-alpine
WORKDIR /app

COPY --from=MAVEN_BUILD /build/target/spring-boot-0.1.0.jar /app/

ENTRYPOINT ["java", "-jar", "spring-boot-0.1.0.jar"]