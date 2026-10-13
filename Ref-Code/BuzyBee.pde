import netP5.*;
import oscP5.*;

OscP5 oscP5;
NetAddress myRemoteLocation;

// game start
boolean gameStarted = false;
boolean ballPlaced = false;

// position
float ballX;
float ballY;

// velocity
float ballVX = 0;
float ballVY = 0;

// size
float ballSize = 100;

// properties
int ballBounce = 0;

// variables
int note = 0;
int buff = 0;
int shift = 0;
int tone = 0;

// obstacles
ArrayList<Obstacle> obstacles = new ArrayList<Obstacle>();

float obstacleSize = 80;

void setup() {
  size(1000, 1000);

  oscP5 = new OscP5(this, 12000);
  myRemoteLocation = new NetAddress("127.0.0.1", 12000);
}

void draw() {
  background(0);
  
  if (!gameStarted) {
    
    ////////////////////
    /// TITLE SCREEN ///
    ////////////////////

    fill(255, 200, 0);
    textAlign(CENTER, CENTER);

    textSize(64);
    text("Buzy Bee", width / 2, height / 2 - 100);

    textSize(24);
    text("Press ENTER to start", width / 2, height / 2 + 0);
    
    fill(150, 200, 150);
    
    textSize(18);
    
    String Info = "CLICK and DRAG to move Buzz\n" +
                  "RELEASE to throw Buzz\n" + 
                  "Press SPACE to place obstacles\n" +
                  "1 = Kick    2 = Snare    3 = Hat\n" +
                  "4 = Low Tom    5 = High Tom\n" +
                  "DELETE = Clear obstacles\n";
    
    text(Info, width / 2, height / 2 + 120);

    return;
  } else {
  
    ////////////////////
    /////// GAME ///////
    ////////////////////
  
    if (ballPlaced) {
      // While holding the mouse, follow the mouse
      if (mousePressed) {
        ballX = mouseX;
        ballY = mouseY;
      }
      // Once released, move the ball
      else {
        ballX += ballVX;
        ballY += ballVY;
    
        // Bounce off left/right walls
        if (ballX - ballSize / 2 <= 0) {
          ballX = ballSize / 2;
          ballVX *= -1;
          ballBounce();
        }
    
        if (ballX + ballSize / 2 >= width) {
          ballX = width - ballSize / 2;
          ballVX *= -1;
          ballBounce();
        }
    
        // Bounce off top/bottom walls
        if (ballY - ballSize / 2 <= 0) {
          ballY = ballSize / 2;
          ballVY *= -1;
          ballBounce();
        }
    
        if (ballY + ballSize / 2 >= height) {
          ballY = height - ballSize / 2;
          ballVY *= -1;
          ballBounce();
        }
      }
    
      // Obstacle Collisions
      checkObstacleCollisions();
      
      updateShift();
      updateTone();
    
      // Draw ball
      noStroke();
      fill(255, 200, 0);
      ellipse(ballX, ballY, ballSize, ballSize);
    }
    
    // Draw Obstacles
    for (Obstacle obstacle : obstacles) {
      obstacle.display();
    }
  }
}

void mousePressed() {
  /* Places Ball and Starts Note */
  
  if (gameStarted) {
  
    // First click places the ball
    ballPlaced = true;
    
    // Start ball at mouse position
    ballX = mouseX;
    ballY = mouseY;
  
    // Stop previous movement
    ballVX = 0;
    ballVY = 0;
  
    // Note //
    
    OscMessage myMessage = new OscMessage("/note");
  
    note = (int)map(mouseY, height, 0, 24, 52);
  
    myMessage.add(note);
  
    oscP5.send(myMessage, myRemoteLocation);
  
    // Note On //
    
    myMessage = new OscMessage("/touch");
  
    int touch = 1;
  
    myMessage.add(touch);
  
    oscP5.send(myMessage, myRemoteLocation);
  }
}

void mouseDragged() {
  /* Aftertouch */
  
  if (gameStarted) {
  
    updateShift();
    updateTone();
  
    // Give ball velocity //
  
    // Horizontal velocity follows horizontal mouse movement
    ballVX = mouseX - pmouseX;
  
    // Vertical velocity follows vertical mouse movement
    ballVY = mouseY - pmouseY;
  }
}

void mouseReleased() {
  /* Release */

  if (gameStarted) {
  
    // Note Off //
  
    OscMessage myMessage = new OscMessage("/touch");
  
    int touch = 0;
  
    myMessage.add(touch);
  
    oscP5.send(myMessage, myRemoteLocation);
  
    // Reset aftertouch
    buff = 0;
    shift = 0;
  
    myMessage = new OscMessage("/shift");
  
    myMessage.add(shift);
  
    oscP5.send(myMessage, myRemoteLocation);
  }
}

void ballBounce() {
  /* Bounces Result in new Message */
  
  OscMessage myMessage = new OscMessage("/bounce");

  int bounce = 1;

  myMessage.add(bounce);
  
  oscP5.send(myMessage, myRemoteLocation);
}

void updateShift() {
  /* Updates Shift Message */
  
  OscMessage myMessage = new OscMessage("/shift");

  shift = (int)map(ballY, height, 0, 24, 52);

  myMessage.add(shift);

  oscP5.send(myMessage, myRemoteLocation);
}

