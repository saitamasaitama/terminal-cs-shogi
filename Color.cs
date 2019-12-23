using System;
using System.Collections;
using System.Collections.Generic;

public struct color{
  public float h;
  public float s;
  public float v;
  public static color White=>new color(){h=0,s=0,v=1};

  public float H=>this.h%1.0f;

  public static implicit operator ConsoleColor(color c)
  {
    float H=c.H;
    float S=c.s;
    float V=c.v;

    if(V<0.1)return ConsoleColor.Black;
    if(V<0.9)return ConsoleColor.White;

    if(S<0.25){
      //グレー
      return 0.5f<V?ConsoleColor.Gray:ConsoleColor.DarkGray;
    }

    //各カラーテーブル
    for(int i=0;i<6;i++){
      if((1.0/6.0)*i<H && H < (1.0/6.0)*i+1){
        switch(i){
          case 0:{return 0.5f<V?ConsoleColor.Red:ConsoleColor.DarkRed;}
          case 1:{return 0.5f<V?ConsoleColor.Yellow:ConsoleColor.DarkYellow;}
          case 2:{return 0.5f<V?ConsoleColor.Green:ConsoleColor.DarkGreen;}
          case 3:{return 0.5f<V?ConsoleColor.Cyan:ConsoleColor.DarkCyan;}
          case 4:{return 0.5f<V?ConsoleColor.Blue:ConsoleColor.DarkBlue;}
          case 5:{return 0.5f<V?ConsoleColor.Magenta:ConsoleColor.DarkMagenta;}
        }
      }
    }

    return ConsoleColor.White;
  }
}

