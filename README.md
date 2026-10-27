# GPR-UN-APU-05 — Observer, prévenir sans connaître

Companion de la séance *Observer, prévenir sans connaître* (SAE Institute Genève, GP-926).

Un petit platformer pirate : on ramasse cinq gemmes, et quand il n'en reste plus un panneau
s'affiche.

> [!warning] Vous êtes sur la branche `01-depart`
> C'est la version **tout couplée**, le point de départ de l'atelier : `Item` appelle
> directement `ItemManager`, qui écrit lui-même dans l'étiquette et allume le panneau.
> Il n'y a ni canal, ni `UnityEvent`, ni son.
>
> L'énoncé est dans le vault ; le corrigé est la branche `main`.

| | Unity 6000.3.25f1 · URP 2D · Input System · UI Toolkit |
|---|---|
| Scène | `Assets/01 - Scenes/Collecte.unity` |
| Commandes | flèches ou A/D, **Espace** pour sauter |

![Le jeu au départ](Documentation~/depart.png)

## Les fichiers qui comptent

| Fichier | Rôle |
|---|---|
| `03 - Scripts/Collectibles/Item.cs` | tient une référence sur `ItemManager` et l'appelle |
| `03 - Scripts/Collectibles/ItemManager.cs` | compte, écrit dans l'étiquette, allume le panneau |
| `03 - Scripts/UI/ResetButton.cs` | le bouton *Recommencer* ; celui-là ne bougera pas |
| `03 - Scripts/Player/PlayerController.cs` | courir et sauter, le strict minimum |
| `06 - UI/Hud.uxml` · `Fin.uxml` · `Jeu.uss` | l'interface, en UI Toolkit |

**`ResetButton` est volontairement laissé tel quel.** Il appelle `ItemManager.ResetItems()`,
directement, et c'est le seul couplage de ce projet qu'on gardera jusqu'au bout : une UI de
jeu a le droit de piloter le jeu. De quoi comparer, à chaque étape, avec ce qu'on est en
train de démonter juste à côté.

## Assets

Dans `Assets/00 - Imports_Packs/` :

- [Treasure Hunters](https://pixelfrog-assets.itch.io/treasure-hunters) —
  [Pixel Frog](https://pixelfrog-assets.itch.io). Le terrain, le capitaine, les gemmes, le
  décor et les cadres de l'interface.
- [RPG Audio](https://kenney.nl/assets/rpg-audio) — [Kenney](https://kenney.nl), **CC0**.
  `handleCoins.ogg` attend l'étape 3 ; sur cette branche, rien ne le joue.
- [Pixel Platformer](https://kenney.nl/assets/pixel-platformer) — Kenney, **CC0**. Le premier
  montage de la scène ; plus utilisé depuis le passage à Treasure Hunters.

Les textures Treasure Hunters sont importées en `Point`, sans compression, à **32 pixels par
unité** : une tuile de terrain fait exactement une unité de monde.
