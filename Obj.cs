using System;
using System.Collections;
using System.Collections.Generic;

//レンダリングのみを請け負うヒエラルキ
public abstract class Obj:List<Renderer>{
  public Transform localTransform;
  
  public Obj Parent;
  public List<Obj> Children=new List<Obj>();

  //トランスフォームは親から引き継ぐ

  public void Render(Transform t){

    //transformを計算

    var transform = this.localTransform+t;
    //自身のコンポーネント
    foreach(Renderer r in this){
      r.Render(transform);
    }
    foreach(Obj o in Children){
      o.Render(transform);
    }
       
  }

}
//2Dのトランスフォーム
public struct Transform{
  public int x,y,z;
  //public const Transform ZERO = default(Transform);

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
