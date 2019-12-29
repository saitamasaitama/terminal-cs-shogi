using System;
using System.Linq;
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
  public static pos ZERO=>new pos(){x=0,y=0};
  public static pos operator +(pos A,pos B){
    return new pos(){
      x=A.x+B.x,y=A.y+B.y
    };
  }

  //オーナーによってポジションを反転させる
  public pos OwnPos(TypeOwner o)=> o switch{
      TypeOwner.GYOKU=>this, 
      TypeOwner.OU=>pos.From(
          9-(this.x+1),
          9-(this.y+1)
      ) ,
      _=>pos.ZERO
  };
  public override string ToString()=>$"[{this.x},{this.y}]";

  public static bool operator == (pos a,pos b){
    return a.x==b.x && a.y ==b.y;
  }

  public static pos operator * (pos a,int b){
    return pos.From(a.x*b,a.y*b);
  }

  
  /*
  public static pos[] operator * (pos[] positions,int b){
    return positions.Select(a=>a*b).ToArray();
  }
  */


  public static bool operator != (pos a,pos b){
    return a.x!=b.x || a.y !=b.y;
  }

}
