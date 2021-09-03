#include <Adafruit_GFX.h> // Library needed for LED matrix
#include <Adafruit_NeoMatrix.h> // Library needed for LED matrix
#include <dht.h> // Library needed for temp sensor
dht DHT; // Define DHT object
#define DHT11_PIN 9 // Defines data pin for temp/humidity sensor
#define PIN 6 // Defines data pin for LED matrix

// variables
int menuSelect; // For switch-case menu
int a; // neded for storing first value
int b; // needed for storing second value
int c; // needed for storing sum

const int ledPin = 13; // Sets pin out for LED output
const int buzzerPin = 12; // sets pin out for pitzo buzzer
const int trigPin = 4; // sets output pin for emitting ultrasonic waves
const int echoPin = 5; // sets input pin for receiving trig feedback
const int ldrPin = A0; // set input pin for photoresistor
long duration; // for calcuating duration of echo pulse
int distance; // for calucating distance of object with HC-SR04 sensor
int safetyDistance; // for triggering LEDs and pitzo buzzer when distance<5cm
int pass = 0; // for incrementing cursor position of LED matrix text
int seconds = 0; // for stopwatch function
int minute = 0; // for stopwatch function

// Constructor for LED Matrix
Adafruit_NeoMatrix matrix = Adafruit_NeoMatrix(32, 8, PIN,
                            NEO_MATRIX_TOP     + NEO_MATRIX_LEFT +
                            NEO_MATRIX_COLUMNS + NEO_MATRIX_ZIGZAG,
                            NEO_GRB            + NEO_KHZ800);
// used for displaying text that scrolls accross the LED matrix see the (printModule)function for example.
int x = matrix.width();

// Array of different LED colors which cycles through when printing text
const uint16_t colors[] = {
  matrix.Color(255, 0, 0), matrix.Color(0, 255, 0), matrix.Color(0, 0, 255)
};

void setup() {
  // Sets inputs and outputs
  pinMode(trigPin, OUTPUT);
  pinMode(echoPin, INPUT);
  pinMode(ledPin, OUTPUT);
  pinMode(buzzerPin, OUTPUT);
  pinMode(ldrPin, INPUT);
  // initialises LED matrix
  matrix.begin();
  matrix.setTextWrap(false);
  // sets brightness between a range of 0-100
  matrix.setBrightness(10);
  matrix.setTextColor(colors[0]); // sets LED text color to the corresponding array value
  matrix.clear(); // clear function disposes of current text, ready for next value


  //Initialize serial and wait for port to open:
  Serial.begin(9600);
  // prints text and menu options for user to select from to the serial monitor
  Serial.println(F("Welcome to the LED Program"));
  delay(1000);
  Serial.println(F(" "));
  delay(1000);
  Serial.println(F("Please select a function "));
  delay(500);
  Serial.println(F(" "));
  delay(500);
  Serial.println(F("Main Menu"));
  delay(500);
  Serial.println(F("---------"));
  delay(500);
  Serial.println(F("1. Stop Watch Function"));
  delay(500);
  Serial.println(F("2. Add Two Numbers Function"));
  delay(500);
  Serial.println(F("3. Light Detection Function"));
  delay(500);
  Serial.println(F("4. Distance Function"));
  delay(500);
  Serial.println(F("5. Temp & Humidity Function"));
  delay(500);
  Serial.println(F("6. Print Module and Number"));
  Serial.println(F(""));
  Serial.flush();
}



// Stopwatch function which simply displays a count value then updates the LED matrix each second with new value
void stopWatch() {
  seconds++; // increments second value every 1000 ms
  matrix.setCursor(5, 0); // sets cursor of LED matrix according to output
  if (minute >= 10) { // if minute is greater than 10 than set mouse cursor to 0 position so that all values are displayed across LED matrix
    matrix.setCursor(0, 0);
  }
  matrix.print(minute); // prints minute value after minute has a value greater than 10
  matrix.setCursor(10, 0); // sets the minute value at position 10 on the LED matrix
  matrix.print(":");
  matrix.setCursor(15, 0); // sets the seconds value to be printed at the 15 position of the LED matrix
  matrix.print(seconds);
  matrix.show(); // used to update LED matrix with data values

  delay(1000); // wait one second before clearing, this delay was used to accuratly calculate each second in the stopwatch method.
  matrix.clear(); // Clear matrix so it’s ready for next input
 // resets the seconds value to 0 after every 60 second interval also increments the minute value 
  if (seconds >= 60){
    minute++;
    seconds = 0;
  }
}

