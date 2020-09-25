#include "main.h"
#include "Arduino.h"



uart::uart(int baud)
{
   begin(baud);
}

uart::~uart()
{
   //Serial.end();
}

String uart::read()
{
  String comdata = "";
    while(Serial.available() > 0)
    {
      char Serial_Buffer = Serial.read();
      if(Serial_Buffer != '\n')                           // still input the String data
      {
          comdata = comdata + Serial_Buffer;
      }
      else break;
    }
   return comdata;
}

void uart:: begin(int baud)
{
    Serial.begin(baud);
}

void uart:: prtln(String str)
{
    Serial.println(str);
}




void clearLED(CRGB (&leds)[213])
{
  for(int i=0;i<213;i++)
  {
      leds[i].setRGB(0,0,0);
  }
}
