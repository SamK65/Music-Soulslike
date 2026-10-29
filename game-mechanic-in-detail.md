# My Ideas for the Game In Detail
## Contents

[Introduction: The Balancing Act](#introduction-the-balancing-act)  
[I. Pedagogy](#i-pedagogy)  
[II. Combat](#ii-combat)  
[III. Soundtrack](#iii-soundtrack)  

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

In this section, I answer the question: "How do we teach the player ear-training within the structure of a boss battle?"

### Teaching Scope
Clearly, ear-training is the focus of the game. However, in addition to ear-training, some basic rhythmic skill will be required because playing melodies involves both pitch and rhythm.

In particular, I want to emphasize that the game will NOT be teaching music theory. Although music theory helps put names to the emotional language of music, it is not essential to ear-training, and only the essentials will be taught. The exception to this would be music theory concepts used as Easter eggs for musicians, but non-musicians can miss them without consequence.

### My Approach to Relative-Pitch Ear-Training.
I have over 12 years of experience in piano. I use relative-pitch ear-training regularly as a jazz pianist. I can play most songs by ear in a few listens. This is the approach I use and intend to build the game around. I will clarify what this technique is, what it is not, and why I believe it is the best technique.

**Definition:** The **tonic** or key is the tonal center of the music. It is the note that feels most stable.  
**Definition:** Once the tonic is established, it is felt even if it is not currently being played. I will call this the **key context** or context.

First, this is a _relative_ pitch technique, which means I listen for notes relative to other notes of a song
This is in contrast to _absolute_ or perfect pitch, which involves hearing exact notes and frequencies, and is much harder to impossible to learn.

Here's how it works. Music makes us _feel_. That raw emotional connection is one of the main reasons music is so special. That connection is proof that we all feel the emotional push and pull of the harmony about the tonic, (even if we don't know what a tonic is). This style of ear training is all about sharpening that emotional compass. 

Each note heard in the context of the tonic creates a distinctly recognizable _sensation_. By sensation, I mean it is more elementary than an emotion, more like a sense of stability or tension. For example, the 2nd scale degree feels somewhat content, but also a little bit stirred. We can feel its tendency to resolve to the tonic. We can especially feel the pull of the 7, or leading tone to resolve up to 1. These sensations are octave equivalent, meaning changing the octave doesn't change the sensation, though it might change the overall emotion. They are also independent of the 12 keys. Changing the key, changes the brightness\timbre of the key context, but not these sensations.

Learning this starts with learning these 12 sensations. Then, we can start grouping them together into melodies and chords. The combination of multiple sensations in a chord creates emotions. For example, a major 7 chord feels stable due to the 1 & 5, warm due to the 3, but with a melancholic pull due to the 7. This nuanced emotion can be learned and recognized as Imaj7. I don't expect to teach grouping, but it shows how this system works.



subtle but critical distinction.
Second, this technique is NOT the standard interval approach that is taught in a basic music theory class (like Fundies). The interval approach is when students memorize the sound of each interval between two notes out of the 12. Typically, the range is from unison to the octave, and the intervals are heard ascending, descending, and played together.



**the interval is felt not played**
For these reasons, I will not be using the term "interval" in reference to ear-training.

- Octave equivalence
- Chunking

### Incentives


### Feedback Systems

- Expansion: Aesthetics of tonal emotion.

- How do we help the player not get lost when they miss a note in the middle of a phrase?

### Pedagogical Scaffolding
-Guide Notes

The use of at least 12 buttons for combat, instead of the single deflection button, means learning this game could be frustratingly difficult. Some ways to mitigate this:

Structure the relative pitch like a metroidvania. The player would only have access to the tonic at first. Then the leading tone. These notes have a strong pull, so they are ideal for starting. Then the major third and perfect fifth. Once we cover the major scale, we could move to minor, then modes. Then, notes taken from modes and other scales. The harmonic complexity would also be closely controlled.
    A combat sequence would teach the player early on that the buttons are relative pitch buttons. (ie. 1 is the tonic always, and this note would be made very clear at the start of an encounter). It might also teach octave equivalence.
    Use the visual aesthetic and in game narrative to communicate the emotional feeling of the training note. Ex: When introducing harmonic minor, use a dramatic battle with a gothic setting.



## II. Combat

### Combat Interface

### The Call and Response Phases of the Game Loop

### Damage System

### Punishing Cheap Tactics
- Anti-Spamming Mechanic


## III. Soundtrack

### Adding Musical Interest Without Extra Difficulty

### Distinguishing Game Sounds from Accompaniment

### The sound of musical weapons

### Expansion: Motivic storytelling

Combat

The physical interface would either be a keyboard or a midi controller. Either way, we need at least 12 buttons for the chromatic notes within the octave. In combat encounters, the player would hear a melody in time with the soundtrack before their opponent attacks. Playing this melody by ear by pressing the corresponding buttons in the controller would 1.) counter the opponents attack and deal damage; and 2.) sound as notes in an instrument determined by the player's weapon. The rhythm of the melody will also reflect in the attack sequence. Mismatched or missed notes will have instant feedback in both incongruent sound and damage taken. The player who plays the song by ear perfectly will get the satisfaction of a smooth combat encounter and fully-realized soundtrack.
