// HOW TO PLAY
// SETUP: Open the pd patch, run this script
// GAME DESCRIPTION
// Numbers 1-7 are mapped to major scale degrees 1-7 (do-ti)
// Play around, and enjoy the visuals. Think about the visuals as sparks that fly off from sword hits.
// PURPOSE:
// The end goal is to build a fighting game where ear training is the basis of the combat system. I aimed to create a snippet that might be useful towards that goal.
// Main goal was to connect the sound to the key press. I'd want to blend in a sharp metallic sound with a physical modelling synthesis later.
// INTERACTIVITY DESCRIPTION
// There is visual and auditory feedback synced to the keyboard presses of 1-7. This feedback will become the way the game teaches ear training.
// For now, the game is more like a synth with some visuals. But soon, I will implement the core gameplay loop. A sequence of notes will be played in time, and then
// the player will have to play the same sequence back in the next measure using their relative pitch skills. Music is the focus, but with ear-training as the
// skill in focus instead of rhythm.
// The DGD1 Type of this game will eventually be the conquerer. For now though, the wanderer might appreciate exploring the sounds that they can make.
// The game
// -----------------------------------------------------------------------------------------------------------------------------------------------------------------

// Tweak mode seems to use a special text replacement that is bug prone.
// Don't put comments in the bottom when using tweak mode. Be careful with it.

//// bound the hitCenter in a box in the center. hitBoundary describes this box as a
//// percentage of the total area of the screen.
import oscP5.*;
import netP5.*;
import java.util.BitSet;

OscP5 oscP5;
NetAddress myRemoteLocation;
int tonicMidi = 60;

float hitBoundary = 0.5;
color hitColor;

int numSparks = 20;
Spark[] sparks = new Spark[numSparks];

// Key management should be done in unity at least for now because key events send
// to active window. When we move to a different interface, this could change.
BitSet heldDeg = new BitSet(7); // efficient way to track held keys.


void setup()
{
  size(500,500);
  background(0);
  stroke(255);
  strokeWeight(2);
  // start oscP5 on port 12000 to connect with pd
  oscP5 = new OscP5(this,12000);
  //  ip address and port to use in oscP5.send
   myRemoteLocation = new NetAddress("127.0.0.1",12000);
  // initialize sparks array
  for(int i = 0; i < numSparks; i++) {
    sparks[i] = new Spark(width/2, height/2, color(255));
  }
}
void draw() {
  //Not the most efficient use of draw as it keeps calculating even when sparks
  // offscreen. It will have to do for today.
  background(0);
  for(Spark spark : sparks) {
    spark.update();
    spark.display();
  }
  
}

void keyPressed() {
  // Pre: key pressed
  // Post: If key was 1-7 trigger the noteOn and flying sparks events
      // set the note to held, so it doesn't retrigger.
  
  int scaleDeg = int(key) - 48;
  // only indexes heldDeg if 1 <= deg <= 7 
  if (scaleDeg < 1 || scaleDeg > 7 || heldDeg.get(scaleDeg)) {
      return;
  }
  heldDeg.set(scaleDeg);
  sendNote("/note-on", scaleDeg);
  onHit();
}

void keyReleased() {
  // Pre: key was being held; now it is released
  // Post: if key is a scaleDeg, remove it
  // from heldDeg bit set, allowing it to be pressed again.
  int scaleDeg = int(key) - 48;
  if (scaleDeg < 1 || scaleDeg > 7) {
    return;
  }
  heldDeg.set(scaleDeg, false);
  sendNote("/note-off", scaleDeg);
}

void sendNote(String routeMsg, int scaleDeg) {
// Pre: key 1-7 has been pressed
// routeMsg: matches route in pd for routing to different function.
// Post: Convert the relative pitch to midi and send to pd.
  int midi = degToMidi(scaleDeg, 6);
  OscMessage msg = new OscMessage(routeMsg);
  msg.add(midi);
  oscP5.send(msg, myRemoteLocation);
}

void onHit() {
  // Pre: Key 1-7 pressed
  // Post: Edit the sparks array to setup a new hitEffect
  // choose random color for effect
  hitColor = color(random(150, 256), random(150, 256), random(150, 256));
  // choose new center for effect // can bound it later
  float hitCenterX =random(width*hitBoundary) + width*(1-hitBoundary)/2;
  float hitCenterY =random(height*hitBoundary) + height*(1-hitBoundary)/2;
  // reset sparks array.
  for(int i = 0; i < numSparks; i++) {
    sparks[i].init(hitCenterX, hitCenterY, hitColor);
  }
}

int degToMidi(int deg, int mode) {
  // Helper method to convert major scale degrees to midi notes
  // mode: 1 = major, 7 = locrian.
  // scaling by 12/7 maps 1-7 to major scale intervals. Subtact 1 to start at 0.
  // (Learned this from fundies 2)
  // You will need to consider being in the correct octave later.
  // Note integer truncation is happening here.
  int interval = ((deg+mode-1) * 12/7) - (mode * 12/7);
  return tonicMidi + interval;
}
