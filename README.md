# Object Match

Object Match is a simple mobile puzzle game inspired by Match Factory.  
The player taps objects on the table and tries to create matches while managing limited inventory slots.

---

# Gameplay

The goal of the game is to collect and match objects.

- When the player taps an object, it moves into one of the 7 slots at the bottom of the screen.
- If 3 identical objects are collected, they disappear from the slots.
- The player must collect:
  - 9 Hamburgers
  - 9 Cakes
to win the game.

The game ends in two different ways:

## Win Condition
- Match 9 Hamburgers and 9 Cakes.

## Lose Condition
- Fill all 7 slots without creating enough matches.
- Or run out of time.

---

# Implemented Mechanics

- Main Menu system
- Scene transition system
- Mobile portrait layout
- 7-slot inventory system
- Triple match mechanic
- Win/Lose system
- Restart system
- Pause system
- Timer system
- Physics-based object spawning
- UI system with TextMeshPro
- Mobile-friendly UI scaling

---

# DOTween Animations

DOTween was used to improve game feel and polish.

Implemented animations:

- Punch Scale animation when clicking collectibles
- Animated Win Panel using DOTween Sequence
- Animated Lose Panel using DOTween Sequence
- UI text punch animation when progress updates

DOTween features used:

- Sequences
- Ease types
- OnComplete callbacks
- Punch Scale effects

---

# AI Usage

AI was used during development to assist with:

- C# scripting
- Game logic implementation
- Unity UI setup
- DOTween animation integration
- Slot system logic
- Match detection system
- Win/Lose conditions
- Mobile UI adaptation
- Debugging and fixing errors

AI was used as a development assistant throughout the project.
