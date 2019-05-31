#include <SoftwareSerial.h> //serial communication(bluetooth)
#include <Wire.h>
#define MPUAddr 0x68
#define gB 1 //gas button pin
#define bB 2 //brake button pin
#define rB 3 //recenter button pin
#define sB 4 //shoot button pin
//wheelie triggered by moving control up(ay)
//turning is ax

uint8_t IMU_addr = 0x68;
SoftwareSerial BT(0,1); //variableName(pin for RX, pin for TX);

// Precision Structs
///////////////////////////////////////////
typedef enum : uint8_t
{
  GYRO_PREC_250 = 0,
  GYRO_PREC_500 = 1,
  GYRO_PREC_1000 = 2,
  GYRO_PREC_2000 = 3
} gyro_precision_e;

typedef enum : uint8_t
{
  ACCEL_PREC_2 = 0,
  ACCEL_PREC_4 = 1,
  ACCEL_PREC_8 = 2,
  ACCEL_PREC_16 = 3
} accel_precision_e;
///////////////////////////////////////////

// Functions
///////////////////////////////////////////
void setSleep(bool enable);
void getAccelData(int16_t* ax,int16_t* ay, int16_t* az);
void getGyroData(int16_t* gx,int16_t* gy, int16_t* gz);
void getAccelX(int16_t* ax);
void getAccelY(int16_t* ay);
void getAccelZ(int16_t* az);
void getGyroX(int16_t* gx);
void getGyroY(int16_t* gy);
void getGyroZ(int16_t* gz);
void setGyroPrec(uint8_t prec);
void setAccelPrec(uint8_t prec);
///////////////////////////////////////////



void setup() {
 
// Start MPU (I2C) Communication
////////////////////////////////////////////////////////////////////  
Wire.begin();
// Starting the wire library
Wire.beginTransmission(MPUAddr);
// Starts communicating with the IMU
setSleep(false);
// Turns on our IMU
////////////////////////////////////////////////////////////////////  

// Setup Gyro/Accelero
////////////////////////////////////////////////////////////////////     
setGyroPrec(GYRO_PREC_500);
// Setting gyroscope precision
setAccelPrec(ACCEL_PREC_4);
// Setting accelerometer precision
////////////////////////////////////////////////////////////////////  
  pinMode(11, OUTPUT);  // this pin will pull the HC-05 pin 34 (key pin) HIGH to switch module to AT mode
  digitalWrite(11, HIGH); //9 pin is where hc-05 key is connected to teensy board
  Serial.begin(9600); 
  BT.begin(38400); //fix baud rate at 9600 in serial monitor after running (AT+BT = 9600,1,0)
  
 //pinMode(22, OUTPUT); //set pin 4 as led
  //Serial(Both NL & CR)
  //AT+NAME=
  //AT+UART=9600,1,0

}

void loop() {

  if(BT.available())
    Serial.write(BT.read());

  if(Serial.available())
    BT.write(Serial.read());

  int u = BT.read();
  if(u == 1){ //only send data if unity is ready
  //order of data turn(ax):wheelie(ay):gas:shoot:brake:recenter
  //generate data string to send to unity
  int16_t turn, wheelie, az; //16-bit signed int
  getAccelData(&turn, &wheelie, &az);
  String turn_string = String(turn, DEC); //takes in some double and change it to data string
  String wheelie_string = String(wheelie, DEC);
  int gB_pressed = digitalRead(gB) + 1; //1 is off, 2 is on
  String gB_string = String(gB_pressed, DEC);
  int sB_pressed = digitalRead(sB) + 1;
  String sB_string = String(sB_pressed, DEC);
  int bB_pressed = digitalRead(bB) + 1;
  String bB_string = String(bB_pressed, DEC);
  int rB_pressed = digitalRead(rB_pressed) + 1;
  String rB_string = String(rB_pressed, DEC);
  String data = turn_string + ':' + wheelie_string + ':' + gB_string + ':' + sB_string + ':' + bB_string + ':' + rB_pressed;

  //send data to unity 
  for(int i = 0; i < data.length(); i++){
    BT.write(data[i]);
  }
  BT.write('/n');
 }

}

void setSleep(bool enable)
{
  Serial.println("Starting to Sleep");
  Wire.beginTransmission(MPUAddr);
  Wire.write(0x6B);
  Wire.endTransmission(false);
  Wire.requestFrom(MPUAddr, 1, true);
  uint8_t power = Wire.read();
  power = (enable) ? (power | 0b01000000) : (power & 0b10111111);
  Wire.beginTransmission(MPUAddr);
  Wire.write(0x6B);
  Wire.write(power);
  Wire.endTransmission(true);
  Serial.println("Finishing Sleep");
}

void setAccelPrec(uint8_t prec)
{
  
  prec &= 0b11100011;
  prec = prec<<3;
  
 Wire.beginTransmission(MPUAddr);
 Wire.write(0x1C);
 Wire.write(prec);
 Wire.endTransmission(true); 
}

void setGyroPrec(uint8_t prec)
{
   prec &= 0b11;
  prec = prec<<3;
  Wire.beginTransmission(MPUAddr);
  Wire.write(0x1B);
  Wire.write(prec);
  Wire.endTransmission(true);
}

void getAccelData( int16_t* ax,int16_t* ay, int16_t* az)
{
   getAccelX(ax);
   getAccelY(ay);
   getAccelZ(az);
    
}

void getAccelX(int16_t* x)
{
    Wire.beginTransmission(MPUAddr);
    Wire.write(0x3B);
    Wire.endTransmission(false);
    Wire.requestFrom(MPUAddr, 2, true);
    *x = Wire.read()<<8 | Wire.read();
}
void getAccelY(int16_t* y)
{
    Wire.beginTransmission(MPUAddr);
    Wire.write(0x3D);
    Wire.endTransmission(false);
    Wire.requestFrom(MPUAddr, 2, true);
    *y = Wire.read()<<8 | Wire.read();
}
void getAccelZ(int16_t* z)
{
    Wire.beginTransmission(MPUAddr);
    Wire.write(0x3F);
    Wire.endTransmission(false);
    Wire.requestFrom(MPUAddr, 2, true);
    *z = Wire.read()<<8 | Wire.read();
}

void getGyroData( int16_t* ax,int16_t* ay, int16_t* az)
{
   getGyroX(ax);
   getGyroY(ay);
   getGyroZ(az);
    
}

void getGyroX(int16_t* x)
{
    Wire.beginTransmission(MPUAddr);
    Wire.write(0x43);
    Wire.endTransmission(false);
    Wire.requestFrom(MPUAddr, 2, true);
    *x = Wire.read()<<8 | Wire.read();
}
void getGyroY(int16_t* y)
{
    Wire.beginTransmission(MPUAddr);
    Wire.write(0x45);
    Wire.endTransmission(false);
    Wire.requestFrom(MPUAddr, 2, true);
    *y = Wire.read()<<8 | Wire.read();
}
void getGyroZ(int16_t* z)
{
    Wire.beginTransmission(MPUAddr);
    Wire.write(0x47);
    Wire.endTransmission(false);
    Wire.requestFrom(MPUAddr, 2, true);
    *z = Wire.read()<<8 | Wire.read();
}