void updateTone() {
  /* Updates Tone Message */
  
  OscMessage myMessage = new OscMessage("/tone");

  tone = (int)map(ballX, width, 0, 24, 52);

  myMessage.add(tone);

  oscP5.send(myMessage, myRemoteLocation);
}

void keyPressed() {
  /* Keyboard actions */
  
  // ENTER = start game
  if (keyCode == ENTER || keyCode == RETURN) {
    gameStarted = true;
  }

  if (gameStarted) {
  
    // SPACE = normal obstacle
    if (key == ' ') {
      obstacles.add(new Obstacle(mouseX, mouseY, obstacleSize / 2, "normObstacle"));
    }
    
    // 1 = kick obstacle
    if (key == '1') {
      obstacles.add(new Obstacle(mouseX, mouseY, obstacleSize / 2, "kickObstacle"));
    }
    
    // 2 = snare obstacle
    if (key == '2') {
      obstacles.add(new Obstacle(mouseX, mouseY, obstacleSize / 2, "snareObstacle"));
    }
    
    // 3 = hat obstacle
    if (key == '3') {
      obstacles.add(new Obstacle(mouseX, mouseY, obstacleSize / 2, "hatObstacle"));
    }
    
    // 4 = low tom obstacle
    if (key == '4') {
      obstacles.add(new Obstacle(mouseX, mouseY, obstacleSize / 2, "lowTomObstacle"));
    }
    
    // 5 = high tom obstacle
    if (key == '5') {
      obstacles.add(new Obstacle(mouseX, mouseY, obstacleSize / 2, "highTomObstacle"));
    }
      
    // Delete = clear all obstacles
    if (keyCode == BACKSPACE || keyCode == DELETE) {
      obstacles.clear(); 
    }  
    
    // Shift = Back to home page
    if (keyCode == SHIFT) {
      initialize();
    }
  }
}

class Obstacle {
  float x;
  float y;
  float radius;
  String type;

  Obstacle(float x, float y, float radius, String type) {
    this.x = x;
    this.y = y;
    this.radius = radius;
    this.type = type;
  }

  void display() {
    noStroke();

    if (type.equals("normObstacle")) {
      fill(150, 200, 150);
    }
    else if (type.equals("kickObstacle")) {
      fill(255, 80, 80);
    }
    else if (type.equals("snareObstacle")) {
      fill(255, 150, 80);
    }
    else if (type.equals("hatObstacle")) {
      fill(255, 220, 80);
    }
    else if (type.equals("lowTomObstacle")) {
      fill(100, 150, 255);
    }
    else if (type.equals("highTomObstacle")) {
      fill(180, 100, 255);
    }

    ellipse(x, y, radius * 2, radius * 2);
  }
}

void checkObstacleCollisions() {

  for (Obstacle obstacle : obstacles) {

    float dx = ballX - obstacle.x; // X distance
    float dy = ballY - obstacle.y; // Y distance

    float distance = sqrt(dx * dx + dy * dy); // Distance forumla

    float collisionDistance = ballSize / 2 + obstacle.radius; // ball radius + obstacle radius

    if (distance > 0 && distance < collisionDistance) {

      // Collision normal //

      float nx = dx / distance;
      float ny = dy / distance;

      // Reflect velocity //

      float dot = ballVX * nx + ballVY * ny;

      ballVX = ballVX - 2 * dot * nx;
      ballVY = ballVY - 2 * dot * ny;

      // Add randomness to skew direction for less repeatability //

      float angle = random(-PI / 8, PI / 8);

      float newVX = ballVX * cos(angle) - ballVY * sin(angle);
      float newVY = ballVX * sin(angle) + ballVY * cos(angle);

      ballVX = newVX;
      ballVY = newVY;

      // Push ball outside obstacle //

      ballX = obstacle.x + nx * collisionDistance;
      ballY = obstacle.y + ny * collisionDistance;

      // Trigger obstacle sound //

      if (obstacle.type.equals("normObstacle")) {
        // Normal obstacle only triggers bounce
        ballBounce();
      }
      else if (obstacle.type.equals("kickObstacle")) {
        drumHit(1);
      }
      else if (obstacle.type.equals("snareObstacle")) {
        drumHit(2);
      }
      else if (obstacle.type.equals("hatObstacle")) {
        drumHit(3);
      }
      else if (obstacle.type.equals("lowTomObstacle")) {
        drumHit(4);
      }
      else if (obstacle.type.equals("highTomObstacle")) {
        drumHit(5);
      }
    }
  }
}

void drumHit(int drum){
  /* Trigger Drum Sound */
  // 1 Kick
  // 2 Snare
  // 3 Hat
  // 4 Tom Low
  // 5 Tom High
  
  OscMessage myMessage = new OscMessage("/drum");
  
  myMessage.add(drum);

  oscP5.send(myMessage, myRemoteLocation);
}

void initialize() {
  /* Reset Game */
  
  // Note OFF //
  
  OscMessage myMessage = new OscMessage("/touch");

  int touch = 0;

  myMessage.add(touch);

  oscP5.send(myMessage, myRemoteLocation);
  
  obstacles.clear();
  
  ballX = 0;
  ballY = 0;
  ballVX = 0;
  ballVY = 0;
  
  ballPlaced = false;
  gameStarted = false;
}