// Light sensor function
void lightSensor() {  
  int ldrStatus = analogRead(ldrPin);  // reads the LRDs resistor analog value 
  if (ldrStatus >= 400) { // If the analog value is greater or equal to 400 light has been detected
    tone(buzzerPin, 100); // Activate Pitzo buzzer 
    digitalWrite(ledPin, HIGH); // Switch on red LEDs
    delay(100); // Switch LEDs and buzzer on for 1/10th of a second
    noTone(buzzerPin); // Switch buzzer off
    digitalWrite(ledPin, LOW); // Switch LEDs off
    delay(100); // Delay for 100 ms
    matrix.setTextColor(matrix.Color(0, 255, 0)); // Set LED matrix text color to green
    matrix.setCursor(0, 0); // Sets x and y cursor position of LED matrix to 0,0
    matrix.print("Light!"); // Prints "Light!" on LED matrix
    matrix.show(); // Needed to start the LED matrix displaying text
    matrix.clear(); // Clears the LED matrix text for ready for next string
  }
  else {
    noTone(buzzerPin); // Stop buzzer
    digitalWrite(ledPin, LOW); // Switch off LEDs
    matrix.setTextColor(matrix.Color(255, 0, 0)); // Sets matrix text color to red
    matrix.setCursor(0, 0); // Sets x and y cursor position of LED matrix to 0,0
    matrix.print("Dark"); // Prints "Dark" on the LED matrix
    matrix.show(); // Needed to start the LED matrix displaying text
    matrix.clear(); // Clears the LED matrix text for ready for next string
  }
}

// Add two numbers function
void addTwoNumbers() {
  Serial.println("Enter the first value, then Press ENTER"); // Asks user for input
  delay(3000); // waits 3 seconds for input
  a = Serial.parseInt(); // Stores user input as int to variable a
  Serial.print("Number one = "); // User feedback
  Serial.println(a); // Prints value of a
  delay(1000); // Waits a second before prompting user for input

  Serial.println("Enter the second value, then Press ENTER"); // Prompts user for input
  delay(3000); // Waits 3 seconds for input
  b = Serial.parseInt(); // Stores second user input as int to variable b
  Serial.print("Number two = "); // User feedback
  Serial.println(b); // Prints value of b
  c = a + b; // Adds a and b stores result in c.

  while (menuSelect = 2) { // while loop that prints the values to the LED matrix
    matrix.fillScreen(0); // used for clearing the matrix
    matrix.setCursor(x, 0); // Sets the x cursor to the value of the matrix.width() used so the text can scroll accross the LED matrix
    matrix.print(a); // Prints value of a to LED matrix
    matrix.print(" + "); // Prints " + " to LED matrix
    matrix.print(b); // Prints value of b to LED matrix
    matrix.print(" = "); // Prints "=" to LED matrix
    matrix.print(c); // Prints the value of c to LED matrix

    if (--x < -60) { // Delays for length of time LED matrix should scroll text
      x = matrix.width(); // Sets x equal to Matrix.width()
      if (++pass >= 3) pass = 0; // Counter increments eveytime text scrolls
      matrix.setTextColor(colors[pass]); // Changes text color of LED matrix for each pass
    }
    matrix.show(); // Displays string data to LED matrix
    delay(75); // Sets the scrolling speed of the text
  }
}

