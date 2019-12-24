using System;
using System.Collections;
using System.Collections.Generic;

public struct pos{
  public int x;
  public int y;
  public static pos From(int x,int y){
    return new pos(){
      x=x,y=y
    };
  }
  public static pos operator +(pos A,pos B){
    return new pos(){
      x=A.x+B.x,y=A.y+B.y
    };
  }
}