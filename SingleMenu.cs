using System;
using System.Text;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

//一次元のメニュー
//
public delegate void OnUpdateSelectKoma(Koma k);

public delegate void MenuHandle();

public interface MenuControl {
  void Enter();
  void Escape();
  void OnArrow(TypeControlArrow a);
}

public class Menu<T>{
  public string label;
  public T obj;
  public MenuHandle callback;

  public virtual string Label=>label;

  public static Menu<T> From(string label,T obj,MenuHandle callback)
    =>new Menu<T>(
    )
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

public class MenuListener:MenuControl{

  private List<MenuControl> menues=new List<MenuControl>();
  
  public void Enter(){
    Current.Enter();
  }
  public void Escape(){
    Current.Escape();
  }
  public virtual void OnArrow(TypeControlArrow a){
    Current.OnArrow(a);
  }
  public MenuControl Current=>menues[0];

  public MenuListener Pop(){
    menues.RemoveAt(0);
    return this;
  }

  public static MenuListener operator +(MenuListener m,MenuControl c){
    m.menues.Insert(0,c);
    return m;
  }
}

public class SingleMenu<T> :List<Menu<T>>,MenuControl
{
  private int current=0;
  public Menu<T> Current=>this[current];

  public SingleMenu(){
   
  }


  public void Enter(){
    this.Current.callback();
  }

  public virtual void OnArrow(TypeControlArrow a){
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
  public void Escape(){
  }

  public static SingleMenu<T> operator + (SingleMenu<T> A,TypeControlArrow a){
    A.OnArrow(a);
    return A;
  }
}
