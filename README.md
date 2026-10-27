# GPR-UN-APU-05 — Observer, prévenir sans connaître

Companion for the session *Observer, prévenir sans connaître* (SAE Institute Geneva, GP-926).

A small pirate platformer: you collect five gems, and when none are left a panel shows up.
The whole point is in **how the news travels** — three different mechanisms, one per floor,
each picked for what it is good at.

| | Unity 6000.3.25f1 · URP 2D · Input System · UI Toolkit |
|---|---|
| Scene | `Assets/01 - Scenes/Collect.unity` |
| Controls | arrows or A/D, **Space** to jump |

![The game at the start](Documentation~/depart.png)

## The chain, end to end

```
Item ──event Action──▶ ItemManager ──VoidEventChannelSO──▶ SoundManager
                        │  │        └─IntEventChannelSO──▶ CounterLabel
                        │  ├─ UnityEvent allCollected ───▶ EndPanel.SetActive(true)
                        │  └─ UnityEvent reset ──────────▶ EndPanel.SetActive(false)
                        ▲
      ResetButton ──────┘  direct call to ResetItems()
```

1. **`event Action`** — `Item` announces that it has been picked up. It knows nobody; it is
   `ItemManager` that subscribes, in `OnEnable`, with a named method.
2. **ScriptableObject channel** — `ItemManager` publishes on two assets, `GemCollected` and
   `GemsRemaining`. The sound and the counter listen to them without anyone having wired one
   to the other: that is what makes it possible to add a system afterwards.
3. **`UnityEvent`** — the last gem fires `allCollected`, wired **in the Inspector** to
   `EndPanel.SetActive(true)`. Not a single script was written for the end panel.

The channel assets live in `Assets/09 - Events/`.

## Making a channel talk

A channel you cannot see is a channel you cannot debug: nothing in the code says who is
listening. Both channels carry a **Verbose** checkbox for that. Tick it on `GemsRemaining`,
press play, pick a gem:

```
GemsRemaining = 5 -> 1 : HUD.Show
GemCollected  -> 1 : SoundManager.PlayPickup
GemsRemaining = 3 -> nobody        (HUD switched off mid-game)
```

Every line is one announcement: the asset, the value, and every subscriber by object and
method. The list comes from `Raised.GetInvocationList()`, so a subscriber that was destroyed
without unsubscribing shows up as `<destroyed>` instead of waiting to throw later — the ghost
subscriber becomes something you can *see*. Clicking a line pings the channel asset in the
Project window.

Off by default: a verbose channel in a loop floods the console.

## The *Restart* button, against the grain

It is there on purpose: it is the one place in the game where **we call instead of
announcing**.

`ResetButton` holds a reference to `ItemManager` and calls `ResetItems()`. That is
legitimate — the game's UI is allowed to drive the game, that is what it is for. The counter,
sitting on the **same object**, has no reason at all to know `ItemManager`: it listens to a
channel.

Both cases on a single screen, three lines apart. The question to ask in class is not "which
one is right" but "why not the same one everywhere".

On the `ItemManager` side, `ResetItems()` does three things in order: give the gems back,
close the panel through the `reset` `UnityEvent`, then republish the count on the channel. The
counter updates without anyone having spoken to it.

## The files that matter

| File | Role |
|---|---|
| `03 - Scripts/Collectibles/Item.cs` | the subject: one `event Action<Item>`, nothing else |
| `03 - Scripts/Collectibles/ItemManager.cs` | observer, then subject in three ways |
| `03 - Scripts/Events/EventChannelSO.cs` | the generic channel |
| `03 - Scripts/Events/IntEventChannelSO.cs` · `VoidEventChannelSO.cs` | one type per shape of announcement |
| `03 - Scripts/Events/ChannelLog.cs` | what a verbose channel prints |
| `03 - Scripts/Audio/SoundManager.cs` | added without opening `Item` or `ItemManager` |
| `03 - Scripts/UI/CounterLabel.cs` | the UI waits to be told the count |
| `03 - Scripts/UI/ResetButton.cs` | the counter-example: it calls, and that is intended |
| `03 - Scripts/Player/PlayerController.cs` | run and jump, the strict minimum |
| `06 - UI/Hud.uxml` · `End.uxml` · `Game.uss` | the interface, in UI Toolkit |

## Three deliberate details

- **The first announcement goes out in `Start`, not in `OnEnable`.** By then everyone has
  subscribed; publishing earlier would make half the observers miss the starting count.
- **`allCollected` points at no script.** `UnityEvent` calls `GameObject.SetActive` directly —
  that is the demonstration of the row "who wires it: the Inspector".
- **The end panel is a second `UIDocument`**, not an element of the first one. That is what
  makes it possible to switch it on and off with `SetActive`, so without writing a line for it.

## Assets

In `Assets/00 - Imports_Packs/`:

- [Treasure Hunters](https://pixelfrog-assets.itch.io/treasure-hunters) —
  [Pixel Frog](https://pixelfrog-assets.itch.io). **What the scene shows**: the ground, the
  captain, the gems, the background and the interface frames.
- [RPG Audio](https://kenney.nl/assets/rpg-audio) — [Kenney](https://kenney.nl), **CC0**.
  The pickup sound (`handleCoins.ogg`).
- [Pixel Platformer](https://kenney.nl/assets/pixel-platformer) — Kenney, **CC0**. The first
  build of the scene; unused since the move to Treasure Hunters.

The Treasure Hunters textures are imported as `Point`, uncompressed, at **32 pixels per
unit**: one ground tile is exactly one world unit.

Four sprites in `Assets/04 - Sprites/` and `Assets/06 - UI/` are **reassemblies** of the pack:
the corner, edge and centre tiles put back together into a single 3×3 image, so they can be
stretched as 9-slice at any size.
