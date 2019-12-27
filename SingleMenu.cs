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

public class KomaSelectMenu:Menu<Koma>{


}

public class Menu<T>{
  public string label;
  public T obj;
  public MenuHandle callback;

  public virtual string Label=>label;

  public static Menu<T> From(string label,T obj,MenuHandle callback)
    =>new Menu<T>()
  {
    label=label,
    obj=obj,
    callback=callback
  };

  public T Item=>this.obj;

  public void Select(){
    callback();
  }
}

public class SingleMenu<T> :List<Menu<T>>
{
  private int current=0;
  public override string ToString(){
    StringBuilder sb=new StringBuilder();
    sb.AppendLine("aaa");

    return sb.ToString();
  }

  public Menu<T> Current=>this[current];

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
