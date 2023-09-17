# Lambda function in docker

```text
1. create ecr
2. build image
3. push to ect
4. create lambda function and choose "Container Image" option
```

```shell
aws ecr get-login-password --region us-east-1 | docker login --username AWS --password-stdin {ecr-url}
```

```shell
docker build -t aws-lambda-docker .
```

```shell
docker tag aws-lambda-docker:latest {ecr-url}/aws-lambda-docker:latest
```

```shell
docker push {ecr-url}/aws-lambda-docker:latest
```