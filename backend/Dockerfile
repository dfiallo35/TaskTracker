FROM mcr.microsoft.com/dotnet/sdk:8.0 AS builder

WORKDIR /app
COPY . .

RUN dotnet restore && \
    dotnet publish -c PublishRelease -o out

FROM mcr.microsoft.com/dotnet/runtime:8.0

WORKDIR /app

COPY --from=builder /app/out .

RUN adduser \
    --disabled-password \
    --gecos "" \
    --home "/nonexistent" \
    --shell "/sbin/nologin" \
    --no-create-home \
    --uid 10014 \
    "choreo"
# Use the above created unprivileged user
USER 10014

ENTRYPOINT ["./backend"]
