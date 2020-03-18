#include "HCPCA9685.h" // Include the HCPCA9685 library created by Andrew Davies
#include <Wire.h>
#include <Servo.h>
#define I2CAdd 0x40 // Default address of the PCA9685 Module
int val;
int oldval; //for debug 
int adress;

byte in1 =11;
byte in2 = 8;
byte in3 = 12;
byte in4 = 13;
byte ena = 9;
byte enb = 10;
int CurAction=17;
HCPCA9685 HCPCA9685(I2CAdd); // Define Library to use I2C communication

void setup(){ 
  pinMode(ena,OUTPUT);
  pinMode(enb,OUTPUT);
  pinMode(in1,OUTPUT);
  pinMode(in2,OUTPUT);
  pinMode(in3,OUTPUT);
  pinMode(in4,OUTPUT);

  Serial.begin(9600);// подключаем последовательный порт
  Serial3.begin(115200);
  Serial.print("Setup finished");
  HCPCA9685.Init();
  HCPCA9685.Init(SERVO_MODE); // Set to Servo Mode
  HCPCA9685.Sleep(false);
  start_position();
  } 

 void start_position(){
  HCPCA9685.Servo(6, mapping(6, 70));  
  HCPCA9685.Servo(3, mapping(3, 85));  
  HCPCA9685.Servo(0, mapping(0, 210));
  HCPCA9685.Servo(9,  mapping(9, 40));
  HCPCA9685.Servo(12, 40);
  }
  

void test_servo(){
    if(Serial.available() > 0){
      int val = Serial.parseInt();
      Serial.println(val);
      int main_servo = map(val, 0, 180, 0, 375);
      Serial.println(main_servo);
      HCPCA9685.Servo(9, main_servo);
  }
}

int mapping(int adress, int val){
  Serial.print("Adress in mapping fun =");
  Serial.print(adress);
  if(adress == 0)
    return map(val, 0, 270, 0, 380);
  if(adress == 3)
    return map(val, 0, 180, 0, 290);
  if(adress == 6)
    return map(val, 0, 180, 0, 340);
  if(adress == 9)
    return map(val, 0, 180, 0, 375);
   return val;
  }


  
void loop(){
  if(Serial3.available() > 0){
    int inputData = Serial3.parseInt();
    if(inputData != 0 && inputData != val){   //Если поступающие данные не равны нулю и если они не повторяют предыдущие
      if(inputData==1000 || inputData==1003 || inputData==1006 || inputData==1009 || inputData==1012){ // Получение адреса
        if(inputData >= 1010){
          adress = inputData % 100;  // Получение адреса(для 12-го адреса)
        }
        else{
          adress = inputData % 10;  // Получение адреса 0-9
        }
        Serial.println("________________________________________________");
        Serial.print("Adress = ");
        Serial.println(adress); 
      }
      if(inputData == 3019){
        start_position();
        }
      if(inputData >= 2001 && inputData <= 2005){
        CurAction = inputData - 2000;
        }
      }
     
    if(inputData < 300){
      val = inputData;
      Serial.print("inputData = "); //Вывод в монитор порта входящих данных
      Serial.println(inputData);  //Вывод в монитор порта входящих данных
      }
    rotation(); 
  }
}
  
void rotation(){
  if (val > 0 && val != oldval){    //Если угол поворота не равен старому
    Serial.print("Angle = ");
    Serial.println(val);
    HCPCA9685.Servo(adress,mapping(adress, val));
    }
    oldval = val;
}

  
  
  
void wheel_func(){
  ///Weel
if(CurAction == 1)   {                     ////////////////////////////  //Forward

      Serial.print("curaction1");
      analogWrite( ena, 255 );
      analogWrite( enb, 255 );
     
      digitalWrite( in1, LOW );
      digitalWrite( in2, HIGH ); 
    
      digitalWrite( in3, LOW );
      digitalWrite( in4, HIGH);
        }
if(CurAction == 2)   { 
  
       Serial.print("curaction3");
      analogWrite( ena, 255 );             ///////////////////////////    //Backward
      analogWrite( enb, 255 );
      digitalWrite( in1, HIGH );
      digitalWrite( in2, LOW); 
     
      digitalWrite( in3, HIGH );
      digitalWrite( in4, LOW);
}
if(CurAction == 3)   { 
      
      Serial.print("curaction7");
     analogWrite( ena, 255 );              ////////////////////////    //toLeft
     analogWrite( enb, 255 );
     digitalWrite( in1, LOW );
     digitalWrite( in2, HIGH); 
     
     digitalWrite( in3, HIGH );
     digitalWrite( in4, LOW);
}
if(CurAction == 4)   { 
 Serial.print("curaction5");
      analogWrite( ena, 255 );               //////////////////////////    //toRight
      analogWrite( enb, 255 );
      digitalWrite( in1, HIGH );
      digitalWrite( in2, LOW); 
     
      digitalWrite( in3, LOW );
      digitalWrite( in4, HIGH);
}
if(CurAction == 5)   { 
    
       Serial.print("curaction9");
     digitalWrite( in1, LOW );            //////////////////////////////    //Stop
     digitalWrite( in2, LOW );
     
     digitalWrite( in3, LOW );
     digitalWrite( in4, LOW );
   
        }
}
