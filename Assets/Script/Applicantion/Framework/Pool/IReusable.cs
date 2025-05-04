using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReusable
{
    void OnSpawn();//取出

    void OnUnSpawn();//回收
}