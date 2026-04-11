# Contributing to dotnet-ignite

Thank you for your interest in contributing!

## Getting Started

1. Fork the repository
2. Clone your fork
3. Create a feature branch: `git checkout -b feature/your-feature`

## Development Setup

Requirements:
- .NET 10 SDK

Install and build:
```bash
dotnet restore
dotnet build
```

## Before Pushing

Always run format check before pushing:
```bash
dotnet format
```

CI will fail if formatting is not correct.

## Pull Request Process

1. Make sure all CI checks pass
2. Push to your fork and open a PR against `develop`
3. Describe what you changed and why

## Commit Convention

Follow this format:
- `feat:` new feature
- `fix:` bug fix
- `chore:` maintenance
- `docs:` documentation
- `style:` formatting
- `test:` tests