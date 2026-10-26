# GPR-UN-APU-05 — Observer, prévenir sans connaître

Companion de la séance *Observer, prévenir sans connaître* (SAE Institute Genève, GP-926).

Un petit platformer : on ramasse cinq gemmes, et quand il n'en reste plus un panneau
s'affiche.

> [!warning] Vous êtes sur la branche `01-depart`
> C'est la version **tout couplée**, le point de départ de l'atelier : `Item` appelle
> directement `ItemManager`, qui écrit lui-même dans l'étiquette et allume le panneau.
> Il n'y a ni canal, ni `UnityEvent`, ni son.
>
> L'énoncé est dans le vault ; le corrigé est la branche `main`.

## Les fichiers qui comptent

| Fichier | Rôle |
|---|---|
| `03 - Scripts/Collectibles/Item.cs` | tient une référence sur `ItemManager` et l'appelle |
| `03 - Scripts/Collectibles/ItemManager.cs` | compte, écrit dans l'étiquette, allume le panneau |
| `03 - Scripts/Player/PlayerController.cs` | courir et sauter, le strict minimum |

## Assets

Deux packs de [Kenney](https://kenney.nl), **CC0**, dans `Assets/00 - Imports_Packs/` :

- [Pixel Platformer](https://kenney.nl/assets/pixel-platformer) — tuiles 18 px, personnages 24 px
- [RPG Audio](https://kenney.nl/assets/rpg-audio) — le son de ramassage (`handleCoins.ogg`)

Les textures sont importées en `Point`, sans compression, à **18 pixels par unité** : une
tuile fait exactement une unité de monde.
