// Class to create sparks that disperse from a given hit point using physics simulation.
// Instantiate each spark with a large initial velocity, and subject them to gravity and air resistance.

class Spark {
  color sparkColor;
  float sparkLength;
  
  PVector acc;
  PVector vel;
  PVector pos;
  float maxInitVel = 10;
  //PVector[] trail; // use a circular array to help update. something I could do later
  
  PVector airResistance;
  PVector g = new PVector(0, 0.2); // downward is positive in pixel coordinate system
  
  Spark(float startX, float startY, color sparkColor) {
    init(startX, startY, sparkColor);
  }
  void init(float startX, float startY, color sparkColor) {
    // Input: Center coordinates of start of spark, spark color.
    // Out: (re)initialize the spark
    this.sparkColor = sparkColor;
    sparkLength = random(3, 20);
    
    // Initial vel simulates collision impulse
    vel = PVector.random2D();
    vel.setMag(random(maxInitVel/3, maxInitVel));
    pos = new PVector(startX, startY);
    airResistance = new PVector(0,0);
  }
  
  void update() {
  // update the particle's motion.
    // air resistance proportional to vel with opposite direction
    airResistance = PVector.mult(vel, -1);
    airResistance.setMag(0.1);
    // net accel consists of downward gravity and airResistance
    acc = PVector.add(g, airResistance);
    vel.add(acc);
    pos.add(vel);
  }
  void display() {
  // Draw the particle.
  // start with a dot, then try a trail.
  // I'd also like it to glow. maybe out of scope.
    //stroke(255);
    //strokeWeight(3);
    stroke(sparkColor);
    // draw line in direction of motion (simple way w/o accounting for torque)
    PVector velUnit = PVector.fromAngle(vel.heading());
    PVector sparkEnd = PVector.add(pos, PVector.mult(velUnit, sparkLength));
    line(pos.x, pos.y, sparkEnd.x, sparkEnd.y);
    // create a trail by remembering past locations
  }
}
