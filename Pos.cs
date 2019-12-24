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

  //オーナーによってポジションを反転させる
  public pos OwnPos(TypeOwner o)=> o switch{
      TypeOwner.OU=>this, 
      TypeOwner.GYOKU=>pos.From(
          9-(this.x+1),
          9-(this.y+1)
      ) 
  };
  public override string ToString()=>$"[{this.x},{this.y}]";
}
