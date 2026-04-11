# dotnet-ignite 🔥

> A CLI scaffolding tool that eliminates boilerplate by generating code files based on your project's architecture and installed packages. The `ng generate` for the .NET ecosystem.

![Build](https://img.shields.io/github/actions/workflow/status/saijaku0/dotnet-ignite/build.yml?branch=main&style=flat-square)
![NuGet](https://img.shields.io/nuget/v/dotnet-ignite?style=flat-square)
![License](https://img.shields.io/github/license/saijaku0/dotnet-ignite?style=flat-square&v=1)

---

## The Problem

Every time you add a new feature to a Clean Architecture or CQRS project, you manually create the same set of files:

- Entity + EF Core Configuration
- Repository interface + implementation
- Command / Query + Handler
- Validator
- Controller or Minimal API endpoint

This is repetitive, error-prone, and pulls your focus away from actual business logic.

## The Solution

`dotnet-ignite` reads your project structure and installed NuGet packages, then generates all the files you need — correctly named, correctly namespaced, and wired together.

```bash
ignite add feature CreateOrder
```

That single command scaffolds the entire feature flow based on your architecture.

---

## Installation

Requires [.NET 8 SDK](https://dotnet.microsoft.com/download) or later.

```bash
dotnet tool install -g dotnet-ignite
```

Verify the installation:

```bash
ignite --version
```

---

## Quick Start

**1. Initialize ignite in your project root:**

```bash
ignite init
```

This starts an interactive prompt that asks about your architecture, layer paths, and creates a `.ignite.json` config file.

**2. Generate a full feature:**

```bash
ignite add feature CreateOrder
```

**3. Or generate individual components:**

```bash
ignite add entity Order
ignite add handler CreateOrder
ignite add repository Order
ignite add endpoint CreateOrder
```

---

## Commands Reference

### Group 1 — Environment Setup

| Command | Description |
|---|---|
| `ignite init` | Interactive setup. Configures architecture, paths, and creates `.ignite.json` |
| `ignite config` | Update individual settings after initialization |

### Group 2 — Code Generation

| Command | Description |
|---|---|
| `ignite add entity <Name>` | Generates entity class + EF Core configuration |
| `ignite add command <Name>` | Generates MediatR command |
| `ignite add query <Name>` | Generates MediatR query |
| `ignite add handler <Name>` | Generates request handler |
| `ignite add repository <Name>` | Generates repository interface + implementation |
| `ignite add endpoint <Name>` | Generates controller or Minimal API endpoint |
| `ignite add feature <Name>` | Scaffolds the full feature flow at once |

> Command behavior adapts automatically based on the architecture selected during `init`.

### Group 3 — Template Management

| Command | Description |
|---|---|
| `ignite template list` | Lists all available built-in templates |
| `ignite template export <Name>` | Exports a built-in template to `.templates/` for customization |
| `ignite template import <Path>` | Registers a custom template with the tool |

### Group 4 — Info & Audit

| Command | Description |
|---|---|
| `ignite list` | Scans the project and lists all generated entities and features |
| `ignite info` | Displays the current tool configuration |

---

## Configuration

Running `ignite init` creates an `.ignite.json` file in your project root. Example:

```json
{
  "architecture": "CleanArchitecture",
  "layers": {
    "domain": "src/Project.Domain",
    "application": "src/Project.Application",
    "infrastructure": "src/Project.Infrastructure",
    "api": "src/Project.Api"
  }
}
```

This file should be committed to source control so all team members share the same configuration.

---

## Smart Context Detection

`dotnet-ignite` parses your `.csproj` files to detect installed NuGet packages and adjusts the generated code accordingly:

| Package detected | Effect on generated code |
|---|---|
| `MediatR` | Adds `IRequest` / `IRequestHandler` to commands and handlers |
| `FluentValidation` | Generates a `Validator` class alongside the command |
| `Entity Framework Core` | Adds `DbContext` injection and configuration scaffolding |

---

## Custom Templates

Not happy with the default output? Export any built-in template, modify it, and register it back:

```bash
# Export the handler template to .templates/handler.sbn
ignite template export handler

# Edit .templates/handler.sbn to match your conventions

# Register it
ignite template import .templates/handler.sbn
```

Templates use [Scriban](https://github.com/scriban/scriban) syntax:

```
namespace {{ namespace }};

public class {{ name }}Handler : IRequestHandler<{{ name }}Command, {{ name }}Result>
{
    public async Task<{{ name }}Result> Handle({{ name }}Command request, CancellationToken cancellationToken)
    {
        // TODO: implement
        return new {{ name }}Result();
    }
}
```

---

## Roadmap

### v0.1 — MVP
- [x] `ignite init` and `ignite config`
- [x] `add entity`, `add handler`, `add feature`
- [x] Clean Architecture support
- [x] Built-in Scriban templates (embedded resources)

### v0.2 — Flexibility
- [ ] Vertical Slice Architecture support
- [ ] `template export`, `template import`, `template list`
- [ ] Custom template override logic

### v0.3 — Intelligence & Audit
- [ ] `.csproj` parsing for smart code generation
- [ ] `ignite list` and `ignite info`
- [ ] Conditional generation based on detected packages

---

## Contributing

Contributions are welcome! If you find a bug or have a feature request, please open an issue first to discuss what you'd like to change.

1. Fork the repository
2. Create your feature branch: `git checkout -b feature/add-vertical-slice`
3. Commit your changes: `git commit -m 'feat: add vertical slice support'`
4. Push to the branch: `git push origin feature/add-vertical-slice`
5. Open a Pull Request against `develop`

Please make sure your code follows the existing style and that all CI checks pass before requesting a review.

---

## License

[MIT](LICENSE)
