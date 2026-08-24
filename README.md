# Living Calradia

Living Calradia is an AI-driven political and social simulation mod for **Mount & Blade II: Bannerlord**.

The project aims to make Calradia feel like a living world where characters can reason about their circumstances, relationships, personality, political situation, and experiences using locally running AI.

## Development Status

**Early prototype — Prototype 0.03 complete.**

### Completed Milestones

**0.01 — Bannerlord Module Loading**

* Created the Living Calradia Bannerlord module.
* Established the C# project and Bannerlord assembly references.
* Successfully loaded and executed the mod in-game.

**0.02 — Character Context**

* Reads live Bannerlord `Hero` data.
* Created a structured `CharacterContext`.
* Extracts identity, culture, clan, kingdom, personality traits, relationships, family, settlements, fiefs, wars, party information, and political context.

**0.03 — Local LLM Connection**

* Integrated Living Calradia with a locally running `llama.cpp` server.
* Established local HTTP communication between Bannerlord and the AI runtime.
* Connected **Qwen3-8B GGUF Q4_K_M** as the current baseline model.
* Sends live Bannerlord character context to the model.
* Receives character-aware AI responses.
* Receives structured JSON output.
* Parses AI output into a strongly typed C# `LlmResponse`.

### Current AI Pipeline

```text
Bannerlord
    ↓
CharacterContextReader
    ↓
CharacterContext
    ↓
CharacterPromptBuilder
    ↓
LocalLlmClient
    ↓
llama.cpp
    ↓
Qwen3-8B
    ↓
Structured JSON
    ↓
LlmResponse
```

Qwen3-8B is the **current development baseline**, not a permanent dependency. Living Calradia is being designed so the AI runtime and model can be replaced or upgraded without rewriting the core simulation.

## Next Milestone

**Prototype 0.04 — AI Decision Prototype**

The next stage will begin moving from simple character-aware responses toward AI-generated decisions that can be validated and safely interpreted by Living Calradia.

## Project Status

Living Calradia is currently experimental development software. Features, architecture, AI models, prompts, and installation requirements are expected to change significantly during development.




## Windows AI Setup

Living Calradia currently uses a local AI model. No paid API key or subscription is required.

### 1. Install llama.cpp

Open **PowerShell** and run:

```powershell
winget install llama.cpp
```

After installation, open a new PowerShell window and verify it:

```powershell
llama-server --version
```

### 2. Download and Run the AI Model

Living Calradia currently uses:

**Qwen3-8B GGUF — Q4_K_M**

Run:

```powershell
llama serve -hf Qwen/Qwen3-8B-GGUF:Q4_K_M
```

The first launch will download the model automatically.

Once loaded, llama.cpp should report that it is listening on:

```text
http://127.0.0.1:8080
```

Keep this PowerShell window open while playing Living Calradia.

### 3. Launch Bannerlord

Once the local AI server is running:

1. Launch Mount & Blade II: Bannerlord.
2. Enable **Living Calradia** in the launcher.
3. Start or load a campaign.

Living Calradia communicates with the locally running model through `127.0.0.1:8080`.

### Current AI Baseline

* Runtime: **llama.cpp**
* Model: **Qwen3-8B**
* Format: **GGUF**
* Quantization: **Q4_K_M**
* API: **Local HTTP**
* Paid API required: **No**
* Internet required during normal inference: **No**, once the model has been downloaded

Qwen3-8B Q4_K_M is the current baseline model and is **not intended to be a permanent dependency**. Living Calradia is being designed so compatible local models can be swapped in later.
