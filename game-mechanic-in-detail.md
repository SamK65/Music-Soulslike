# My Ideas for the Game In Detail

## Introduction: The Balancing Act

The core mechanic of the game is the ear-training combat system. It's important to realize that designing this game is a balancing act of 3 interrelated components.

1. **Pedagogy:** Ear training is not the easiest or most intuitive skill to learn, so every boss must also function as a teacher. We need to build robust systems of visual and auditory feedback while also making sure any feedback does not give the player a way to cheat the system.
2. **Engaging Combat:** The ear training is the basis of combat and progression in the game. It should be fun. It should be intense, even in a tutorial (which is all we're going to be able to make). This is not an ear-training quiz pretending to be a game. It is a battle between rivals.
3. **Soundtrack:** The musical combat can be the basis of the soundtrack in a boss battle, coupled with an accompaniment. Progressing in a boss battle should sound amazing. It is part of the reward for learning this game.

When combined these components have the potential to create a deeply immersive experience. The ear-training requires attentive listening from the player, so that they can accurately respond. Coupled with the combat and responsive soundtrack, it would put players in a flow state.

Note that these components present an optimization problem. For example, faster melodies can make the soundtrack more engaging, but they would require more skill from the player. Conversely, allowing the player to play notes asynchronously would ease the learning curve, but it would create unwanted rhythmic dissonance in the soundtrack. 

Below I will detail a game system that deals with these intricacies. I will start with [I. pedagogy](#i-pedagogy), then [II. combat](#ii-combat), and finally [III. soundtrack](#iii-soundtrack). The target audience for the game will be musicians and non-musicians which I assume have little ear-training skill. The game length will be 3-5 minutes. I will denote ideas that are suited to a longer version of the game with the marker "Expansion:".

Here's a sketch of my overall idea for the game.
<img width="1280" height="800" alt="Game_Sketch" src="https://github.com/user-attachments/assets/075db0b2-478c-4e9f-9c09-932dc84be99a" />

## I. Pedagogy

### My Approach to Ear Training.

### Incentives


### Feedback Systems


### Pedagogical Scaffolding
-Guide Notes

## II. Combat

### Combat Interface

### Damage System

### Anti-Spamming Mechanic


## III. Soundtrack

### Adding Musical Interest Without Extra Difficulty

### Distinguishing Game Sounds from Accompaniment

### The sound of musical weapons

### Expansion: Motivic storytelling

Combat

The physical interface would either be a keyboard or a midi controller. Either way, we need at least 12 buttons for the chromatic notes within the octave. In combat encounters, the player would hear a melody in time with the soundtrack before their opponent attacks. Playing this melody by ear by pressing the corresponding buttons in the controller would 1.) counter the opponents attack and deal damage; and 2.) sound as notes in an instrument determined by the player's weapon. The rhythm of the melody will also reflect in the attack sequence. Mismatched or missed notes will have instant feedback in both incongruent sound and damage taken. The player who plays the song by ear perfectly will get the satisfaction of a smooth combat encounter and fully-realized soundtrack.

Pedagogy

The use of at least 12 buttons for combat, instead of the single deflection button, means learning this game could be frustratingly difficult. Some ways to mitigate this:

Structure the relative pitch like a metroidvania. The player would only have access to the tonic at first. Then the leading tone. These notes have a strong pull, so they are ideal for starting. Then the major third and perfect fifth. Once we cover the major scale, we could move to minor, then modes. Then, notes taken from modes and other scales. The harmonic complexity would also be closely controlled.
    A combat sequence would teach the player early on that the buttons are relative pitch buttons. (ie. 1 is the tonic always, and this note would be made very clear at the start of an encounter). It might also teach octave equivalence.
    Use the visual aesthetic and in game narrative to communicate the emotional feeling of the training note. Ex: When introducing harmonic minor, use a dramatic battle with a gothic setting.

