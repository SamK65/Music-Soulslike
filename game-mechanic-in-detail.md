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

Each note heard in the context of the tonic creates a distinctly recognizable _sensation_. By sensation, I mean it is more elementary than an emotion, more like a sense of stability or tension. For example, the 2nd scale degree feels somewhat content, but also a little bit stirred. We can feel its tendency to resolve to the tonic. We can especially feel the pull of the 7, or leading tone to resolve up to 1. These sensations are octave equivalent, meaning changing the octave doesn't change the sensation, though it might change the overall emotion. They are also independent of the 12 keys. Changing the key, changes the brightness/timbre of the key context, but not these sensations.

Learning this starts with learning these 12 sensations. Then, we can start grouping them together into melodies and chords. The combination of multiple sensations in a chord creates emotions. For example, a major 7 chord feels stable due to the 1 & 5, warm due to the 3, but with a melancholic pull due to the 7. This nuanced emotion can be learned and recognized as Imaj7. (we won't teach grouping).

A subtle but CRITICAL distinction is this is NOT the interval approach that is taught in a basic music theory class. The interval approach is when students memorize the sound of each interval between two notes out of the 12. The difference is in my method, notes are heard relative to the tonic. Yes, these are technically intervals, but the tonic is implied. For this reason, I use the word "scale_degrees" instead of "intervals". These sensations are _constant_. Conversely, in the interval method, intervals are essentially memorized _out of context_. Every new interval starts from the end of the last interval. This creates cognitive dissonance when the actual key context suggests a different emotion than the interval implies out of context. For example, a minor 3rd, from the tonic sounds dark/sad, but it occurs from the 3rd to the 5th of a major triad built on the 1, which sounds much happier. Using the interval method, we have to override that feeling of happiness to imagine the tonic to be the 3. In my method, we hear 1, 3, 5, or even just the chord as a whole.

### Incentives

- How do we incentivize a specific type of ear training?
    - We use melodies instead of  

- Expansion: Aesthetics of tonal emotion.

### Pedagogical Scaffolding
Given our inexperienced audience, we have to be very intentional about teaching ear-training in a structured way. That said, I am still attached to the idea of a souls-like game, which is very unforgiving in its learning curve. We have had this discussion, and I am willing to concede on the difficulty.

Teaching the controls incrementally  
We can structure the ear-training like a metroidvania, in which players start with almost no abilities and gain additional abilities throughout the game. This would correspond to a widening of the spectrum of pitches that the player can play and must listen for. The player would only have access to the tonic at first. Then another note with a strong pull, like the leading tone or a minor 3rd. The contrasting sensations of stability and tension are ideal for starting. We can continue this process until all the notes are in play.

- How will we communicate the ear training system to the player at the start of the first boss?


It is important to communicate octave equivalence to the player even in the first boss battle. We can do this by playing the tonic an octave above where it was first played, and doing the same for below.

In a full game, we would need to communicate that the idea of relative pitch. The second boss, for example, could have a song written in a different key than the first. This tonic would be clearly established at the beginning of the song. We could communicate relative pitch by starting with melodies from the last battle, transcribed in the new key.


 Use the visual aesthetic and in game narrative to communicate the emotional feeling of the training note. Ex: When introducing harmonic minor, use a dramatic battle with a gothic setting.

Melody sequence example (put all of this together)
- 
-
-
-
-
-
-





-Guide Notes

## II. Combat

### Combat Interface



### The Call and Response Phases of the Game Loop

### Feedback Systems

Immediate, clear visual and auditory feedback is a great way that we can teach ear training without talking about the music theory. Here are some ideas I have.

- The player should hear every note they play, regardless of whether it was right or wrong. If it is wrong, it will introduce dissonance, which will help them understand that it is wrong.

- How do we communicate to the player that they missed a note?
- How do we help the player not get lost when they miss a note in the middle of a phrase?

- 

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
