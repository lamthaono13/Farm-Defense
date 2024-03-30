using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUi
{
    public void Show(bool isShow);

    public void Init<T>(T data);
}
