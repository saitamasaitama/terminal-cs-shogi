using System;
using System.Collections;
using System.Collections.Generic;


class Program
{
  static void Main(string[] args)
  {
    //初期化
    var shogi=Shogi.Create();
    //レンダラ初期化
    ShogiRenderer.CreateFromShogi(shogi);
    shogi.Run();




  }
}
