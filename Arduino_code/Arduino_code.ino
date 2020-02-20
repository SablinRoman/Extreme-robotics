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

        //Вывод в компорт для Дебага
        Serial.print("Adress = ");
        Serial.println(adress); 
      }
        if(inputData >= 2001 && inputData <= 2005){
          CurAction = inputData - 2000;
        }
      }
     
    if(inputData < 300){
        val = inputData;
        if (adress == 0){    //нормирование диапазона горизонтального привода                 
          val = map(val,0, 220, 0, 375);
        }
       if(adress >0)
          val = map(val,0, 180, 0, 350);
        
     
      Serial.print("inputData = "); //Вывод в монитор порта входящих данных
      Serial.println(inputData);  //Вывод в монитор порта входящих данных
      }
    
    testservo();
    //rotation(); 
   
  }
}
  
void rotation(){
  if (val > 0 && val != oldval){    //Если угол поворота не равен старому
    Serial.print("Angle = ");
    Serial.println(val);
    HCPCA9685.Servo(adress,val);
    }
    oldval = val;
}

void testservo(){
      if(Serial3.available() != 0 && val != 0){
        Serial.println("Start test");
        val = Serial3.parseInt();
        Serial.print("11111111tets val = ");
        Serial.println(val);
        HCPCA9685.Servo(1000, val);
      }
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
