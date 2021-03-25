# KeyCloak Proof of Concept

This project is a proof of concept, using KeyCloak as a self hosted identity provider, with an example .Net 5 API.

## KeyCloak

KeyCloak is an open source identity manager, providing user registration and administration; supports recognised protocols including OpenID Connect and OAuth 2.0; can support third party providers such as Google and Facebook login; and includes optional default GUI interfaces for many processes. It is developed by JBoss, a division of RedHat.

KeyCloak is a Java application.

https://www.keycloak.org/

## Project Overview

This project is using the Docker image of KeyCloak provided by JBoss - https://hub.docker.com/r/jboss/keycloak. This makes it simpler to run as we don't have to setup the Java environment manually. Alternatively, we could set up the Java environment on our own machines, or use a virtual machine, for example on Azure or AWS. The latest Docker image, 12.0.4 has a bug that prevents startup, so for this example, the version has been pinned to 12.0.3.

KeyCloak uses realms to define tenants, and a master realm is created by default for managing KeyCloak - this should not be used for applications. A new realm should created for our application, along with at least one new client.

This project includes a simple API which shows how authorization and authentication can be handled using KeyVault with bearer tokens.

## Pre-requisites

The following should be installed on your machine:
- Docker
- Visual Studio / VS Code with support for .Net 5.
- Postman

## Setup

- Run the docker-compose file with command `docker-compose up`. This will create an instance of Microsoft Sql Server, create a KeyCloak table, and run the KeyCloak instance.

- Run the SimpleApi project with `dotnet run` or from within VS Code / Visual Studio

## Walkthrough

1. Access the KeyCloak portal

The keycloak portal will be available at http://localhost:8080. A default login is created with username `admin` and password `admin`. The portal can be used to change the configuration, and manage users, including creating users, resetting passwords and updating roles.

2. Obtain a token

With Postman, make a POST request to http://localhost:8080/auth/realms/mytestapp/protocol/openid-connect/token with an x-www-form-urlencoded body of:
```
grant_type:password
username:myuser
password:password
client_id:myApi
```