// Distance function
void distanceFunction() {
  // Clears the trigPin
  digitalWrite(trigPin, LOW);
  delayMicroseconds(2);

  // Sets the trigPin on HIGH state for 10 micro seconds
  digitalWrite(trigPin, HIGH);
  delayMicroseconds(10);
  digitalWrite(trigPin, LOW);

  // Reads the echoPin, returns the sound wave travel time in microseconds
  duration = pulseIn(echoPin, HIGH);

  // Calculates distance
  distance = duration * 0.034 / 2;

 // If the sensor detects an object within 5 cms then execute
  safetyDistance = distance;
  if (safetyDistance <= 5) {
    tone(buzzerPin, 100); // Activate buzer
    digitalWrite(ledPin, HIGH); // activate LEDs
    delay(100); // delay for 100ms
    noTone(buzzerPin); // Swich off buzzer
    digitalWrite(ledPin, LOW); // Switch off LEDs
    delay(100); // delay for 10ms
  }
  else {
    noTone(buzzerPin); // If sensor doesn’t detect object within 5cm turn buzzer off
    digitalWrite(ledPin, LOW); // Switch off LEDs
  }

  // Prints the distance on the LED Matrix
  matrix.setCursor(0, 0);
  matrix.print("D: ");
  matrix.print(distance);
  matrix.show();
  matrix.clear();
}

// Prints the Embedded System module name and CIS code to LED matrix
void printMoudle() {
  matrix.fillScreen(0);
  matrix.setCursor(x, 0);
  matrix.print("EMBEDDED SYSTEMS CIS3146");
  // Same code used in add two numbers method for scrolling text
  if (--x < -160) {
    x = matrix.width();
    if (++pass >= 3) pass = 0;
    matrix.setTextColor(colors[pass]);
  }
  matrix.show();
  delay(75);
}

// Checks temp and humidity and prints temp value to LED matrix
void tempAndHumidity()
{
  int chk = DHT.read11(DHT11_PIN); // constructor for temp sensor
  Serial.print("Temperature = "); // Prints to serial monitor for user feedback
  Serial.println(DHT.temperature); // Prints sensors temp values
  Serial.print("Humidity = "); // Prints to serial monitor for user feedback
  Serial.println(DHT.humidity); // Prints sensors humidity values
  matrix.setCursor(0, 0);
  matrix.print("T: ");
  matrix.print(DHT.temperature); // Prints sensors temp value to LED matrix
  matrix.show();
  matrix.clear();
  delay(1000); // Update temp values every second
}



void loop() {
  //check for valid user input. halts program, prints error message then loops back to check for valid input
  if (menuSelect < 1 || menuSelect > 6) {

    if (Serial.available())  //Constantly checks to see if anything has been sent over the USB Connection and if it needs to be processed
    {
      menuSelect = Serial.read(); //Reads user input to decide switch case
      // switch cases for menu functionality
      switch (menuSelect) {
        case '1': 
          Serial.flush();//Try to clear the serial monitor and then go to conversion
          Serial.println("Option 1 Selected");
          while (menuSelect = 1) {
            stopWatch(); // while menuSelect is equal to one loop stopWatch function
          }

          break;

        case '2':
          Serial.println("Option 2 selected "); // For user feedback
          addTwoNumbers(); // If case two then start addTwoNumbers method

          break;

        case '3':
          Serial.println("Option 3 selected ");

          while (menuSelect = 3) {
            lightSensor(); // while menuSelect = 3 loop light sensor funtion
          }
          break;

        case '4':
          Serial.println("Option 4 selected ");
          while (menuSelect = 4) { // while menuSelect = 4 loop distanceFunction
            distanceFunction();
          }
          break;

        case '5':
          Serial.println("Option 5 Selected");
          while (menuSelect = 5) { // while menuSelect = 5 loop tempfunction
            tempAndHumidity();
          }

        case '6':
          Serial.println("Option 6 Selected");
          while (menuSelect = 6) { // while menuSelect=6 loop printModule function
            printMoudle();
          }
      }

      // print if user input is invalid
      Serial.println("invalid input please select a number from 1-6");

    }
  }
}
