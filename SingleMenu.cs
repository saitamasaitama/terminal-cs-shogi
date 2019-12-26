using System;
using System.Text;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

//一次元のメニュー
//
public delegate void MenuHandle();

public interface AllowControl{
  void UpArrow();
  void DownArrow();
  void LeftArrow();
  void RightArrow();
}

public struct Menu{
  public string label;
  public MenuHandle callback;

  public static Menu From(string label,MenuHandle callback)=>new Menu()
  {
    label=label,
    callback=callback
  };

  public void Select(){
    callback();
  }
}

public class SingleMenu :List<Menu>{
  private int current=0;

  public override string ToString(){
    StringBuilder sb=new StringBuilder();
    sb.AppendLine("aaa");

    return sb.ToString();
  }

  public string Current=>this[current].label;

  //<- アイテム選択 ->
  public void Apply(TypeControlArrow a){
    
    switch(a){
      case TypeControlArrow.DOWN :
      case TypeControlArrow.RIGHT :
        if(current+1<this.Count){
          current++;
        }else{
          current=0;
        }

        break;
      case TypeControlArrow.LEFT :
      case TypeControlArrow.UP : 
        if(0<=current-1){
          current--;
        }else{
          current=this.Count-1;
        }
        break;
    }
  } 
}

