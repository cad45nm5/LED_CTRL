#ifndef _main_H__
#define _main_H__
#include "Arduino.h"
#include "FastLED.h"



class uart
{
  public :
    uart(int baud);
    ~uart();

    void prtln(String str);
    void begin(int baud);
    String read();
  
  
};


void clearLED(CRGB (&leds)[213]);









#endif
