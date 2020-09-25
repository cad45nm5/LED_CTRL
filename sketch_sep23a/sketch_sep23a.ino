#include "FastLED.h"
#include "main.h"


#define NUM_LEDS 213
#define DATA_PIN 17
#define RESOLUTION 256
#define NUM_METEORS 64
#define INCREASE_BRIGHTNESS (RESOLUTION / NUM_METEORS)
extern int DISPLAY_SPEED=20;

CRGB leds[NUM_LEDS];
CHSV color[NUM_LEDS] ;
int br=100,brs=1;
int mode=0;
int bright=0;
int speed=0;
int wave=155,waves=1;
uart u1(921600);





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

  }
void flow()
{
  
       leds[0]=leds[NUM_LEDS-1];
      for(int i=NUM_LEDS-1;i>0;i--)
      {
         leds[i]=leds[i-1];
      
      }
}





void loop(){

     //rainbow();
     String str=u1.read();
     if(str!="")u1.prtln(">>"+str);
   //  u1.prtln(">>"+str);
     if(str.startsWith("mode:"))
     {
        mode=str.substring(5).toInt();
        if(mode==0)setRGB();
        if(mode==5)
        {
          u1.prtln("--------------customize-------------");
          clearLED(leds);
        }
     }
     if(str.startsWith("brightness:"))
    {
      int bright =str.substring(11).toInt();
      
      FastLED.setBrightness(bright);  
      
     }
     if(str.startsWith("speed:"))
    {
      int speed =str.substring(6).toInt();
      
      DISPLAY_SPEED=speed; 
      
     }
    
    if(mode==5)
    {//led:000123000123
        String led,r,g,b;
        if(str.startsWith("led:"))
        {
          led=str.substring(4,7).toInt();
          r=str.substring(7,10);
          g=str.substring(10,13);
          b=str.substring(13,16);
          u1.prtln("<<led:"+led+"r:"+r+"g:"+g+"b:"+b);
          int ir,ig,ib,iled;
          ir=r.toInt();
          ig=g.toInt();
          ib=b.toInt();
          iled=led.toInt();
          
          leds[iled].setRGB(ig,ir,ib);
        }
    }
   
     switch(mode)
     {
     
        case 0:rainbow();break;
        case 1:starry();break;
        case 2:flow();break;
        case 5:break;
   
      
     }
      
      FastLED.show(); 
      delay(100/DISPLAY_SPEED);
 


  
  
}
