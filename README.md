# GPR-UN-APU-05 — Observer, prévenir sans connaître

Companion de la séance *Observer, prévenir sans connaître* (SAE Institute Genève, GP-926).

Un petit platformer : on ramasse cinq gemmes, et quand il n'en reste plus un panneau
s'affiche. Tout l'intérêt est dans **la façon dont la nouvelle circule** — trois mécanismes
différents, un par étage, choisis chacun pour ce qu'il sait faire.

| | Unity 6000.3.25f1 · URP 2D · Input System |
|---|---|
| Scène | `Assets/01 - Scenes/Collecte.unity` |
| Commandes | flèches ou A/D, **Espace** pour sauter |

![Le jeu au départ](Documentation~/depart.png)

## La chaîne, de bout en bout

```
Item ──event Action──▶ ItemManager ──VoidEventChannelSO──▶ SoundManager
                            │        └─IntEventChannelSO──▶ CounterLabel
                            └────────UnityEvent───────────▶ PanneauFin.SetActive
```

1. **`event Action`** — `Item` annonce qu'on l'a ramassé. Il ne connaît personne ; c'est
   `ItemManager` qui s'abonne, dans `OnEnable`, par une méthode nommée.
2. **Canal en ScriptableObject** — `ItemManager` publie sur deux assets, `GemmeRamassee` et
   `GemmesRestantes`. Le son et le compteur les écoutent sans que personne ne les ait
   branchés l'un à l'autre : c'est ce qui permet d'ajouter un système après coup.
3. **`UnityEvent`** — la dernière gemme déclenche `allCollected`, câblé **dans l'Inspector**
   sur `PanneauFin.SetActive(true)`. Aucun script n'a été écrit pour le panneau de fin.

Les assets de canal sont dans `Assets/09 - Events/`.

## Les fichiers qui comptent

| Fichier | Rôle |
|---|---|
| `03 - Scripts/Collectibles/Item.cs` | le sujet : un `event Action<Item>`, rien d'autre |
| `03 - Scripts/Collectibles/ItemManager.cs` | observateur, puis sujet de trois façons |
| `03 - Scripts/Events/EventChannelSO.cs` | le canal générique |
| `03 - Scripts/Events/IntEventChannelSO.cs` · `VoidEventChannelSO.cs` | un type par forme d'annonce |
| `03 - Scripts/Audio/SoundManager.cs` | ajouté sans ouvrir `Item` ni `ItemManager` |
| `03 - Scripts/UI/CounterLabel.cs` | l'UI attend qu'on lui annonce le compte |
| `03 - Scripts/Player/PlayerController.cs` | courir et sauter, le strict minimum |

## Deux détails volontaires

- **La première annonce part dans `Start`, pas dans `OnEnable`.** À ce moment-là tout le monde
  s'est abonné ; publier plus tôt ferait rater le compte de départ à la moitié des
  observateurs.
- **`allCollected` ne pointe sur aucun script.** `UnityEvent` appelle directement
  `GameObject.SetActive` — c'est la démonstration de la ligne « qui câble : l'Inspector ».

## Assets

Dans `Assets/00 - Imports_Packs/` :

- [Pixel Platformer](https://kenney.nl/assets/pixel-platformer) — [Kenney](https://kenney.nl), **CC0**.
  Tuiles 18 px, personnages 24 px : ce que la scène utilise aujourd'hui.
- [RPG Audio](https://kenney.nl/assets/rpg-audio) — Kenney, **CC0**. Le son de ramassage
  (`handleCoins.ogg`).
- [Treasure Hunters](https://pixelfrog-assets.itch.io/treasure-hunters) —
  [Pixel Frog](https://pixelfrog-assets.itch.io). Pirates 32 px, décor d'île, UI
  « Wood and Paper ». **Importé, pas encore branché** sur la scène.

Les textures Kenney sont importées en `Point`, sans compression, à **18 pixels par unité** :
une tuile fait exactement une unité de monde.
