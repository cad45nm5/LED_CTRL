#include "FastLED.h"

#define NUM_LEDS 300
#define DATA_PIN 17
#define RESOLUTION 256
#define NUM_METEORS 64
#define INCREASE_BRIGHTNESS (RESOLUTION / NUM_METEORS)
int DISPLAY_SPEED=60;

CRGB leds[NUM_LEDS];
 CHSV color[NUM_LEDS] ;
int br=100,brs=1;
int mode=0;
int bright=0;
int speed=0;
int wave=155,waves=1;

void meteors_with_spots(){
    int j=213;
    int H=random(128, 256);
    for(int i=INCREASE_BRIGHTNESS-1;i<256;i+=INCREASE_BRIGHTNESS){

    if(i==255)leds[j++].setHSV(H,0,i);
    else leds[j++].setHSV(H,255,i);
    }
    for(int a=0;a<50;a++){
      leds[0]=leds[NUM_LEDS-1];
    
      for(int i=NUM_LEDS-1;i>0;i--)
      {
         leds[i]=leds[i-1];
       
      }
      FastLED.show();
      delay(1000/DISPLAY_SPEED);
    }
}

void meteors(){
    int j=213;
    int H=random(128, 256);
    for(int i=INCREASE_BRIGHTNESS-1;i<256;i+=INCREASE_BRIGHTNESS){
          color[j].h=H;
          color[j].s=255;
          color[j].v=i;
        leds[j++]=color[j];
       
    }
    for(int a=0;a<50;a++){
      leds[0]=leds[NUM_LEDS-1];
     
      for(int i=NUM_LEDS-1;i>0;i--)
      {
        
         leds[i]=leds[i-1];
         
      }
      FastLED.show();
      delay(1000/DISPLAY_SPEED);
    }
}

void rainbow(){
  
   br+=brs;
      if(br==200)brs=-1;
      if(br==50)brs=1;
      color[0]=color[NUM_LEDS-1];
      for(int i=NUM_LEDS-1;i>0;i--)
      {
         color[i]=color[i-1];
         color[i].v=br;
         leds[i]=color[i];
      }

      FastLED.show();
      delay(1000/DISPLAY_SPEED);
  
}
void setRGB(){
   for(int i=0;i<NUM_LEDS;i++){
          color[i].h=i;
          color[i].s=255;
          color[i].v=100;
          leds[i]=color[i];
        }
      
}

void setup(){
  delay(1000);
  FastLED.addLeds<WS2812B,DATA_PIN>(leds,NUM_LEDS);
       setRGB();
      
        Serial.begin(9600);
      Serial.println("-------------Current version 20201006 updated---------------");
  
}
void starry(){
int ran=random(0, NUM_LEDS);
if(wave==160)waves=-1;
if(wave==150)waves=1;

  
   for(int i=0;i<NUM_LEDS;i++){
    color[i].h=wave;
    color[i].s=255;
    color[i].v=150;
    leds[i]=color[i];
   }
   wave+=waves;
   color[ran].s=0;
    color[ran].v=255;
    leds[ran]=color[ran];
    FastLED.show();
      delay(1000/DISPLAY_SPEED);
  }


void snow(){
const int R=60;  
int ran[R];

for(int i=0;i<R;i++){
  ran[i]=random(0,NUM_LEDS);
  
}

if(wave==160)waves=-1;
if(wave==150)waves=1;

  
   for(int i=0;i<NUM_LEDS;i++){
    color[i].h=155;
    color[i].s=255;
    color[i].v=150;
    leds[i]=color[i];
 
   }

   for(int i=0;i<R;i++){
    color[ran[i]].s=0;
    color[ran[i]].v=255;
    leds[ran[i]]=color[i];
   
   }
  
   for(int j=0;j<255;j++){
     for(int i=0;i<R;i++){
    color[ran[i]].s=j;
    color[ran[i]].v=150;
    leds[ran[i]]=color[ran[i]];
   
   }
     FastLED.show();
      delay(100/DISPLAY_SPEED);
   }

   
  
  
  }


void loop(){

unsigned long record;
record=millis();
while((millis()-record)<15000)
{
    switch(mode){
              case 0:
                rainbow();
                break;
              case 1:
                meteors();
                break;
              case 2:
                meteors_with_spots();
                break;
              case 3:
                //starry();
                snow();
                break;
              case 4:
                starry();
                break;
              
                
         }

}
     if(mode<4)mode++;
     else mode=0;
     if(mode==0)setRGB();

FastLED.setBrightness(0);
FastLED.show();
delay(2000);
    

     
    switch(bright){
          case 0:
            FastLED.setBrightness(200);
            break;
          case 1:
            FastLED.setBrightness(80);
            break;
          case 2:
            FastLED.setBrightness(30);
            break;
            
     }
      switch(speed){
          case 0:
            DISPLAY_SPEED=60;
            break;
          case 1:
            DISPLAY_SPEED=60;
            break;
          case 2:
            DISPLAY_SPEED=120;
            break;
            
     }
     
     
     
  //rainbow();
  
}
