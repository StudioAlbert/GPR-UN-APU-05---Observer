# GPR-UN-APU-05 — Observer, prévenir sans connaître

Companion for the session *Observer, prévenir sans connaître* (SAE Institute Geneva, GP-926).

A small pirate platformer: you collect five gems, and when none are left a panel shows up.

> [!warning] You are on branch `01-depart`
> This is the **fully coupled** version, the starting point of the workshop: `Item` calls
> `ItemManager` directly, and `ItemManager` writes the label and switches the panel on
> itself. There is no channel, no `UnityEvent`, and no sound.
>
> The brief lives in the vault; the reference solution is branch `main`.

| | Unity 6000.3.25f1 · URP 2D · Input System · UI Toolkit |
|---|---|
| Scene | `Assets/01 - Scenes/Collect.unity` |
| Controls | arrows or A/D, **Space** to jump |

![The game at the start](Documentation~/depart.png)

## The files that matter

| File | Role |
|---|---|
| `03 - Scripts/Collectibles/Item.cs` | holds a reference to `ItemManager` and calls it |
| `03 - Scripts/Collectibles/ItemManager.cs` | counts, writes the label, switches the panel on |
| `03 - Scripts/UI/ResetButton.cs` | the *Restart* button; that one will not move |
| `03 - Scripts/Player/PlayerController.cs` | run and jump, the strict minimum |
| `06 - UI/Hud.uxml` · `End.uxml` · `Game.uss` | the interface, in UI Toolkit |

**`ResetButton` is deliberately left as is.** It calls `ItemManager.ResetItems()`, directly,
and it is the one coupling in this project we will keep to the end: a game's UI is allowed to
drive the game. Something to compare, at every step, with what is being taken apart right
next to it.

## Assets

In `Assets/00 - Imports_Packs/`:

- [Treasure Hunters](https://pixelfrog-assets.itch.io/treasure-hunters) —
  [Pixel Frog](https://pixelfrog-assets.itch.io). The ground, the captain, the gems, the
  background and the interface frames.
- [RPG Audio](https://kenney.nl/assets/rpg-audio) — [Kenney](https://kenney.nl), **CC0**.
  `handleCoins.ogg` is waiting for step 3; on this branch nothing plays it.
- [Pixel Platformer](https://kenney.nl/assets/pixel-platformer) — Kenney, **CC0**. The first
  build of the scene; unused since the move to Treasure Hunters.

The Treasure Hunters textures are imported as `Point`, uncompressed, at **32 pixels per
unit**: one ground tile is exactly one world unit.
