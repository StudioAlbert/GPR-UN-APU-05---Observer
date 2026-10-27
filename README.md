# GPR-UN-APU-05 — Observer, prévenir sans connaître

Companion de la séance *Observer, prévenir sans connaître* (SAE Institute Genève, GP-926).

Un petit platformer pirate : on ramasse cinq gemmes, et quand il n'en reste plus un panneau
s'affiche. Tout l'intérêt est dans **la façon dont la nouvelle circule** — trois mécanismes
différents, un par étage, choisis chacun pour ce qu'il sait faire.

| | Unity 6000.3.25f1 · URP 2D · Input System · UI Toolkit |
|---|---|
| Scène | `Assets/01 - Scenes/Collecte.unity` |
| Commandes | flèches ou A/D, **Espace** pour sauter |

![Le jeu au départ](Documentation~/depart.png)

## La chaîne, de bout en bout

```
Item ──event Action──▶ ItemManager ──VoidEventChannelSO──▶ SoundManager
                        │  │        └─IntEventChannelSO──▶ CounterLabel
                        │  ├─ UnityEvent allCollected ───▶ EndPanel.SetActive(true)
                        │  └─ UnityEvent reset ──────────▶ EndPanel.SetActive(false)
                        ▲
      ResetButton ──────┘  appel direct de ResetItems()
```

1. **`event Action`** — `Item` annonce qu'on l'a ramassé. Il ne connaît personne ; c'est
   `ItemManager` qui s'abonne, dans `OnEnable`, par une méthode nommée.
2. **Canal en ScriptableObject** — `ItemManager` publie sur deux assets, `GemmeRamassee` et
   `GemmesRestantes`. Le son et le compteur les écoutent sans que personne ne les ait
   branchés l'un à l'autre : c'est ce qui permet d'ajouter un système après coup.
3. **`UnityEvent`** — la dernière gemme déclenche `allCollected`, câblé **dans l'Inspector**
   sur `EndPanel.SetActive(true)`. Aucun script n'a été écrit pour le panneau de fin.

Les assets de canal sont dans `Assets/09 - Events/`.

## Le bouton *Recommencer*, à contre-courant

Il est là exprès : c'est le seul endroit du jeu où **on appelle au lieu d'annoncer**.

`ResetButton` tient une référence à `ItemManager` et appelle `ResetItems()`. C'est légitime —
l'UI du jeu a le droit de piloter le jeu, elle est faite pour ça. Le compteur, posé sur le
**même objet**, n'a lui aucune raison de connaître `ItemManager` : il écoute un canal.

Les deux cas sur un seul écran, à trois lignes l'un de l'autre. La question à poser en classe
n'est pas « lequel est bien » mais « pourquoi pas le même partout ».

Côté `ItemManager`, `ResetItems()` fait les trois choses dans l'ordre : rendre les gemmes,
refermer le panneau par le `UnityEvent` `reset`, puis republier le compte sur le canal. Le
compteur se remet à jour sans qu'on lui ait parlé.

## Les fichiers qui comptent

| Fichier | Rôle |
|---|---|
| `03 - Scripts/Collectibles/Item.cs` | le sujet : un `event Action<Item>`, rien d'autre |
| `03 - Scripts/Collectibles/ItemManager.cs` | observateur, puis sujet de trois façons |
| `03 - Scripts/Events/EventChannelSO.cs` | le canal générique |
| `03 - Scripts/Events/IntEventChannelSO.cs` · `VoidEventChannelSO.cs` | un type par forme d'annonce |
| `03 - Scripts/Audio/SoundManager.cs` | ajouté sans ouvrir `Item` ni `ItemManager` |
| `03 - Scripts/UI/CounterLabel.cs` | l'UI attend qu'on lui annonce le compte |
| `03 - Scripts/UI/ResetButton.cs` | le contre-exemple : il appelle, et c'est voulu |
| `03 - Scripts/Player/PlayerController.cs` | courir et sauter, le strict minimum |
| `06 - UI/Hud.uxml` · `Fin.uxml` · `Jeu.uss` | l'interface, en UI Toolkit |

## Trois détails volontaires

- **La première annonce part dans `Start`, pas dans `OnEnable`.** À ce moment-là tout le monde
  s'est abonné ; publier plus tôt ferait rater le compte de départ à la moitié des
  observateurs.
- **`allCollected` ne pointe sur aucun script.** `UnityEvent` appelle directement
  `GameObject.SetActive` — c'est la démonstration de la ligne « qui câble : l'Inspector ».
- **Le panneau de fin est un second `UIDocument`**, pas un élément du premier. C'est ce qui
  permet de l'allumer et de l'éteindre par `SetActive`, donc sans écrire une ligne pour lui.

## Assets

Dans `Assets/00 - Imports_Packs/` :

- [Treasure Hunters](https://pixelfrog-assets.itch.io/treasure-hunters) —
  [Pixel Frog](https://pixelfrog-assets.itch.io). **Ce que la scène montre** : le terrain, le
  capitaine, les gemmes, le décor et les cadres de l'interface.
- [RPG Audio](https://kenney.nl/assets/rpg-audio) — [Kenney](https://kenney.nl), **CC0**.
  Le son de ramassage (`handleCoins.ogg`).
- [Pixel Platformer](https://kenney.nl/assets/pixel-platformer) — Kenney, **CC0**. Le premier
  montage de la scène ; plus utilisé depuis le passage à Treasure Hunters.

Les textures Treasure Hunters sont importées en `Point`, sans compression, à **32 pixels par
unité** : une tuile de terrain fait exactement une unité de monde.

Quatre sprites de `Assets/04 - Sprites/` et `Assets/06 - UI/` sont des **réassemblages** du
pack : les tuiles de coin, de bord et de centre remises en une seule image 3×3, pour pouvoir
les étirer en 9-slice à n'importe quelle taille.
