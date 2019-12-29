using System;
using System.Collections;
using System.Collections.Generic;

//レンダリングのみを請け負うヒエラルキ
public class View:List<Renderer>{
  public Transform localTransform;
  
  public View Parent;
  public List<View> Children=new List<View>();

  //トランスフォームは親から引き継ぐ

  public void Render(Transform t){

    //transformを計算

    var transform = this.localTransform+t;
    //自身のコンポーネント
    foreach(Renderer r in this){
      r.Render(transform);
    }
    foreach(View o in Children){
      o.Render(transform);
    }
       
  }
  public static View operator + (View v ,Renderer r){
    v.Add(r);
    return v;
  }

  public View Pop(){
    this.RemoveAt(this.Count-1);
    return this;
  }


}
//2Dのトランスフォーム
public struct Transform{
  public int x,y,z;
  //public const Transform ZERO = default(Transform);
  public static Transform ZERO=>new Transform(){
    x=0,y=0,z=0
  };
  public static Transform From(int x,int y,int z)=>new Transform(){x=x,y=y,z=z};
  public static Transform operator + (Transform A,Transform B){
    return new Transform(){
      x=A.x+B.x,
      y=A.y+B.y,
      z=A.z+B.z
    };
  }
}

public abstract class Renderer{
  public bool enable=false;

  public abstract void Render(Transform t);
}
